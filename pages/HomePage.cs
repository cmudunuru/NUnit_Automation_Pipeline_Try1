using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using log4net;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;

namespace NUnit_Automation_Pipeline_Try1.pages;

public class HomePage
{
    
    [FindsBy(How=How.XPath,Using = "//h1[@class='heading']")]
    private IWebElement header;
    
    private IWebElement addOrRemoveElement;

    private string application_xpath = "//a[contains(@href,'{0}')]";

    private ILog log = LogManager.GetLogger(typeof(HomePage));


    private IWebDriver? driver;

    private WebDriverWait wait;

    private string pageHeader = "Welcome to the-internet";



    public enum Apps
    {
        add_remove_elements,
        abtest

    };

    public HomePage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        PageFactory.InitElements(driver,this);
    }

    public Boolean isPageLoaded()
    {
        log.Info("Waiting for the page load");
       
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h1[@class='heading']")));
        return String.Equals(pageHeader, header.Text);
    }

    public void launchApp(Apps appName)
    {
        Console.WriteLine("Launching the application");
        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(String.Format(application_xpath, appName.ToString()))));
        IWebElement? app = driver?.FindElement(By.XPath(String.Format(application_xpath, appName.ToString())));
        Console.WriteLine("Clicking on the application:"+appName.ToString());
        app.Click();
        
        
    }


    
}
