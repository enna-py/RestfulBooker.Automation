#!/usr/bin/env node
// PostToolUse (Bash) hook: detects a `dotnet test` invocation that produced BOTH
// a trx file (--logger "trx;LogFileName=...") and NUnit's native XML output
// (-- NUnit.TestOutputXml=...) in the same run, resolves both artifact paths on
// disk, and surfaces them as additionalContext so the parent session can invoke
// the "RBP Result Sync" agent with concrete paths. Silent (no stdout) whenever
// the command doesn't match or either artifact can't be confirmed on disk -
// never blocks, never fabricates a path it hasn't verified.

const fs = require("fs");
const path = require("path");

// `dotnet test` flushes both artifacts at the very end of the run, and this hook
// fires immediately after via PostToolUse - so a genuine match is always only
// seconds old. Anything older than this is leftover output from a previous run
// sitting in the same TestResults/output folder, not this invocation's result.
const FRESHNESS_WINDOW_MS = 5 * 60 * 1000;

function readStdin() {
  try {
    return fs.readFileSync(0, "utf8");
  } catch {
    return "";
  }
}

function findFileInDir(dir, predicate) {
  let entries;
  try {
    entries = fs.readdirSync(dir, { withFileTypes: true });
  } catch {
    return [];
  }
  const matches = [];
  for (const entry of entries) {
    if (entry.isFile() && predicate(entry.name)) {
      matches.push(path.join(dir, entry.name));
    }
  }
  return matches;
}

function findDirsNamed(root, targetName, maxDepth = 6) {
  const results = [];
  function walk(dir, depth) {
    if (depth > maxDepth) return;
    let entries;
    try {
      entries = fs.readdirSync(dir, { withFileTypes: true });
    } catch {
      return;
    }
    for (const entry of entries) {
      if (!entry.isDirectory()) continue;
      if (entry.name === "obj" || entry.name === ".git" || entry.name === "node_modules") continue;
      const full = path.join(dir, entry.name);
      if (entry.name === targetName) results.push(full);
      walk(full, depth + 1);
    }
  }
  walk(root, 0);
  return results;
}

function isFresh(mtimeMs, nowMs) {
  return nowMs - mtimeMs <= FRESHNESS_WINDOW_MS;
}

// Resolves the TRX produced by THIS invocation from a `LogFileName=...` value.
//
// - Absolute value: honored exactly as given (unchanged from prior behavior),
//   no freshness check - the caller pointed at an explicit file.
// - Relative value: `dotnet test` writes a bare LogFileName under the test
//   project's default results directory, "<projectDir>/TestResults/<name>", not
//   under the repo root. Try the literal repo-root-relative path first (kept for
//   any setup that genuinely writes there), then fall back to searching every
//   "TestResults" directory under the repo for a same-named file. Every relative
//   candidate must fall inside FRESHNESS_WINDOW_MS of `nowMs`, so a stale
//   results.trx left over from an earlier run is never mistaken for this run's
//   output; among fresh matches the newest one wins.
function resolveTrxPath(repoRoot, trxArg, nowMs) {
  if (path.isAbsolute(trxArg)) {
    if (fs.existsSync(trxArg) && fs.statSync(trxArg).isFile()) {
      return { trxPath: trxArg, mtimeMs: fs.statSync(trxArg).mtimeMs };
    }
    return null;
  }

  const directPath = path.resolve(repoRoot, trxArg);
  if (fs.existsSync(directPath) && fs.statSync(directPath).isFile()) {
    const mtimeMs = fs.statSync(directPath).mtimeMs;
    if (isFresh(mtimeMs, nowMs)) {
      return { trxPath: directPath, mtimeMs };
    }
  }

  const trxBaseName = path.basename(trxArg);
  const testResultsDirs = findDirsNamed(path.join(repoRoot, "tests"), "TestResults");

  let best = null;
  for (const dir of testResultsDirs) {
    const matches = findFileInDir(dir, (name) => name === trxBaseName);
    for (const file of matches) {
      const mtimeMs = fs.statSync(file).mtimeMs;
      if (!isFresh(mtimeMs, nowMs)) continue;
      if (!best || mtimeMs > best.mtimeMs) {
        best = { trxPath: file, mtimeMs };
      }
    }
  }
  return best;
}

