using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GDPRTEST
{
    [TestClass]
    public class GDPR_Admin
    {
        String link = "https://admin.gdpr.netzon.se/auth/login";

        [TestMethod]
        public void firefoxTry()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(link);

            Thread.Sleep(5000);

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void loginFirefox()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(link);

            Thread.Sleep(3000);

            driver.FindElement(By.XPath("/html/body/app-root/div/app-login/div/div/div/div[4]/form/div[1]/app-input/div/input")).Click();
            driver.FindElement(By.XPath("/html/body/app-root/div/app-login/div/div/div/div[4]/form/div[1]/app-input/div/input")).SendKeys("root");
            driver.FindElement(By.XPath("/html/body/app-root/div/app-login/div/div/div/div[4]/form/div[2]/app-input/div/input")).Click();
            driver.FindElement(By.XPath("/html/body/app-root/div/app-login/div/div/div/div[4]/form/div[2]/app-input/div/input")).SendKeys("password");


            driver.FindElement(By.XPath("/html/body/app-root/div/app-login/div/div/div/div[5]/div[1]/app-button/button")).Click();

            Thread.Sleep(5000);

            driver.Close();
            driver.Quit();
        }
    }
}
