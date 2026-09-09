# Architecture & Design Conventions

Authoritative detail behind the architecture summary in `CLAUDE.md`. Read before adding or
changing Page Objects, Components, Steps, Builders, DTOs, API Clients, or Factories.

## Layers

Dependency direction is strict: **Core → Data → Business → Tests**. A lower layer MUST NOT
reference a higher one.

### Core (`RBP.Core`)
Configuration, logging, authentication, shared abstractions, exceptions, helpers.
MUST NOT depend on any other solution project.

### Data (`RBP.Data`)
DTOs, Builders, Enums, Constants. MUST NOT contain business logic or I/O.

### Business (`RBP.Business.Api`, `RBP.Business.Ui`)
API Clients, Endpoints, Factories, Browser management, Page Objects, Components, Steps, mapping.
MUST NOT contain assertions — **except Steps**, which MAY host business-level verification for
the Page(s) they own (see [Steps](#steps)).

### Tests (`RBP.Tests.Api`, `RBP.Tests.Ui`, `RBP.Tests.E2E`)
Fixtures, scenarios, and (when justified) `Assertions` classes. Orchestrate Steps and Clients.
MUST NOT contain locators.

## Patterns in use

Page Object · Component Object · Builder · DTO · Factory · Fluent API · composition over
inheritance.

Design principles: SOLID, DRY, KISS, YAGNI; single responsibility per class; high cohesion, low
coupling; explicit over implicit; small reusable abstractions; prefer extension over
modification.

## Page Objects

MUST contain only: locators, atomic UI actions, navigation, and waits needed for UI stability.

MUST NOT contain: assertions, test logic, business logic, orchestration.

One class per page.

## Components

Reusable UI fragments shared across pages (`RoomCardComponent`, `BookingFormComponent`,
`EditRoomComponent`, …).

- MAY expose fluent methods.
- MUST NOT navigate between pages.
- MUST NOT contain assertions.

## Fluent API

- A method that mutates UI state returns `this`.
- A method that navigates returns the destination Page Object.
- Never expose a bare sequence of `SetX()` / `Click()` / `Save()` calls where a single fluent
  chain reads better.

```csharp
editor.SetDescription(...).SetPrice(...).SaveAsync();   // correct
homePage.OpenLoginAsync();  // returns AdminLoginPage    // correct
```

## Steps

A Step is the **business-level layer for the Page(s) it owns**. It turns multiple UI/API actions
into one meaningful operation (`BookRoom`, `EditRoom`, `LoginAsAdmin`, `CreateBooking`).

Steps MAY:
- orchestrate multiple Pages and Components;
- perform business-level verification for the Page(s) they own
  (`ShouldContainRoomAsync`, `ShouldMatchAsync`, …), receiving expected values as parameters;
- return the resulting Page Object;
- log workflow execution.

Steps MUST NOT:
- create test data (the caller supplies it via a Builder or DTO);
- depend on NUnit, `TestContext`, or ReportPortal.

Steps MAY depend on: Page Objects, Components, DTOs, Builders, and an assertion library
(AwesomeAssertions for data, `Microsoft.Playwright.Assertions` for UI state — only when the Step
exposes verification methods).

### When to create a Step

Create one when the workflow has 3+ actions, is reused, and represents one business action.

Do **not** create a Step just to read UI data, open one page, call one method, or hold a single
assertion. A verification belongs on a Step only when that Step already exists for a real
business action and naturally owns the behaviour.

```
Correct:  BookingSteps, RoomManagementSteps, AuthenticationSteps
Wrong:    OpenHomePageStep, ClickSaveButtonStep, GetRoomsStep
```

## Builders

- Provide sensible defaults, fluent `WithX(...)` methods, and immutable DTO output.
- MUST NOT expose DTO constructors.
- One Builder per DTO — never a second Builder for the same DTO.

## DTOs

- Immutable: `init`-only properties, no setters.
- Reuse an existing DTO before creating a similar one.

## API Clients

- One client per API area (`RoomApiClient`, `BookingApiClient`, …) over the shared base client.
- Never duplicate an existing client or its request-construction logic (`RequestFactory`).

## Forbidden

MUST NOT: rewrite architecture · duplicate classes · rename public APIs without a request ·
change project structure · introduce libraries · add static mutable state · create a Helper when
a Component already covers it · create a duplicate DTO or Builder.
