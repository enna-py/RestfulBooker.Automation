---
name: ui-automation
description: Playwright UI mechanics for the RestfulBooker framework — locator strategy, Page Object / Component / Step responsibilities, fluent return types, and failure artifacts. Use when writing or changing UI or E2E tests, Page Objects, or Components.
---

# UI Automation (Playwright)

Complements `.claude/instructions/architecture.md` and `testing.md` with UI-specific detail.

## Locator strategy

Priority: `GetByTestId` → `GetByRole` → `GetByLabel` → `GetByPlaceholder` → CSS → XPath.
Never use XPath when another option works.

Build locators hierarchically — a container locator first, children derived from it:

```csharp
private ILocator BookingCard => Page.Locator("[data-testid='booking-card']");
private ILocator ConfirmationTitle => BookingCard.GetByRole(AriaRole.Heading);
private ILocator ConfirmationMessage => BookingCard.GetByRole(AriaRole.Paragraph).First;
```

## Responsibilities

- **Page Object** — locators, atomic actions, navigation. No assertions, no business logic.
- **Component** — reusable fragment; fluent methods; never navigates; no assertions.
- **Step** — business workflow across Pages/Components; may verify behaviour it owns.
- **Test** — orchestrates Steps; Arrange/Act/Assert; ≤10–15 lines.

## Fluent returns

Mutating method → `this`. Navigating method → destination Page Object.

## Waits

Event-based only (`Expect`, `WaitForURLAsync`, `Locator.WaitForAsync`, `WaitForResponseAsync`).
Never `Thread.Sleep` / `WaitForTimeout`.

## Browser & failure artifacts

- Browser and headless mode come from configuration only.
- On failure, ensure screenshot + logs + exception reach ReportPortal. Never write temporary
  files from a test.
