using System.Threading;
using System.Threading.Tasks;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.TestEnvironment.Options;
using Soenneker.Playwrights.TestHosts;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Playwrights.Tests.Unit;

/// <summary>
/// A hosted unit test for testing with Playwright
/// </summary>
public abstract class PlaywrightUnitTest : HostedUnitTest
{
    protected PlaywrightTestHost TestHost { get; }

    protected string BaseUrl => TestHost.BaseUrl;

    protected PlaywrightUnitTest(PlaywrightTestHost testHost) : base(testHost)
    {
        TestHost = testHost;
    }

    protected ValueTask<BrowserSession> CreateSession(PlaywrightSessionOptions? sessionOptions = null, CancellationToken cancellationToken = default)
    {
        return TestHost.CreateSession(sessionOptions, cancellationToken);
    }
}