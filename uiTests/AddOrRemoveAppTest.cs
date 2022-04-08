using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit_Automation_Pipeline_Try1.pages;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.Remote;

namespace NUnit_Automation_Pipeline_Try1.uiTests;

public class AddOrRemoveAppTest
{
    private IWebDriver driver;
    private HomePage homePage;
    private AddOrRemovePage addOrRemovePage;
    private ExtentReports extent;
    private ExtentTest extentTest;


    [OneTimeSetUp]
    public void initReport()
    {
        string workingDirectory = Environment.CurrentDirectory;
        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        String reportPath = projectDirectory + "//report.html";
         
        extent = new ExtentReports();
        extent.AttachReporter(new ExtentSparkReporter(reportPath));
        
    }
    
    [SetUp]
    public void setUp()
    {
        Boolean runLocal = true;
        String value = TestContext.Parameters["runLocal"];
        if (value != null)
        {
            runLocal = Boolean.Parse(value);
        }
        
        Console.WriteLine("Running Local:" + runLocal);
        extentTest = extent.CreateTest(TestContext.CurrentContext.Test.Name);

        if (runLocal)
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
        }
        else
        {
            var sauceOptions = new Dictionary<string, object>
            {
                ["name"] = TestContext.CurrentContext.Test.Name
            };

            var chromeOptions = new ChromeOptions
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10"
            };
            chromeOptions.AddAdditionalOption("sauce:options", sauceOptions);
            driver = new RemoteWebDriver(new Uri("https://oauth-chandrasekharvarma.m-8c38b:6b4098a4-5ab0-4a80-825a-896e7daaf8bf@ondemand.us-west-1.saucelabs.com:443/wd/hub"), chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(30));
        }
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        driver.Manage().Window.Maximize();
    }


    [Test]
    public void addElementsTest()
    {
        int numberOfElementsToBeAdded = 4;
        driver.Url = "http://the-internet.herokuapp.com/";
        extentTest.Log(Status.Info,"Loading the application",captureScreenShot());
        homePage = new HomePage(driver);
        addOrRemovePage = new AddOrRemovePage(driver);
        Assert.IsTrue(homePage.isPageLoaded(),"Home Page is not loaded");

        extentTest.Log(Status.Pass, "Application Loaded", captureScreenShot());

        homePage.launchApp(HomePage.Apps.add_remove_elements);

        

        Assert.IsTrue(addOrRemovePage.isPageLoaded(),"Add/Remove page is not loaded");
        extentTest.Log(Status.Pass, "Add/Remove application Loaded", captureScreenShot());
        for (int i = 0; i < numberOfElementsToBeAdded; i++)
        {
            addOrRemovePage.clickAddElement();
        }

        Assert.AreEqual(numberOfElementsToBeAdded, addOrRemovePage.getButtonsAddedManually(), "Elements are not added");
        extentTest.Log(Status.Pass, "validated the added elements", captureScreenShot());
    }

    [TearDown]
    public void cleanUp()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;
        if(status == NUnit.Framework.Interfaces.TestStatus.Passed)
        {
            extentTest.Pass("Test Passed",captureScreenShot());

        }else if(status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            extentTest.Pass("Test Failed", captureScreenShot());
        }
        else
        {
            extentTest.Skip("Test Skipped");
        }
        extent.Flush();
        driver.Quit();
    }

    public AventStack.ExtentReports.Model.Media captureScreenShot()
    {
        DateTime time = DateTime.Now;
        string fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
        ITakesScreenshot ts = (ITakesScreenshot)driver;
        var shot = ts.GetScreenshot().AsBase64EncodedString;
        return MediaEntityBuilder.CreateScreenCaptureFromBase64String(shot, fileName).Build();  
    }
}
