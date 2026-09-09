# RestfulBooker Automation — Project Constitution

.NET 8 test-automation framework for the Restful Booker Platform (https://automationintesting.online):
its REST APIs (Room, Auth, Booking, Message, Branding) and its public + admin web UIs.
Tests are organized by layer (API / UI / E2E) and are written to read like documentation.

**Stack:** .NET 8 · NUnit 3 · RestSharp (API) · Microsoft Playwright (browser) ·
AwesomeAssertions (data) · Playwright `Expect` (UI) · Serilog · ReportPortal.

## Architecture

Dependency direction is strict: **Core → Data → Business → Tests**.

| Layer | Project(s) | Contains | Must not |
|---|---|---|---|
| Core | `RBP.Core` | configuration, logging, authentication, shared abstractions | depend on any other solution project |
| Data | `RBP.Data` | DTOs, Builders, Enums, Constants | contain business logic |
| Business | `RBP.Business.Api`, `RBP.Business.Ui` | API Clients, Page Objects, Components, Steps, Browser, mapping | contain assertions (Steps are the one exception) |
| Tests | `RBP.Tests.Api`, `RBP.Tests.Ui`, `RBP.Tests.E2E` | Fixtures, scenarios, `Assertions` classes | contain locators |

## Always-on rules

These apply to every change. Full detail is in `.claude/instructions/` — read the relevant file
before working in that area.

- **Reuse first.** Search for an existing Page, Component, Step, Builder, DTO, Assertion, or
  Client before creating one. Zero duplicate abstractions. No new libraries. No architecture
  rewrites. No renaming public APIs unless asked.
- **Page Objects and Components do interaction only** — locators, atomic actions, navigation.
  Never assertions, business logic, or orchestration.
- **Assertions:** Playwright `Expect` for UI state, AwesomeAssertions for data/objects. Never
  `Assert.*`. Default home is the owning Step; a dedicated `Assertions` class is the exception.
  Compare whole DTOs (`actual.ShouldMatch(expected)`), never primitive-by-primitive.
- **Async everywhere.** No `Thread.Sleep`, `WaitForTimeout`, `.Result`, `.Wait()`, or
  sync-over-async `Task.Run`.
- **Log every user action** — navigation, click, form submit, API call/response, browser
  lifecycle.
- **Tests:** class `*Fixture`; method `<Action>_Should_<ExpectedResult>`;
  `[Category("Smoke"|"Regression")]` + `[Category("API"|"UI"|"E2E")]` +
  `[Property("JiraKey","RBP-XX")]`; Arrange/Act/Assert; data from Builders, not inline
  primitives; ≤15 lines (aim for ≤10).
- **Browser comes from configuration/environment only** — tests never choose it.
- **Secrets never go in tracked files;** no machine-specific paths in tracked instructions.
- **Do not commit** unless explicitly asked. Do not swallow exceptions or retry silently.

## Detailed project knowledge

| Working on… | Read |
|---|---|
| layers, Page/Component/Step/Builder/DTO/Client design, fluent API, forbidden actions | `.claude/instructions/architecture.md` |
| C# style, async, waits, logging, error handling | `.claude/instructions/coding-standards.md` |
| test structure, naming, categories, assertions, parallelism, data isolation, ReportPortal | `.claude/instructions/testing.md` |
| implementing or updating a test from a Jira issue | skill `jira-test-case` |
| UI/E2E Pages, Components, Playwright locators | skill `ui-automation` |
| syncing test results to Jira | agent `rbp-result-sync` (triggered by the `dotnet test` hook) |

## Precedence and conflicts

- Explicit user instructions in the current task override project conventions — unless they
  conflict with system or safety constraints.
- The locations below are different **sources of project knowledge**, not a strict ranking:
  - `CLAUDE.md` — always-on constitution and hard global constraints
  - `.claude/instructions/` — authoritative detailed project conventions
  - `.claude/skills/` — task-specific workflows that complement the project rules
  - `.claude/agents/` — specialized roles / workflows
  - `.claude/hooks/` — automation
  - `.claude/settings.json` — tracked project tooling configuration
  - `.claude/settings.local.json` — machine-specific local configuration (gitignored)
- If two project instructions conflict: name the conflict explicitly; keep the more specific
  rule only when it does not violate a higher-level constraint in `CLAUDE.md`; fix the conflict
  at its source where possible. Never silently pick one.