// Resolves the NUnit XML for THIS run: searches every directory under
// repoRoot/tests named `xmlFolderName`, and picks the freshest *.xml file inside
// any of them that falls within FRESHNESS_WINDOW_MS of `nowMs`. Tying this to the
// same freshness window as resolveTrxPath keeps the two artifacts pinned to the
// same run without needing a second correlation mechanism.
function resolveNunitXmlPath(repoRoot, xmlFolderName, nowMs) {
  const candidateDirs = findDirsNamed(path.join(repoRoot, "tests"), xmlFolderName);
  let best = null;
  for (const dir of candidateDirs) {
    const xmlFiles = findFileInDir(dir, (name) => name.toLowerCase().endsWith(".xml"));
    for (const file of xmlFiles) {
      const mtimeMs = fs.statSync(file).mtimeMs;
      if (!isFresh(mtimeMs, nowMs)) continue;
      if (!best || mtimeMs > best.mtimeMs) {
        best = { xmlPath: file, mtimeMs };
      }
    }
  }
  return best ? best.xmlPath : null;
}

// Pure decision function: given an already-parsed hook payload and injectable
// { repoRoot, now }, returns the hook response object to emit, or null to stay
// silent. Kept separate from stdin/stdout handling so it can be unit tested
// without spawning the script as a process.
function run(payload, { repoRoot, now }) {
  if (!payload || payload.tool_name !== "Bash") return null;
  const cmd = payload.tool_input && payload.tool_input.command;
  if (!cmd || typeof cmd !== "string") return null;

  if (!/\bdotnet\s+test\b/.test(cmd)) return null;

  const trxMatch = cmd.match(/LogFileName=([^\s"]+)/);
  const xmlFolderMatch = cmd.match(/NUnit\.TestOutputXml=([^\s"]+)/);
  if (!trxMatch || !xmlFolderMatch) return null;

  const trxArg = trxMatch[1];
  const xmlFolderName = xmlFolderMatch[1];

  const resolvedTrx = resolveTrxPath(repoRoot, trxArg, now);
  if (!resolvedTrx) return null;

  const nunitXmlPath = resolveNunitXmlPath(repoRoot, xmlFolderName, now);
  if (!nunitXmlPath) return null;

  const context =
    "A `dotnet test` run just completed producing two artifacts from the SAME run: " +
    `TRX at "${resolvedTrx.trxPath}" and NUnit XML at "${nunitXmlPath}". ` +
    "If any test in this run carries a JiraKey property, invoke the 'RBP Result Sync' agent " +
    "with these two exact paths so it can correlate results by fully-qualified test identity " +
    "and sync each JiraKey-tagged result to Jira. Do not invoke it again for the same TestRun id " +
    "if it was already synced.";

  return {
    hookSpecificOutput: {
      hookEventName: "PostToolUse",
      additionalContext: context,
    },
  };
}

function main() {
  const raw = readStdin();
  if (!raw) return;

  let payload;
  try {
    payload = JSON.parse(raw);
  } catch {
    return;
  }

  // Repo root = two levels up from this script (.claude/hooks/<script>.js)
  const repoRoot = path.resolve(__dirname, "..", "..");

  const result = run(payload, { repoRoot, now: Date.now() });
  if (!result) return;

  process.stdout.write(JSON.stringify(result));
}

module.exports = {
  FRESHNESS_WINDOW_MS,
  findFileInDir,
  findDirsNamed,
  resolveTrxPath,
  resolveNunitXmlPath,
  run,
};

if (require.main === module) {
  main();
}
