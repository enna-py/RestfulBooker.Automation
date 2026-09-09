# Testing Conventions

Read before adding or changing any Fixture.

## Naming

- **Fixture class:** ends with `Fixture` (`RoomBookingFixture`). Never `Tests` / `Test` / `Spec`.
- **Test method:** `<Action>_Should_<ExpectedResult>` with underscores
  (`User_Should_Be_Able_To_Book_Room`, `Room_List_Should_Match_Api_Data`).
  Not `ShouldBookRoom()`, `BookRoom()`, `Test1()`.

## Required metadata

```csharp
[Test]
[Category("Smoke")]                 // or "Regression"
[Category("UI")]                    // "API" | "UI" | "E2E"
[Property("JiraKey", "RBP-45")]
```

- `Category` = filtering/classification. `Property("JiraKey", …)` = Jira traceability only.
- MUST NOT omit `JiraKey`. MUST NOT turn a Jira key into a `Category`, or replace `JiraKey`
  with a `Category`.
- Preserve every relevant test-type label from the Jira issue as a `Category`.

## Structure

- Arrange / Act / Assert, in that order.
- ≤15 lines, aim for ≤10.
- Reads like documentation.
- Data from Builders; arguments as DTOs/records, not loose primitives.
- No raw locators, no magic strings, no inline test data, no direct Playwright instantiation.

```csharp
RoomApiRequest request = new RoomApiRequestBuilder().WithRoomName(roomName).Build();
RoomDto created = await RoomApiClient.CreateRoomAsync(request);

RoomCardDto expected = new EditRoomDataBuilder().WithDescription("Updated").WithPrice(275).Build();
AdminRoomDetailsPage details = await roomSteps.EditRoomAsync(roomName, expected);

(await details.GetRoomSummaryAsync()).ShouldMatch(expected);
```

## Assertions

- UI state → Playwright `Expect` (`Microsoft.Playwright.Assertions`).
- Data/objects → AwesomeAssertions; compare whole DTOs with `ShouldMatch`, never
  primitive-by-primitive `Assert.AreEqual` chains.
- Never `Assert.AreEqual` / `Assert.True` / `Assert.False` anywhere (tests, Steps, Assertions
  classes).
- **Default location:** the Step that owns the behaviour (see `architecture.md` → Steps).
- **Dedicated `Assertions` class** (`tests/**/Assertions`) only when the check is genuinely
  shared, complex/reusable, part of a Step-less flow, or an infrastructure/data-level check that
  is no single Page's business behaviour.
- A test MAY assert directly for Step-less flows or one-off checks.

## Isolation, data, cleanup

- Each Fixture creates the data it needs; it MUST NOT rely on data from another test or a
  pre-seeded environment.
- Clean up entities the test created (rooms, bookings, messages) so repeated runs stay green.
- Tests MUST be safe under NUnit parallel execution — see each project's `ParallelSettings.cs`.
  Do not share mutable static state between Fixtures.

## Browser

Browser and headless mode come from `config/appsettings.json` / environment only. Tests never
select a browser.

## ReportPortal

- Driven by `ReportPortal.json` / `reportportal.json` per test project, auto-discovered by the
  NUnit adapter. Checked in with `"enabled": false` and empty `apiKey` — never commit a
  credential.
- Enable locally via environment variables (`Enabled=true`, `Server__ApiKey=…`), not by editing
  the tracked JSON.
- On failure, attach: screenshot, logs, exception details.
- Tests MUST NOT save temporary files.

## Result → Jira sync

After `dotnet test` produces a `.trx` + NUnit XML from the same run, the
`.claude/hooks/detect-dotnet-test-run.js` hook surfaces both paths; invoke the `rbp-result-sync`
agent to post each `JiraKey`-tagged result to its Jira issue (deduplicated by TestRun id). Do
not post results to Jira manually.
