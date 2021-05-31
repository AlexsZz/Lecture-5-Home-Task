using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Lecture_5
{
    [TestFixture]
    public class Task1
    {
        IWebDriver driver;

        [SetUp]
        public void BeforeTest()
        {
            driver = new ChromeDriver();
        }

        //[TestCase("notrealuser", "1")]
        [TestCase("testusername_123", "testpassword_123")]
        public void LoginTest1(string login, string pass)
        {
            string url = "https://www.demoblaze.com/index.html";
            driver.Navigate().GoToUrl(url);
            IWebElement loginButton = driver.FindElement(By.XPath("//*[@id='login2']"));
            loginButton.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='logInModal']")));

            IWebElement loginField = driver.FindElement(By.Id("loginusername"));
            loginField.Click();
            loginField.Clear();
            loginField.SendKeys(login);

            IWebElement passwordField = driver.FindElement(By.CssSelector("input[id='loginpassword']"));
            passwordField.Click();
            passwordField.Clear();
            passwordField.SendKeys(pass);

            IWebElement logInButton = driver.FindElement(By.CssSelector("#logInModal .modal-footer .btn-primary"));
            logInButton.Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='logout2']")));


            StringAssert.Contains(login, driver.FindElement(By.CssSelector("#nameofuser")).Text, "Username is different");
            Assert.True(driver.FindElement(By.CssSelector("#logout2")).Displayed, "Log out button not displayed!");
            //StringAssert.Contains("newuser", driver.FindElement(By.CssSelector("#nameofuser")).Text, "Username is different");

        }

        [TearDown]
        public void AfterTest()
        {
            driver.Quit();
        }

    }
}
