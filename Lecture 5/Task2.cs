using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Lecture_5
{

    [TestFixture]
    class Task2
    {
        IWebDriver driver;

        [SetUp]
        public void BeforeTest()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            driver = new ChromeDriver(chromeOptions);
            /*driver = new ChromeDriver();*/
        }

        public void LoginCheckFunc(string login, string pass)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            /*Thread.Sleep(2000);*/
            string url = "http://automationpractice.com/";
            driver.Navigate().GoToUrl(url);

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".footer-container")));

            /*IWebElement loginButton = driver.FindElement(By.XPath("//a[@class='login']"));*/
            IWebElement loginButton = driver.FindElement(By.CssSelector("a.login"));

            /*Thread.Sleep(1000);*/

            loginButton.Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='login_form']")));
            /*wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#login_form")));*/

            IWebElement LoginField = driver.FindElement(By.Id("email"));
            LoginField.Click();
            LoginField.Clear();
            LoginField.SendKeys(login);

            IWebElement PasswordField = driver.FindElement(By.CssSelector("input[id='passwd']"));
            PasswordField.Click();
            PasswordField.Clear();
            PasswordField.SendKeys(pass);

            IWebElement LogInButton = driver.FindElement(By.CssSelector("button[name='SubmitLogin']"));

            /*Thread.Sleep(1000);*/

            LogInButton.Click();


            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".footer-container")));

            List<IWebElement> elementList = new List<IWebElement>();
            elementList.AddRange(driver.FindElements(By.CssSelector(".alert.alert-danger li")));

            if(elementList.Count == 0)
            {
                Assert.Fail("User successfully logged in!");
            } else
            {
                string errorText = "Invalid email address.";
                Assert.AreEqual(errorText, driver.FindElement(By.CssSelector(".alert.alert-danger li")).Text, "Error text is different!");
                Assert.IsTrue(driver.FindElement(By.CssSelector(".alert.alert-danger")).Displayed, "Error massage was not displayed!");

            }
        }

        [TestCase("JohnDoe", "passw0rd")]
        [TestCase("LiliaJY", "IsNotMe")]
        [TestCase("GoingTo", "BeAuto!")]
        [TestCase("testemailadress@mail.ru", "testpass")]
        public void Test1(string login, string pass)
        {
            LoginCheckFunc(login, pass);
        }

        [TestCaseSource("InvalidUserData")]
        public void Test2(string login, string pass)
        {
            LoginCheckFunc(login, pass);
        }

        public static IEnumerable<TestCaseData> InvalidUserData
        {
            get
            {
                yield return new TestCaseData("JohnDoe", "passw0rd");
                yield return new TestCaseData("LiliaJY", "IsNotMe");
                yield return new TestCaseData("GoingTo", "BeAuto!");
            }
        }

        [TearDown]
        public void AfterTest()
        {
            driver.Quit();
        }
    }
}
