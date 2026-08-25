#!/usr/bin/env node
// Unit tests for detect-dotnet-test-run.js, using Node's built-in test runner
// (no external test library). Run with: node --test .claude/hooks

const test = require("node:test");
const assert = require("node:assert/strict");
const fs = require("node:fs");
const os = require("node:os");
const path = require("node:path");

const {
  resolveTrxPath,
  resolveNunitXmlPath,
  run,
  FRESHNESS_WINDOW_MS,
} = require("./detect-dotnet-test-run.js");

function makeRepoRoot(t) {
  const dir = fs.mkdtempSync(path.join(os.tmpdir(), "detect-dotnet-test-run-"));
  t.after(() => fs.rmSync(dir, { recursive: true, force: true }));
  return dir;
}

function writeFile(filePath, mtimeMs) {
  fs.mkdirSync(path.dirname(filePath), { recursive: true });
  fs.writeFileSync(filePath, "content");
  if (mtimeMs !== undefined) {
    const mtime = new Date(mtimeMs);
    fs.utimesSync(filePath, mtime, mtime);
  }
}

function escapeRegExp(str) {
  return str.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
}

// --- resolveTrxPath -----------------------------------------------------

test("resolveTrxPath: absolute LogFileName is honored directly, even if stale", (t) => {
  const repoRoot = makeRepoRoot(t);
  const trxPath = path.join(repoRoot, "somewhere", "results.trx");
  writeFile(trxPath, Date.now() - FRESHNESS_WINDOW_MS * 10);

  const resolved = resolveTrxPath(repoRoot, trxPath, Date.now());

  assert.ok(resolved, "absolute path should resolve unconditionally");
  assert.equal(resolved.trxPath, trxPath);
});

test("resolveTrxPath: absolute LogFileName that does not exist on disk resolves to null", (t) => {
  const repoRoot = makeRepoRoot(t);
  const missing = path.join(repoRoot, "nope", "results.trx");

  assert.equal(resolveTrxPath(repoRoot, missing, Date.now()), null);
});

test("resolveTrxPath: relative LogFileName resolves via the project's TestResults directory", (t) => {
  const repoRoot = makeRepoRoot(t);
  const trxPath = path.join(repoRoot, "tests", "RBP.Tests.Api", "TestResults", "results.trx");
  writeFile(trxPath, Date.now());

  const resolved = resolveTrxPath(repoRoot, "results.trx", Date.now());

  assert.ok(resolved);
  assert.equal(resolved.trxPath, trxPath);
});

test("resolveTrxPath: stale TestResults file is excluded; a fresh one in another project wins", (t) => {
  const repoRoot = makeRepoRoot(t);
  const staleTrx = path.join(repoRoot, "tests", "RBP.Tests.E2E", "TestResults", "results.trx");
  const freshTrx = path.join(repoRoot, "tests", "RBP.Tests.Api", "TestResults", "results.trx");
  writeFile(staleTrx, Date.now() - FRESHNESS_WINDOW_MS * 5);
  writeFile(freshTrx, Date.now());

  const resolved = resolveTrxPath(repoRoot, "results.trx", Date.now());

  assert.ok(resolved);
  assert.equal(resolved.trxPath, freshTrx);
});

test("resolveTrxPath: only a stale match exists -> null, never silently picks the old file", (t) => {
  const repoRoot = makeRepoRoot(t);
  const staleTrx = path.join(repoRoot, "tests", "RBP.Tests.Api", "TestResults", "results.trx");
  writeFile(staleTrx, Date.now() - FRESHNESS_WINDOW_MS * 5);

  assert.equal(resolveTrxPath(repoRoot, "results.trx", Date.now()), null);
});

test("resolveTrxPath: falls back to TestResults search when the direct repo-root-relative path is stale", (t) => {
  const repoRoot = makeRepoRoot(t);
  const staleDirect = path.join(repoRoot, "results.trx");
  const freshInTestResults = path.join(repoRoot, "tests", "RBP.Tests.Api", "TestResults", "results.trx");
  writeFile(staleDirect, Date.now() - FRESHNESS_WINDOW_MS * 5);
  writeFile(freshInTestResults, Date.now());

  const resolved = resolveTrxPath(repoRoot, "results.trx", Date.now());

  assert.ok(resolved);
  assert.equal(resolved.trxPath, freshInTestResults);
});

// --- resolveNunitXmlPath --------------------------------------------------

test("resolveNunitXmlPath: finds the freshest xml in the named folder, ignoring stale ones", (t) => {
  const repoRoot = makeRepoRoot(t);
  const staleXml = path.join(repoRoot, "tests", "RBP.Tests.Api", "bin", "Debug", "net8.0", "nunit-results", "old.xml");
  const freshXml = path.join(repoRoot, "tests", "RBP.Tests.Api", "bin", "Debug", "net8.0", "nunit-results", "RBP.Tests.Api.xml");
  writeFile(staleXml, Date.now() - FRESHNESS_WINDOW_MS * 5);
  writeFile(freshXml, Date.now());

  const resolved = resolveNunitXmlPath(repoRoot, "nunit-results", Date.now());

  assert.equal(resolved, freshXml);
});

test("resolveNunitXmlPath: only stale xml files exist -> null", (t) => {
  const repoRoot = makeRepoRoot(t);
  const staleXml = path.join(repoRoot, "tests", "RBP.Tests.Api", "bin", "Debug", "net8.0", "nunit-results", "old.xml");
  writeFile(staleXml, Date.now() - FRESHNESS_WINDOW_MS * 5);

  assert.equal(resolveNunitXmlPath(repoRoot, "nunit-results", Date.now()), null);
});

