using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BMICalculator.UI.Tests
{
    public class Tests
    {
        private IWebDriver driver;
        private IJavaScriptExecutor js;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            js = (IJavaScriptExecutor)driver;
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
        }

        [Test]
        public void LoginToApp()
        {
            driver.Navigate().GoToUrl("https://localhost:7250/");
            driver.Manage().Window.Size = new System.Drawing.Size(1936, 1066);
            driver.FindElement(By.LinkText("Login")).Click();
            driver.FindElement(By.Id("Input_Email")).SendKeys("lasoty@o2.pl");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Qwerty.1");
            //driver.FindElement(By.Id("Input_RememberMe")).Click();
            driver.FindElement(By.Id("login-submit")).Click();

            Assert.IsTrue(driver.FindElement(By.LinkText("Witaj lasoty@o2.pl!"))?.Displayed);
        }
    }
}