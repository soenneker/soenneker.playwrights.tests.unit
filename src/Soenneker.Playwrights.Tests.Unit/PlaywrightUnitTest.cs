using Soenneker.Tests.FixturedUnit;
using System.Threading.Tasks;
using Soenneker.Playwrights.Fixtures;
using Soenneker.Playwrights.Session;
using Xunit;

namespace Soenneker.Playwrights.Tests.Unit;

/// <summary>
/// A fixtured unit test for testing with Playwright
/// </summary>
public abstract class PlaywrightUnitTest : FixturedUnitTest
{
    protected PlaywrightFixture Fixture { get; }

    protected string BaseUrl => Fixture.BaseUrl;

    protected PlaywrightUnitTest(PlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
        Fixture = fixture;
    }

    protected ValueTask<BrowserSession> CreateSession()
    {
        return Fixture.CreateSession();
    }
}