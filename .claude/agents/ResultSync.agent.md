---
name: RBP Result Sync
description: Correlates a .trx and a sibling NUnit XML file produced by the SAME `dotnet test` run, joins results by fully-qualified test identity, and synchronizes each JiraKey-tagged result to its Jira issue as a comment. Deduplicates by TRX TestRun id so the same run is never posted twice.
tools: Read, Grep, Glob, Bash, mcp__atlassian__getJiraIssue, mcp__atlassian__addCommentToJiraIssue
---

# RBP Result Sync Agent

## Input contract

Invoked with two explicit file paths from the SAME `dotnet test` run:

- the `.trx` path (wherever `--logger "trx;LogFileName=..."` wrote it)
- the NUnit XML path (wherever `-- NUnit.TestOutputXml=...` wrote it)

Never guess or search for these paths yourself — the caller (the parent session,
typically acting on the `detect-dotnet-test-run.js` hook's context) must supply
both. If only one path is given, or a given path doesn't exist, stop and report
it — do not proceed with partial data.

## Correlation algorithm

1. Parse the `.trx` file. Read `TestRun/@id` — this is the **Run ID** used for
   deduplication. For each `TestDefinitions/UnitTest`, build the identity
   `TestMethod/@className + "." + TestMethod/@name` and look up the matching
   `Results/UnitTestResult` (joined by `@id`/`@testId`) for `@outcome`,
   `@duration`, and `Output/ErrorInfo/Message` + `Output/ErrorInfo/StackTrace`
   when present.
2. Parse the NUnit XML file. For each `test-case`, read `@fullname` and the
   `properties/property[@name='JiraKey']/@value`.
3. Join TRX identity to NUnit XML `test-case/@fullname` — exact string match.
   A test on one side with no match on the other is a correlation error:
   report it, do not silently drop it.
4. Tests with no `JiraKey` property are skipped and counted, not silently
   dropped from the report.

This produces one `TestExecutionResult` per correlated, JiraKey-tagged test:
`FullyQualifiedName`, `JiraKey`, `Outcome`, `Duration`, `ErrorMessage`,
`ErrorStackTrace`.

## Comment format

Exactly this shape (Duration in whole milliseconds):

Passed:
```
Automation result: PASSED
Duration: 99 ms
Run ID: <trx TestRun/@id>
```

Failed:
```
Automation result: FAILED
Duration: 99 ms
Run ID: <trx TestRun/@id>

Error:
<exception message>

Stack trace:
<stack trace>
```

## Deduplication (mandatory, before every comment)

Before posting to a `JiraKey`, call `mcp__atlassian__getJiraIssue` with
`fields: ["comment"]` and scan `fields.comment.comments[].body` for a line
matching `Run ID: <this run's Run ID>`. If found, skip posting and report it
as a duplicate-skip. If not found, post via `mcp__atlassian__addCommentToJiraIssue`.

Known limitation to disclose in every report: `getJiraIssue` with
`fields:["comment"]` may not return the full comment history on an issue with
very many comments (server-side paging). Dedup coverage is therefore reliable
for the comments actually returned, not provably complete on issues with an
unusually long comment history. This is a disclosed limitation, not a reason
to invent a second lookup path — do not build a custom Jira REST client or any
other workaround to close this gap.

If `getJiraIssue` cannot be called at all, or its response shape doesn't
actually expose comment bodies when tested, stop before posting anything and
report that dedup cannot be safely performed. Do not fall back to posting
without a dedup check, and do not invent a workaround.

## Reporting

After each run report: how many results were correlated, how many were synced,
how many were skipped (no `JiraKey`), how many were skipped as duplicates
(same Run ID already present), any correlation mismatches, and the list of
Jira issue keys touched.

## Communication

If a `JiraKey` does not resolve to a real Jira issue (`getJiraIssue` returns
not-found), report it — do not guess an issue, do not create one, do not
silently skip without reporting it.

Never post to Jira without first showing what will be posted, until the
dedup check and the write permission have been confirmed working end to end.