// --- run() (end-to-end decision function) --------------------------------

test("run(): non-dotnet-test command stays silent", (t) => {
  const repoRoot = makeRepoRoot(t);
  const payload = { tool_name: "Bash", tool_input: { command: "dotnet build" } };

  assert.equal(run(payload, { repoRoot, now: Date.now() }), null);
});

test("run(): non-Bash tool stays silent", (t) => {
  const repoRoot = makeRepoRoot(t);
  const payload = { tool_name: "Read", tool_input: { command: "dotnet test" } };

  assert.equal(run(payload, { repoRoot, now: Date.now() }), null);
});

test("run(): dotnet test missing NUnit.TestOutputXml stays silent even when a matching trx exists", (t) => {
  const repoRoot = makeRepoRoot(t);
  writeFile(path.join(repoRoot, "tests", "RBP.Tests.Api", "TestResults", "results.trx"), Date.now());
  const payload = {
    tool_name: "Bash",
    tool_input: { command: 'dotnet test --logger "trx;LogFileName=results.trx"' },
  };

  assert.equal(run(payload, { repoRoot, now: Date.now() }), null);
});

test("run(): dotnet test missing LogFileName stays silent even when a matching NUnit xml exists", (t) => {
  const repoRoot = makeRepoRoot(t);
  writeFile(
    path.join(repoRoot, "tests", "RBP.Tests.Api", "bin", "Debug", "net8.0", "nunit-results", "RBP.Tests.Api.xml"),
    Date.now()
  );
  const payload = {
    tool_name: "Bash",
    tool_input: { command: "dotnet test -- NUnit.TestOutputXml=nunit-results" },
  };

  assert.equal(run(payload, { repoRoot, now: Date.now() }), null);
});

test("run(): dotnet test with both arguments but no artifacts on disk stays silent", (t) => {
  const repoRoot = makeRepoRoot(t);
  const payload = {
    tool_name: "Bash",
    tool_input: {
      command: 'dotnet test --logger "trx;LogFileName=results.trx" -- NUnit.TestOutputXml=nunit-results',
    },
  };

  assert.equal(run(payload, { repoRoot, now: Date.now() }), null);
});

test("run(): stale leftover artifacts from a previous run do not get picked up", (t) => {
  const repoRoot = makeRepoRoot(t);
  const staleTrx = path.join(repoRoot, "tests", "RBP.Tests.Api", "TestResults", "results.trx");
  const staleXml = path.join(repoRoot, "tests", "RBP.Tests.Api", "bin", "Debug", "net8.0", "nunit-results", "RBP.Tests.Api.xml");
  writeFile(staleTrx, Date.now() - FRESHNESS_WINDOW_MS * 5);
  writeFile(staleXml, Date.now() - FRESHNESS_WINDOW_MS * 5);
  const payload = {
    tool_name: "Bash",
    tool_input: {
      command: 'dotnet test --logger "trx;LogFileName=results.trx" -- NUnit.TestOutputXml=nunit-results',
    },
  };

  assert.equal(run(payload, { repoRoot, now: Date.now() }), null);
});

test("run(): absolute LogFileName full match reports both resolved paths", (t) => {
  const repoRoot = makeRepoRoot(t);
  const trxPath = path.join(repoRoot, "out", "results.trx");
  const xmlPath = path.join(repoRoot, "tests", "RBP.Tests.Api", "bin", "Debug", "net8.0", "nunit-results", "RBP.Tests.Api.xml");
  writeFile(trxPath, Date.now());
  writeFile(xmlPath, Date.now());
  const payload = {
    tool_name: "Bash",
    tool_input: {
      command: `dotnet test --logger "trx;LogFileName=${trxPath}" -- NUnit.TestOutputXml=nunit-results`,
    },
  };

  const result = run(payload, { repoRoot, now: Date.now() });

  assert.ok(result);
  assert.equal(result.hookSpecificOutput.hookEventName, "PostToolUse");
  assert.match(result.hookSpecificOutput.additionalContext, new RegExp(escapeRegExp(trxPath)));
  assert.match(result.hookSpecificOutput.additionalContext, new RegExp(escapeRegExp(xmlPath)));
});

test("run(): relative LogFileName full match resolves the project TestResults trx and reports both paths", (t) => {
  const repoRoot = makeRepoRoot(t);
  const trxPath = path.join(repoRoot, "tests", "RBP.Tests.Api", "TestResults", "results.trx");
  const xmlPath = path.join(repoRoot, "tests", "RBP.Tests.Api", "bin", "Debug", "net8.0", "nunit-results", "RBP.Tests.Api.xml");
  writeFile(trxPath, Date.now());
  writeFile(xmlPath, Date.now());
  const payload = {
    tool_name: "Bash",
    tool_input: {
      command: 'dotnet test --logger "trx;LogFileName=results.trx" -- NUnit.TestOutputXml=nunit-results',
    },
  };

  const result = run(payload, { repoRoot, now: Date.now() });

  assert.ok(result);
  assert.match(result.hookSpecificOutput.additionalContext, new RegExp(escapeRegExp(trxPath)));
  assert.match(result.hookSpecificOutput.additionalContext, new RegExp(escapeRegExp(xmlPath)));
});
