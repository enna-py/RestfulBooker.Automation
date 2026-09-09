---
name: jira-test-case
description: Implement or update an automated test from a Jira issue (RBP-xx) in the RestfulBooker automation framework — classify the layer, reuse existing framework pieces, plan before coding, run and diagnose, then report. Use whenever a task references a Jira key or asks for a new or changed test case.
---

# Implementing a Jira Test Case

## When to use

Any task that says "implement RBP-xx", "add a test for…", "automate this Jira issue", or that
updates an existing Jira-tracked test.

## Workflow

### 1. Understand
- Read the Jira issue (`mcp__atlassian__getJiraIssue`). Note acceptance criteria, labels, and
  test type (Smoke/Regression, API/UI/E2E).
- If UI behaviour is unclear, inspect the live app before assuming.
- If the live app contradicts the issue, report the discrepancy before implementing.

### 2. Classify the layer
- **API** — pure REST behaviour → `RBP.Tests.Api`, RestSharp clients, no browser.
- **UI** — public or admin site behaviour → `RBP.Tests.Ui`.
- **E2E** — API setup + UI verification, or a multi-surface flow → `RBP.Tests.E2E`.

### 3. Search before creating
Look for a reusable: Page Object · Component · Step · Builder · DTO · Assertion · API Client.
Reuse first; create only what is genuinely missing (`CLAUDE.md` → Always-on rules).

### 4. Plan — get approval for architectural decisions
State: files to create, files to modify, classes to reuse, test-data strategy, cleanup strategy,
stability risks. Wait for approval when the change involves an architectural decision or a
significant test-strategy change.

### 5. Implement
- Approved scope only; do not touch unrelated files.
- Follow `.claude/instructions/architecture.md`, `coding-standards.md`, and `testing.md`.
- For UI work, also follow the `ui-automation` skill.
- Required metadata: two `[Category(...)]` + `[Property("JiraKey","RBP-xx")]` with the exact key.

### 6. Run and diagnose
- Run the new test in isolation, then the relevant regression subset.
- Never call a test stable on a single green run.
- Classify every failure: implementation · pre-existing · environment · parallel execution ·
  shared test data. Do not silently fix unrelated failures.

### 7. Report
Files created/modified, key design decisions, tests run, results, known pre-existing failures,
`git status`, `git diff`. Do not commit unless asked.

## Validation checklist
- [ ] Solution builds.
- [ ] New test green in isolation and in a repeat run.
- [ ] `JiraKey` + both categories present and correct.
- [ ] No duplicate Page / Component / Step / Builder / DTO / Client introduced.
- [ ] Data the test creates is cleaned up.
- [ ] No assertions in Page Objects or Components; no `Assert.*` anywhere.
