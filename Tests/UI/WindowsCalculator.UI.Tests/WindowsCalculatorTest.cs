using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace WindowsCalculator.UI.Tests
{
    public class Tests : CalculatorSession
    {
        private static WindowsElement header;
        private static WindowsElement calculatorResult;

        [Test]
        public void Addition()
        {
            // Find the buttons by their names and click them in sequence to perform 1 + 7 = 8
            session.FindElementByName("One").Click();
            session.FindElementByName("Plus").Click();
            session.FindElementByName("Seven").Click();
            session.FindElementByName("Equals").Click();
            Assert.AreEqual("8", GetCalculatorResultText());
        }

        [Test]
        public void Division()
        {
            // Find the buttons by their accessibility ids and click them in sequence to perform 88 / 11 = 8
            session.FindElementByAccessibilityId("num8Button").Click();
            session.FindElementByAccessibilityId("num8Button").Click();
            session.FindElementByAccessibilityId("divideButton").Click();
            session.FindElementByAccessibilityId("num1Button").Click();
            session.FindElementByAccessibilityId("num1Button").Click();
            session.FindElementByAccessibilityId("equalButton").Click();
            Assert.AreEqual("8", GetCalculatorResultText());
        }

        [Test]
        public void Multiplication()
        {
            // Find the buttons by their names using XPath and click them in sequence to perform 9 x 9 = 81
            session.FindElementByXPath("//Button[@Name='Nine']").Click();
            session.FindElementByXPath("//Button[@Name='Multiply by']").Click();
            session.FindElementByXPath("//Button[@Name='Nine']").Click();
            session.FindElementByXPath("//Button[@Name='Equals']").Click();
            Assert.AreEqual("81", GetCalculatorResultText());
        }

        [Test]
        public void Subtraction()
        {
            // Find the buttons by their accessibility ids using XPath and click them in sequence to perform 9 - 1 = 8
            session.FindElementByXPath("//Button[@AutomationId=\"num9Button\"]").Click();
            session.FindElementByXPath("//Button[@AutomationId=\"minusButton\"]").Click();
            session.FindElementByXPath("//Button[@AutomationId=\"num1Button\"]").Click();
            session.FindElementByXPath("//Button[@AutomationId=\"equalButton\"]").Click();
            Assert.AreEqual("8", GetCalculatorResultText());
        }

        [TestCase("One", "Plus", "Seven", "8")]
        [TestCase("Nine", "Minus", "One", "8")]
        [TestCase("Eight", "Divide by", "Eight", "1")]
        public void Templatized(string input1, string operation, string input2, string expectedResult)
        {
            // Run sequence of button presses specified above and validate the results
            session.FindElementByName(input1).Click();
            session.FindElementByName(operation).Click();
            session.FindElementByName(input2).Click();
            session.FindElementByName("Equals").Click();
            Assert.AreEqual(expectedResult, GetCalculatorResultText());
        }

        [OneTimeSetUp]
        public static void ClassInitialize()
        {
            // Create session to launch a Calculator window
            Setup();

            // Identify calculator mode by locating the header
            try
            {
                header = session.FindElementByAccessibilityId("Header");
            }
            catch
            {
                header = session.FindElementByAccessibilityId("ContentPresenter");
            }

            // Ensure that calculator is in standard mode
            if (!header.Text.Equals("Standard", StringComparison.OrdinalIgnoreCase))
            {
                session.FindElementByAccessibilityId("TogglePaneButton").Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                var splitViewPane = session.FindElementByClassName("SplitViewPane");
                splitViewPane.FindElementByName("Standard Calculator").Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Assert.IsTrue(header.Text.Equals("Standard", StringComparison.OrdinalIgnoreCase));
            }

            // Locate the calculatorResult element
            calculatorResult = session.FindElementByAccessibilityId("CalculatorResults");
            Assert.IsNotNull(calculatorResult);
        }

        [TearDown]
        public static void ClassCleanup()
        {
            TearDown();
        }

        [SetUp]
        public void Clear()
        {
            session.FindElementByName("Clear").Click();
            Assert.AreEqual("0", GetCalculatorResultText());
        }

        private string GetCalculatorResultText()
        {
            return calculatorResult.Text.Replace("Display is", string.Empty).Trim();
        }
    }
}