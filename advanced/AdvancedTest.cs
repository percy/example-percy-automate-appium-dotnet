// PER-8195 Phase 3 — automate-appium-dotnet advanced example.
//
// Percy on Automate with Appium captures a mobile *browser* session (Chrome on a real
// Android device), the same flow as ../PercyTest.cs. Native apps belong to App Percy:
// the CLI's Automate capture runs JavaScript to read the screen size and regions, which
// a native app session cannot do.
//
// PercyOnAutomate logs and swallows capture errors, so `make test` fails the run when
// the Percy log reports one (see Makefile).

using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using PercyIO.Appium;

namespace PoaAdvanced;

[TestFixture]
public class AdvancedTests
{
    private const string HeaderXpath = "//h1";

    private AndroidDriver? _driver;
    private PercyOnAutomate? _poa;

    private static string Env(string name, string fallback) =>
        Environment.GetEnvironmentVariable(name) ?? fallback;

    [OneTimeSetUp]
    public void SetUp()
    {
        var caps = new AppiumOptions { PlatformName = "Android", BrowserName = "chrome" };
        caps.AddAdditionalAppiumOption("bstack:options", new Dictionary<string, object>
        {
            { "osVersion", Env("OS_VERSION", "12.0") },
            { "deviceName", Env("DEVICE", "Samsung Galaxy S22 Ultra") },
            { "appiumVersion", Env("APPIUM_VERSION", "2.19.0") },
            { "projectName", Env("PERCY_PROJECT", "Percy Automate Appium-.NET Advanced") },
            { "buildName", Env("PERCY_BUILD", "Advanced Automate Appium .NET") },
            { "sessionName", "advanced_visual_test" },
            { "userName", Env("BROWSERSTACK_USERNAME", "") },
            { "accessKey", Env("BROWSERSTACK_ACCESS_KEY", "") },
        });
        _driver = new AndroidDriver(new Uri("https://hub-cloud.browserstack.com/wd/hub"), caps, TimeSpan.FromMinutes(3));
        _driver.Navigate().GoToUrl(Env("URL", "https://en.wikipedia.org/wiki/BrowserStack"));
        _poa = new PercyOnAutomate(_driver);
        Thread.Sleep(5000);
    }

    [OneTimeTearDown]
    public void TearDown() => _driver?.Quit();

    [Test]
    public void ExercisesBaseline() => _poa!.Screenshot("Wikipedia Article");

    [Test]
    public void ExercisesFullPage()
    {
        _poa!.Screenshot("Wikipedia Article — full page", new Dictionary<string, object>
        {
            { "full_page", true },
        });
    }

    [Test]
    public void ExercisesIgnoreRegionsViaXpath()
    {
        _poa!.Screenshot("Wikipedia Article — ignore via xpath", new Dictionary<string, object>
        {
            { "ignore_region_xpaths", new[] { HeaderXpath } },
        });
    }

    [Test]
    public void ExercisesIgnoreRegionsViaSelector()
    {
        _poa!.Screenshot("Wikipedia Article — ignore via selector", new Dictionary<string, object>
        {
            { "ignore_region_selectors", new[] { "h1" } },
        });
    }

    [Test]
    public void ExercisesCustomIgnoreRegions()
    {
        var region = new Dictionary<string, object>
        {
            { "top", 0 }, { "bottom", 100 }, { "left", 0 }, { "right", 300 },
        };
        _poa!.Screenshot("Wikipedia Article — custom ignore region", new Dictionary<string, object>
        {
            { "custom_ignore_regions", new[] { region } },
        });
    }

    [Test]
    public void ExercisesConsiderRegionsViaXpath()
    {
        _poa!.Screenshot("Wikipedia Article — consider via xpath", new Dictionary<string, object>
        {
            { "consider_region_xpaths", new[] { HeaderXpath } },
        });
    }

    [Test]
    public void ExercisesSyncMode()
    {
        _poa!.Screenshot("Wikipedia Article — sync", new Dictionary<string, object>
        {
            { "sync", true },
        });
    }

    [Test]
    public void ExercisesTestCaseAndLabels()
    {
        _poa!.Screenshot("Wikipedia Article — test_case + labels", new Dictionary<string, object>
        {
            { "test_case", "home-smoke" },
            { "labels", "smoke,automate-appium-dotnet" },
        });
    }
}
