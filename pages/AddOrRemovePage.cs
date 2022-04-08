using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using log4net;

namespace NUnit_Automation_Pipeline_Try1.pages;

public class AddOrRemovePage
{
    private IWebDriver? driver;

    private WebDriverWait wait;

    private string pageHeader = "Add/Remove Elements";

    [FindsBy(How = How.XPath,Using = "//div[@class='example']/button[contains(text(),'Add')]")]
    private IWebElement addElementBtn;

    [FindsBy(How = How.XPath, Using = "//div[@id='content']//h3")]
    private IWebElement header;

    private string added_manually = "//div[@id='elements']//button[@class='added-manually']";


    private ILog log = LogManager.GetLogger(typeof(HomePage));

    public AddOrRemovePage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        PageFactory.InitElements(driver, this);
    }


    public Boolean isPageLoaded()
    {
        Console.WriteLine("Waiting for the page load");
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='content']//h3")));
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='example']/button[contains(text(),'Add')]")));
        return String.Equals(pageHeader, header.Text);
    }


    public void clickAddElement()
    {
        Console.WriteLine("Clicking on the add element button");
        wait.Until(ExpectedConditions.ElementToBeClickable(addElementBtn));
        addElementBtn.Click();
        Console.WriteLine("Clicked on the add element button");
    }

    public int getButtonsAddedManually()
    {
        Console.WriteLine("Geeting the buttons added manually");
        IReadOnlyList<IWebElement> addedBtns =  driver.FindElements(By.XPath(added_manually));
        Console.WriteLine("Number of elements added = "+addedBtns.Count);
        return addedBtns.Count();
    }


}
