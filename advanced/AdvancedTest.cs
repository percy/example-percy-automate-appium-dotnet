// PER-8195 Phase 3 — automate-appium-dotnet advanced example.

using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using PercyIO.Appium;

namespace PoaAdvanced;

[TestFixture]
public class AdvancedTests
{
    private AndroidDriver<AndroidElement>? _driver;
    private PercyOnAutomate? _poa;

    [OneTimeSetUp]
    public void SetUp()
    {
        var caps = new AppiumOptions();
        var bstackOptions = new Dictionary<string, object>
        {
            { "osVersion", Environment.GetEnvironmentVariable("OS_VERSION") ?? "12.0" },
            { "deviceName", Environment.GetEnvironmentVariable("DEVICE") ?? "Samsung Galaxy S22 Ultra" },
            { "projectName", Environment.GetEnvironmentVariable("PERCY_PROJECT") ?? "Percy Automate Appium-.NET Advanced" },
            { "buildName", Environment.GetEnvironmentVariable("PERCY_BUILD") ?? "Advanced Automate Appium .NET" },
            { "sessionName", "advanced_visual_test" },
            { "userName", Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME") ?? "" },
            { "accessKey", Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY") ?? "" },
        };
        caps.AddAdditionalCapability("bstack:options", bstackOptions);
        caps.AddAdditionalCapability("app", Environment.GetEnvironmentVariable("APP"));
        _driver = new AndroidDriver<AndroidElement>(
            new Uri("https://hub-cloud.browserstack.com/wd/hub"), caps);
        _poa = new PercyOnAutomate(_driver);
        Thread.Sleep(5000);
    }

    [OneTimeTearDown]
    public void TearDown() => _driver?.Quit();

    [Test]
    public void ExercisesBaseline() => _poa!.Screenshot("Wikipedia Home");

    [Test]
    public void ExercisesDeviceNameAndOrientation()
    {
        _poa!.Screenshot("Wikipedia Home — landscape", new Dictionary<string, object>
        {
            { "device_name", Environment.GetEnvironmentVariable("DEVICE") ?? "Samsung Galaxy S22 Ultra" },
            { "orientation", "landscape" },
        });
    }

    [Test]
    public void ExercisesFullscreenAndBars()
    {
        _poa!.Screenshot("Wikipedia Home — fullscreen", new Dictionary<string, object>
        {
            { "fullscreen", true },
            { "status_bar_height", 24 },
            { "nav_bar_height", 0 },
        });
    }

    [Test]
    public void ExercisesIgnoreRegionsViaXpath()
    {
        _poa!.Screenshot("Wikipedia Home — ignore via xpath", new Dictionary<string, object>
        {
            { "ignore_regions_xpaths", new[] { "//android.widget.TextView[@text=\"Search Wikipedia\"]" } },
        });
    }

    [Test]
    public void ExercisesCustomIgnoreRegions()
    {
        var region = new Dictionary<string, object>
        {
            { "top", 0 }, { "bottom", 100 }, { "left", 0 }, { "right", 300 },
        };
        _poa!.Screenshot("Wikipedia Home — custom ignore region", new Dictionary<string, object>
        {
            { "custom_ignore_regions", new[] { region } },
        });
    }

    [Test]
    public void ExercisesConsiderRegionsViaXpath()
    {
        _poa!.Screenshot("Wikipedia Home — consider via xpath", new Dictionary<string, object>
        {
            { "consider_regions_xpaths", new[] { "//android.widget.TextView[@text=\"Search Wikipedia\"]" } },
        });
    }

    [Test]
    public void ExercisesSyncMode()
    {
        _poa!.Screenshot("Wikipedia Home — sync", new Dictionary<string, object>
        {
            { "sync", true },
        });
    }

    [Test]
    public void ExercisesTestCaseAndLabels()
    {
        _poa!.Screenshot("Wikipedia Home — test_case + labels", new Dictionary<string, object>
        {
            { "test_case", "home-smoke" },
            { "labels", "smoke,automate-appium-dotnet" },
        });
    }
}
