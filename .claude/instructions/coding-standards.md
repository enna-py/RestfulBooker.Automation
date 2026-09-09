# Coding Standards

C#-level conventions. Read before writing framework or test code.

## Style

Prefer: file-scoped namespaces · primary constructors where appropriate · target-typed `new` ·
collection expressions · expression-bodied members · `readonly` fields.

Match the surrounding file's existing idiom, naming, and comment density.

## Async

- Everything I/O-bound is `async`, all the way up.
- Never block: no `.Result`, `.Wait()`, `.GetAwaiter().GetResult()`, or `Task.Run` to fake sync.

## Waits

Never use `Thread.Sleep` or Playwright `WaitForTimeout`.

Use event-based waits: `WaitForURLAsync`, `Expect(...)`, `Locator.WaitForAsync`,
`WaitForLoadStateAsync`, `WaitForResponseAsync`.

## Logging

Log every user-visible action: opening a page, clicking, submitting a form, calling an API,
receiving a response, creating or closing a browser. Use the Core logging abstraction, not
`Console`.

## Error handling

- Do not swallow exceptions; do not retry silently.
- Every failure message states **what** failed, **where**, and **why**.
- Custom exceptions live in `RBP.Core/Exceptions`.
