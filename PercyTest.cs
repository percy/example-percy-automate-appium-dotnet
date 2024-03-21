using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using PercyIO.Appium;

namespace PoaScreenshot{
    [TestFixture]
    [Category("sample-percy-test")]
    public class PercyTest
    {

    string USERNAME = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
    string ACCESS_KEY = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
    string BROWSERSTACK_URL = "https://hub-cloud.browserstack.com/wd/hub";
    protected AndroidDriver<AndroidElement> driver;

    protected WebDriverWait wait;

    [SetUp]
    public void Init()
    {
      AppiumOptions capabilities = new AppiumOptions();
      Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();
      browserstackOptions.Add("osVersion", "11.0");
      browserstackOptions.Add("deviceName", "Samsung Galaxy S21 Ultra");
      browserstackOptions.Add("projectName", "Percy");
      browserstackOptions.Add("buildName", "Appium SDKs");
      browserstackOptions.Add("sessionName", "dotnet-android11");
      browserstackOptions.Add("realMobile", true);
      browserstackOptions.Add("local", "false");
      browserstackOptions.Add("userName", USERNAME);
      browserstackOptions.Add("accessKey", ACCESS_KEY);
      browserstackOptions.Add("browserName", "chrome");
      capabilities.AddAdditionalCapability("bstack:options", browserstackOptions);
      driver = new AndroidDriver<AndroidElement>(new Uri(BROWSERSTACK_URL), capabilities);
    }

        [Test]
        public void SearchBstackDemo()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            PercyOnAutomate poa = new PercyOnAutomate(driver);
            try
              {
                Thread.Sleep(5000);
              }
              catch (ThreadInterruptedException e)
              {
                Console.WriteLine(e.Message);
              }
            
            driver.Navigate().GoToUrl("https://bstackdemo.com/");
            wait.Until(d => d.Title.Contains("StackDemo"));

            // click on the apple products
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='__next']/div/div/main/div[1]/div[1]/label/span"))).Click();

            // [percy note: important step]
            // Percy Screenshot 1
            // take percy_screenshot using the following command
            poa.Screenshot("screenshot_1");

            // Get text of current product
            string productOnPageText = driver.FindElement(By.XPath("//*[@id=\"1\"]/p")).Text;

            // clicking on 'Add to cart' button
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"1\"]/div[4]"))).Click();

            //Check if the Cart pane is visible
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@class=\"float-cart__content\"]")));
            
            string productOnCartText = driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div/div[2]/div[2]/div[2]/div/div[3]/p[1]")).Text;

            // [percy note: important step]
            // Percy Screenshot 2
            // take percy_screenshot using the following command
            poa.Screenshot("screenshot_2");
            
            Assert.AreEqual(productOnCartText, productOnPageText);
        }

        [TearDown]
        public void Cleanup()
        {
          driver.Quit();
        }
    }
}
