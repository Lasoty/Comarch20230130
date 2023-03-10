// Generated by Selenium IDE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;

namespace BMICalculator.UI.Tests;

[TestFixture]
public class AddMeasurementPositivePathTest
{
    private IWebDriver driver;
    string userLogin;

    public IDictionary<string, object> vars { get; private set; }
    private IJavaScriptExecutor js;
    [SetUp]
    public void SetUp()
    {
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArguments("headless", "no-sandbox", "disable-gpu");
        
        driver = new ChromeDriver(chromeOptions);
        js = (IJavaScriptExecutor)driver;
        vars = new Dictionary<string, object>();

    }
    [TearDown]
    protected void TearDown()
    {
        driver.Close();
        driver.Quit();
    }
    [Test]
    public void AddMeasurementPositivePath()
    {
        driver.Navigate().GoToUrl("https://localhost:7250/");
        driver.Manage().Window.Size = new System.Drawing.Size(1920, 1050);
        driver.FindElement(By.LinkText("Pomiar BMI")).Click();
        driver.FindElement(By.Id("Weight")).SendKeys("100");
        driver.FindElement(By.Id("Height")).SendKeys("180");
        driver.FindElement(By.CssSelector(".btn-primary")).Click();
        var elements = driver.FindElements(By.XPath("//b[contains(.,\'30.86\')]"));
        Assert.True(elements.Count > 0);
    }

    [Test]
    public void AddMeasurementNegativePath()
    {
        driver.Navigate().GoToUrl("https://localhost:7250/");
        driver.Manage().Window.Size = new System.Drawing.Size(1920, 1050);
        
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(driver => driver.FindElements(By.CssSelector(".display-4")).Count > 0);
        
        driver.FindElement(By.LinkText("Pomiar BMI")).Click();
        driver.FindElement(By.Id("Weight")).SendKeys("1000");
        driver.FindElement(By.Id("Height")).SendKeys("1000");
        
        var dropdown = driver.FindElement(By.CssSelector(".form-select"));
        dropdown.FindElement(By.XPath("//option[. = 'Imperialny']")).Click();
        
        driver.FindElement(By.CssSelector(".btn-primary")).Click();
        
        var elements = driver.FindElements(By.XPath("//span[contains(.,\'The field Weight must be between 1 and 400.\')]"));
        Assert.True(elements.Count > 0);

        elements = driver.FindElements(By.XPath("//span[contains(.,\'The field Height must be between 1 and 250.\')]"));
        Assert.True(elements.Count > 0);
    }
}
