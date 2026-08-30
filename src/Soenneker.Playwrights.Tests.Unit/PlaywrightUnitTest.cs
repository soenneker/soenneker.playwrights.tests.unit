using System.Threading;
using System.Threading.Tasks;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.TestEnvironment.Options;
using Soenneker.Playwrights.TestHosts;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Playwrights.Tests.Unit;

/// <summary>
/// Base class for hosted browser tests backed by a <see cref="PlaywrightTestHost"/>.
/// </summary>
public abstract class PlaywrightUnitTest : HostedUnitTest
{
    /// <summary>
    /// Gets the shared Playwright test host.
    /// </summary>
    public PlaywrightTestHost TestHost { get; }

    /// <summary>
    /// Gets the application base URL after the host has initialized.
    /// </summary>
    public string BaseUrl => TestHost.BaseUrl;

    /// <summary>
    /// Creates a browser test base around an initialized Playwright host.
    /// </summary>
    /// <param name="testHost">Shared host supplied by the test framework.</param>
    public PlaywrightUnitTest(PlaywrightTestHost testHost) : base(testHost)
    {
        TestHost = testHost;
    }

    /// <summary>
    /// Creates a browser session using the host defaults or per-test reuse overrides.
    /// </summary>
    /// <param name="sessionOptions">Optional overrides for shared context and page reuse.</param>
    /// <param name="cancellationToken">Token used to cancel session creation.</param>
    /// <returns>The new or shared browser session.</returns>
    protected ValueTask<BrowserSession> CreateSession(PlaywrightSessionOptions? sessionOptions = null, CancellationToken cancellationToken = default)
    {
        return TestHost.CreateSession(sessionOptions, cancellationToken);
    }
}
