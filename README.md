# RestfulBooker Automation Framework

A .NET 8 test automation framework for the [Restful Booker Platform](https://automationintesting.online) — covering its REST APIs (Room, Auth, Booking, Message, Branding) and both its public and admin web UIs.

Tests are organized by layer (API / UI / E2E) and written to read like documentation, using Page Objects, Components, Builders, and DTOs rather than raw locators and inline data.

## Technology Stack

| Concern             | Library                                            |
|----------------------|----------------------------------------------------|
| Runtime              | .NET 8                                              |
| Test runner           | NUnit 3                                             |
| API testing            | RestSharp                                           |
| Browser automation      | Microsoft Playwright                                |
| Non-UI assertions      | AwesomeAssertions                                   |
| UI assertions          | Playwright's `Expect` API (`Microsoft.Playwright.Assertions`) |
| Logging               | Serilog (console + file sinks)                      |
| Test reporting          | ReportPortal (NUnit + Serilog integrations)          |

## Architecture

The solution is split into `src/` (production automation code) and `tests/` (test projects), following a strict dependency direction: **Core → Data → Business → Tests**.

```
RestfulBooker.Automation
│
├── config/                        # appsettings.json, ReportPortal.config.json
│
├── src/
│   ├── RBP.Core/                  # configuration, logging, auth, shared abstractions
│   ├── RBP.Data/                  # DTOs, Builders, Enums — no business logic
│   └── RBP.Business.Api/          # API clients & endpoints
│   └── RBP.Business.Ui/           # Page Objects, Components, Browser, Steps
│
└── tests/
    ├── RBP.Tests.Api/             # API-only fixtures (RestSharp, no browser)
    ├── RBP.Tests.Ui/              # UI-only fixtures (public site)
    └── RBP.Tests.E2E/             # End-to-end fixtures spanning API + Admin/Public UI
```

**Layer responsibilities**

- **Core** — configuration, logging, authentication, shared abstractions. Depends on nothing else in the solution.
- **Data** — DTOs, Builders, enums, constants. No business logic.
- **Business** — Page Objects, Components, API Clients, Browser management, Steps. No assertions.
- **Tests** — Fixtures and test scenarios. Orchestrates everything; never contains locators.

## Design Patterns

- **Page Object** — one class per page; locators, navigation, and atomic UI actions only (no assertions).
- **Component Object** — reusable UI fragments shared across pages (e.g. `RoomCardComponent`, `BookingFormComponent`).
- **Builder** — fluent, immutable test-data construction for DTOs (e.g. `RoomApiRequestBuilder`, `EditRoomDataBuilder`).
- **Steps** — business-workflow orchestration across Pages/Components (e.g. `BookingSteps`, `AuthenticationSteps`). A Step is the business-level layer for the Page(s) it owns, and may include business-level verification for those Page(s) alongside its actions.
- **Factory** — request/client construction (`RequestFactory`, `BrowserFactory`).
- **Fluent API** — UI-mutating methods return `this`; navigation methods return the destination Page Object.

Business-level assertions default to living on the Page Step that owns the corresponding behavior; a separate `tests/**/Assertions` class is the exception. Test bodies never call `Assert.*` directly. Full rules: [.claude/instructions/](.claude/instructions/).

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- A running instance of the [Restful Booker Platform](https://github.com/mwinteringham/restful-booker-platform) (Room/Auth/Booking/Message/Branding APIs and the UI), reachable at the URLs configured in `config/appsettings.json`

### Setup

```bash
# Restore dependencies
dotnet restore

# Install Playwright browsers (once per machine)
pwsh src/RBP.Business.Ui/bin/Debug/net8.0/playwright.ps1 install
```

### Configuration

All environment settings live in [config/appsettings.json](config/appsettings.json):

```json
{
  "Api": {
    "RoomUrl": "http://localhost:3001/room/",
    "AuthUrl": "http://localhost:3004/auth",
    "BookingUrl": "http://localhost:3000/booking/",
    "MessageUrl": "http://localhost:3006/message/",
    "BrandingUrl": "http://localhost:3002/branding/"
  },
  "UI": {
    "BaseUrl": "http://localhost:3003/",
    "Browser": "chromium",
    "Headless": false
  },
  "Credentials": { "Username": "admin", "Password": "password" },
  "Logging": { "LogDirectory": "Logs", "MinimumLevel": "Information" },
  "ReportPortal": { "Endpoint": "", "Project": "", "ApiKey": "" }
}
```

Browser choice and headless mode are driven entirely by this configuration — tests never select a browser themselves.

## Running Tests

```bash
# Run everything
dotnet test

# Run a single project
dotnet test tests/RBP.Tests.Api/RBP.Tests.Api.csproj
dotnet test tests/RBP.Tests.Ui/RBP.Tests.Ui.csproj
dotnet test tests/RBP.Tests.E2E/RBP.Tests.E2E.csproj

# Filter by category
dotnet test --filter "Category=Smoke"
dotnet test --filter "Category=Regression&Category=E2E"
```

| Project | Scope |
|---|---|
| `RBP.Tests.Api` | API-only fixtures, no browser |
| `RBP.Tests.Ui` | Public site UI fixtures |
| `RBP.Tests.E2E` | Full flows across API setup + Admin/Public UI |

## Test Conventions

- **Fixture classes** end with `Fixture` (e.g. `RoomBookingFixture`, `EditRoomFixture`) — never `Tests`/`Test`/`Spec`.
- **Test methods** follow `<Action>_Should_<ExpectedResult>` (e.g. `User_Should_Be_Able_To_Book_Room`).
- Every test carries `[Category("Smoke"|"Regression")]`, `[Category("API"|"UI"|"E2E")]`, and `[Property("JiraKey", "RBP-XX")]` for traceability back to Jira.
- Tests compare DTOs via `actual.ShouldMatch(expected)`, never primitive-by-primitive `Assert.AreEqual` chains.

Example:

```csharp
[Test]
[Category("Regression")]
[Category("UI")]
[Property("JiraKey", "RBP-13")]
public async Task Edit_Room_Via_Admin_Panel_Should_Update_Admin_Room_List()
{
    RoomApiRequest roomRequest = new RoomApiRequestBuilder()
        .WithRoomName(roomName)
        .Build();

    RoomDto createdRoom = await RoomApiClient.CreateRoomAsync(roomRequest);

    RoomCardDto expectedRoom = new EditRoomDataBuilder()
        .WithDescription("Updated via automated test")
        .WithPrice(275)
        .Build();

    AdminRoomDetailsPage roomDetails = await roomSteps.EditRoomAsync(roomName, expectedRoom);

    RoomCardDto adminDisplayedRoom = await roomDetails.GetRoomSummaryAsync();

    adminDisplayedRoom.ShouldMatch(expectedRoom);
}
```

## Logging & Reporting

- Every user action (navigation, click, form submit, API call/response) is logged via Serilog to `Logs/` and the console.
- Test results, logs, and — on failure — a screenshot and exception details are published to ReportPortal when enabled.

### Enabling ReportPortal locally

ReportPortal reporting is driven by the `ReportPortal.json` / `reportportal.json` file in each test project (`tests/RBP.Tests.Api`, `tests/RBP.Tests.E2E`, `tests/RBP.Tests.Ui`), auto-discovered by the ReportPortal NUnit adapter at test-run time. These files are checked in with `"enabled": false` and an empty `apiKey` — no credential is ever committed.

To report to ReportPortal locally, override the values via environment variables rather than editing the tracked JSON (the underlying config loader supports standard `Microsoft.Extensions.Configuration` environment-variable binding, using `__` for nesting):

```bash
export Enabled=true
export Server__ApiKey="<your ReportPortal API key>"
```

Set these in your shell profile or a local, git-ignored `.env`-style script you source before running `dotnet test` — never in a tracked file. Unrelated: application configuration (`config/appsettings.json`) supports its own `RBP_`-prefixed environment-variable overrides via `RestfulBooker.Core.Configuration.ConfigurationService` — that mechanism is separate from the ReportPortal loader above.

## Project Status

| Area | Status |
|---|---|
| Configuration, Logging, Authentication | Done |
| API Clients (Room, Auth, Booking, Message, Branding) | Done |
| Builders & DTOs | Done |
| Page Objects / Components / Steps (Public + Admin UI) | Done |
| API fixtures (`RBP.Tests.Api`) | Done |
| UI fixtures (`RBP.Tests.Ui`) | Done |
| E2E fixtures (`RBP.Tests.E2E`) — Room, Booking, Branding, Message flows | Done |
| CI/CD pipeline | Not yet configured |

## Contributing

Architecture, layering, naming, and design-pattern rules are documented for both humans and
AI-assisted changes:

- [CLAUDE.md](CLAUDE.md) — the concise project constitution (always-on rules, layer map, precedence).
- [.claude/instructions/](.claude/instructions/) — detailed conventions for architecture,
  coding standards, and testing.
- [.claude/skills/](.claude/skills/) — task workflows (`jira-test-case`, `ui-automation`).

Read the relevant file before adding new Pages, Components, Builders, or Fixtures, and reuse
existing implementations before creating new ones.
