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

namespace CrossPlatformGDPRTest
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

        [TestMethod]
        public void checkConsentDef()
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

            driver.FindElement(By.XPath("//div[contains(@class,'organization-container')]/div[2]")).Click();

            Thread.Sleep(2000);

            IList<IWebElement> consentlist = driver.FindElements(By.XPath("//div[contains(@class,'inner-wrapper flex-cross-center')]"));
            int consent_count = consentlist.Count();
            consent_count -= 1;
            System.Diagnostics.Debug.WriteLine("Consents: " + consent_count);

            if (consent_count == 0)
            {
                driver.Close();
                driver.Quit();
            }
            else if (consent_count == 1)
            {
                System.Diagnostics.Debug.WriteLine("Consents: " + consent_count);
                driver.Close();
                driver.Quit();
            }
            else
            {
                int wholeNum = consent_count / 10;                             
                for (int k = 1; k <= 10; k++)
                {
                    driver.FindElement(By.XPath("//div[contains(@class,'body-container flex')]/app-tr[" + k + "]/div/div/div/app-tc[3]/div")).Click();
                    Thread.Sleep(3000);
                    driver.FindElement(By.XPath("//div[contains(@class,'left-content flex-cross-center clickable faded')]/div")).Click();               
                    Thread.Sleep(3000);
                }
                for (int j = 1; j <= wholeNum; j++)
                {
                    driver.FindElement(By.XPath("//ul[contains(@class,'ngx-pagination ng-star-inserted')]/li[contains(@class,'pagination-next ng-star-inserted')]")).Click();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                }                
                driver.Close();
                driver.Quit();
            }
        }

        [TestMethod]
        public void checkPatients()
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

            driver.FindElement(By.XPath("//div[contains(@class,'organization-container')]/div[2]")).Click();

            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//div[contains(@class, 'nav-items flex-main-start')]/div[2]")).Click();

            Thread.Sleep(8000);

            IList<IWebElement> consentlist = driver.FindElements(By.XPath("//app-tc[contains(@class,'email-cell flex')]"));
            int consent_count = consentlist.Count();
            System.Diagnostics.Debug.WriteLine("Consents: " + consent_count);

            if (consent_count == 0)
            {
                driver.Close();
                driver.Quit();
            }
            else if (consent_count == 1)
            {
                System.Diagnostics.Debug.WriteLine("Consents: " + consent_count);
                driver.Close();
                driver.Quit();
            }
            else
            {
                for(int j=1;j <= consent_count; j++)
                {
                    driver.FindElement(By.XPath("//div[contains(@class,'body-container flex')]/app-tr["+ j
 +"]/div/div/div/app-tc[4]/div")).Click();
                    Thread.Sleep(5000);
                    driver.FindElement(By.XPath("//span[contains(@class,'font-medium')]")).Click();
                    Thread.Sleep(5000);
                }
            }

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void addPatient()
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

            driver.FindElement(By.XPath("//div[contains(@class,'organization-container')]/div[2]")).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//div[contains(@class, 'nav-items flex-main-start')]/div[2]")).Click();
            Thread.Sleep(8000);

            driver.FindElement(By.XPath("//div[contains(@class,'add-wrapper flex-cross-center clickable ng-star-inserted')]")).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//div[contains(@class,'body-wrapper')]/div[1]/app-button/button")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//div[contains(@class,'icon-wrapper flex-center')]")).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//div[contains(@class,'add-wrapper flex-cross-center clickable ng-star-inserted')]")).Click();        
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//div[contains(@class,'body-wrapper')]/div[2]/app-button/button")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//app-input[contains(@formcontrolname, 'id_number')]/div/input")).SendKeys("789456");            
            driver.FindElement(By.XPath("//app-input[contains(@formcontrolname, 'first_name')]/div/input")).SendKeys("Mark");
            driver.FindElement(By.XPath("//app-input[contains(@formcontrolname, 'middle_name')]/div/input")).SendKeys("B.");
            driver.FindElement(By.XPath("//app-input[contains(@formcontrolname, 'last_name')]/div/input")).SendKeys("Twain");
            driver.FindElement(By.XPath("//app-input[contains(@formcontrolname, 'email_address')]/div/input")).SendKeys("marktwain@mail2.com");
            driver.FindElement(By.XPath("//app-input[contains(@formcontrolname, 'mobile_number')]/div/input")).SendKeys("987654321");

            driver.FindElement(By.XPath("//button[contains(@class,'label btnPrimary')]")).Click();
            Thread.Sleep(2000);

            driver.Close();
            driver.Quit();
        }
    }
}
