using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace NUnit_Automation_Pipeline_Try1.pages;

public class HomePage
{
    
    private WebElement? header;
    
    private WebElement? addOrRemoveElement;

    private string application_xpath = "$//a[@href='{}']";


    private IWebDriver? driver;

    HomePage(IWebDriver driver)
    {
        this.driver = driver;
    }
    
}
