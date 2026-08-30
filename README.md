[![](https://img.shields.io/nuget/v/soenneker.playwrights.tests.unit.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.playwrights.tests.unit/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.playwrights.tests.unit/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.playwrights.tests.unit/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.playwrights.tests.unit.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.playwrights.tests.unit/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.playwrights.tests.unit/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.playwrights.tests.unit/actions/workflows/codeql.yml)

# Soenneker.Playwrights.Tests.Unit

A hosted browser-test base class that exposes the application `BaseUrl` and creates ownership-aware Playwright sessions from a shared `PlaywrightTestHost`.

## Installation

```bash
dotnet add package Soenneker.Playwrights.Tests.Unit
```

## Usage

First define a `PlaywrightHostedTestHost` for the application as described by [Soenneker.Playwrights.TestHosts](https://github.com/soenneker/soenneker.playwrights.testhosts). Then use that host as the test class data source:

```csharp
using Microsoft.Playwright;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;

[ClassDataSource<AppPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class AccountPageTests : PlaywrightUnitTest
{
    public AccountPageTests(AppPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Account_page_loads()
    {
        await using BrowserSession session = await CreateSession();

        await session.Page.GotoAsync($"{BaseUrl}account");
        await Assertions.Expect(session.Page.GetByRole(
                            AriaRole.Heading,
                            new PageGetByRoleOptions { Name = "Account" }))
                        .ToBeVisibleAsync();
    }
}
```

`CreateSession()` uses the host's context/page reuse defaults. Override them for one test when needed:

```csharp
await using BrowserSession session = await CreateSession(
    new PlaywrightSessionOptions
    {
        ReuseBrowserContextAcrossSessions = true,
        ReusePageAcrossSessions = false
    },
    cancellationToken);
```

Dispose each returned session. Shared pages and contexts remain owned by the host; isolated ones are released with the session.
