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
using OpenQA.Selenium.Interactions;
using TestContext = NUnit.Framework.TestContext;
using System.Net;
using System.Net.Mail;

namespace GDPRTEST
{
    [TestClass]
    public class GDPR
    {
        String link = "https://gdpr.netzon.se/auth/login";

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

            driver.FindElement(By.XPath("//input[@placeholder='Username']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("root");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("password");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[contains(@class,'font-bold') and contains(text(), 'Log in')]")).Click();

            Thread.Sleep(5000);

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void checkRequests()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(link);

            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//input[@placeholder='Username']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("allan.casey@mail.com");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("12345678");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[contains(@class,'font-bold') and contains(text(), 'Log in')]")).Click();

            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//span[text()='Requests']")).Click();
            Thread.Sleep(2000);

            IList <IWebElement> consentlist = driver.FindElements(By.XPath("//div[contains(@class, 'content-button flex-main-center')]/app-button/button[contains(@class, 'btnSuccess')]"));
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
                driver.FindElement(By.XPath("//button[contains(@class, 'btnSuccess')]")).Click();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//div[contains(@class, 'left-wrapper flex-center clickable')]")).Click();
                Thread.Sleep(4000);
            }
            else
            {
                for (int j = 1; j <= consent_count; j++)
                {
                    driver.FindElement(By.XPath("//div[contains(@class, 'consent-list')]/div[" + j + "]/app-consent-card/div/div/div/app-button/button[contains(@class, 'btnSuccess')]")).Click();
                    Thread.Sleep(2000);
                    driver.FindElement(By.XPath("//div[contains(@class, 'left-wrapper flex-center clickable')]")).Click();
                    Thread.Sleep(4000);
                }
                Thread.Sleep(5000);
            }
            

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void giveConsent()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(link);

            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//input[@placeholder='Username']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("allan.casey@mail.com");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("12345678");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/app-root/div/app-login/div/div/form/div[2]/app-button[1]/button")).Click();

            Thread.Sleep(5000);

            IList<IWebElement> consentlist = driver.FindElements(By.XPath("//div[contains(@class, 'consent-card')]"));
            int consent_count = consentlist.Count();
            consent_count -= 1;
            Console.WriteLine("Consents: " + consent_count);

            if(consent_count >= 2)
            {
                driver.FindElement(By.XPath("//div[contains(@class, 'consent-list')]/div[2]/app-consent-card/div/div/div/app-button/button[contains(@class, 'btnSuccess')]")).Click();
                Thread.Sleep(2000);

                IWebElement giveConsent = driver.FindElement(By.XPath("//button[contains(@class, 'btnSuccess btnOutline')]"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView();", giveConsent);

                driver.FindElement(By.XPath("//div[contains(@class, 'checkbox flex-center cboxSuccess')]")).Click();
                Thread.Sleep(3000);

                driver.FindElement(By.XPath("//button[contains(@class, 'btnSuccess btnOutline')]")).Click();
                Thread.Sleep(3000);
            }
            else 
            {
                driver.Close();
                driver.Quit();
            }

           

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void checkGiven()
        {
            
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(link);

            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//input[@placeholder='Username']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("allan.casey@mail.com");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("12345678");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("//span[contains(@class,'font-bold') and contains(text(), 'Log in')]")).Click();

            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//span[text()='Given']")).Click();
            Thread.Sleep(2000);

            IList<IWebElement> consentlist = driver.FindElements(By.XPath("//button[contains(@class, 'btnOutline btnDark')]"));
            int consent_count = consentlist.Count();            
            System.Diagnostics.Debug.WriteLine("Consents: " + consent_count);

            if (consent_count == 0)
            {
                driver.Close();
                driver.Quit();
            }
            else if (consent_count == 1)
            {
                driver.FindElement(By.XPath("//button[contains(@class, 'btnOutline btnDark')]")).Click();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//div[contains(@class, 'left-wrapper flex-center clickable')]")).Click();
                Thread.Sleep(4000);                
            }
            else
            {
                for (int j = 1; j <= consent_count; j++)
                {
                    driver.FindElement(By.XPath("//div[contains(@class, 'consent-list')]/div[" + j + "]/app-consent-card/div/div/div[contains(@class, 'content-button-double flex-cross-center')]/span[2]/app-button/button[contains(@class, 'btnOutline btnDark')]")).Click();
                    Thread.Sleep(2000);
                    driver.FindElement(By.XPath("//div[contains(@class, 'left-wrapper flex-center clickable')]")).Click();
                    Thread.Sleep(4000);
                }
                Thread.Sleep(5000);
            }


            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void withdrawConsentDetail()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(link);

            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//input[@placeholder='Username']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("allan.casey@mail.com");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("12345678");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("//span[contains(@class,'font-bold') and contains(text(), 'Log in')]")).Click();

            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//span[text()='Given']")).Click();
            Thread.Sleep(2000);

            IList<IWebElement> consentlist = driver.FindElements(By.XPath("//button[contains(@class, 'btnOutline btnDark')]"));
            int consent_count = consentlist.Count();
            System.Diagnostics.Debug.WriteLine("Consents: " + consent_count);

            if (consent_count == 0)
            {
                driver.Close();
                driver.Quit();
            }
            else if (consent_count == 1)
            {
                driver.FindElement(By.XPath("//button[contains(@class, 'btnOutline btnDark')]")).Click();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//button[contains(@class, 'btnDanger btnOutline')]")).Click();
                Thread.Sleep(4000);
            }
            else
            {
                driver.FindElement(By.XPath("//div[contains(@class, 'consent-list')]/div[2]/app-consent-card/div/div/div[contains(@class, 'content-button-double flex-cross-center')]/span[2]/app-button/button[contains(@class, 'btnOutline btnDark')]")).Click();
                Thread.Sleep(2000);
                IWebElement withdrawConsent = driver.FindElement(By.XPath("//button[contains(@class, 'btnDanger btnOutline')]"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView();", withdrawConsent);
                driver.FindElement(By.XPath("//button[contains(@class, 'btnDanger btnOutline')]")).Click();
                Thread.Sleep(3000);
                driver.FindElement(By.XPath("//button[contains(@class, 'btnDanger btnOutline')]")).Click();
                Thread.Sleep(4000);
               
                Thread.Sleep(5000);
            }

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void withdrawConsent()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(link);

            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//input[@placeholder='Username']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("allan.casey@mail.com");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("12345678");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("//span[contains(@class,'font-bold') and contains(text(), 'Log in')]")).Click();

            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//span[text()='Given']")).Click();
            Thread.Sleep(4000);

            IList<IWebElement> consentlist = driver.FindElements(By.XPath("//button[contains(@class, 'btnOutline btnDark')]"));
            int consent_count = consentlist.Count();
            System.Diagnostics.Debug.WriteLine("Consents: " + consent_count);

            if (consent_count == 0)
            {
                driver.Close();
                driver.Quit();
            }
            else if (consent_count == 1)
            {
                driver.FindElement(By.XPath("//button[contains(@class, 'btnDanger')]")).Click();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//button[contains(@class, 'btnDanger btnOutline')]")).Click();
                Thread.Sleep(4000);
            }
            else
            {
                driver.FindElement(By.XPath("//div[contains(@class, 'consent-list')]/div[2]/app-consent-card/div/div/div[contains(@class, 'content-button-double flex-cross-center')]/span[1]/app-button/button[contains(@class, 'btnDanger')]")).Click();                
                Thread.Sleep(3000);
                driver.FindElement(By.XPath("//button[contains(@class, 'btnDanger btnOutline')]")).Click();
                Thread.Sleep(4000);                
            }

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void checkWithdrawn()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(link);

            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//input[@placeholder='Username']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("allan.casey@mail.com");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("12345678");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[contains(@class,'font-bold') and contains(text(), 'Log in')]")).Click();

            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//span[text()='Withdrawn']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            IList<IWebElement> consentlist = driver.FindElements(By.XPath("//button[contains(@class, 'btnOutline btnDark')]"));
            int consent_count = consentlist.Count();
            System.Diagnostics.Debug.WriteLine("Consents: " + consent_count);

            if (consent_count == 0)
            {
                driver.Close();
                driver.Quit();
            }
            else if (consent_count == 1)
            {
                driver.FindElement(By.XPath("//button[contains(@class, 'btnOutline btnDark')]")).Click();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//div[contains(@class, 'left-wrapper flex-center clickable')]")).Click();
                Thread.Sleep(4000);
            }
            else
            {
                for (int j = 1; j <= consent_count; j++)
                {
                    driver.FindElement(By.XPath("//div[contains(@class, 'consent-list')]/div[" + j + "]/app-consent-card/div/div/div[contains(@class, 'content-button-right flex-main-end')]/app-button/button[contains(@class, 'btnOutline btnDark')]")).Click();
                    Thread.Sleep(2000);
                    driver.FindElement(By.XPath("//div[contains(@class, 'left-wrapper flex-center clickable')]")).Click();
                    Thread.Sleep(4000);
                }
                Thread.Sleep(5000);
            }

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void changeLanguage()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(link);

            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//input[@placeholder='Username']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("allan.casey@mail.com");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("12345678");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("//span[contains(@class,'font-bold') and contains(text(), 'Log in')]")).Click();

            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//div[contains(@class, 'settings-wrapper clickable flex-center')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            driver.FindElement(By.XPath("//div[contains(@class, 'main-container clickable')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            //Finnish Language
            driver.FindElement(By.XPath("//div/span[contains(text(), 'Finnish')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.FindElement(By.XPath("//button[contains(@class, 'btnOutline btnDark')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.FindElement(By.XPath("//div[contains(@class, 'left-wrapper flex-center clickable')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.FindElement(By.XPath("//span[text()='Pyynnöt']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("//span[text()='Annettu']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("//span[text()='Peruutettu']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.FindElement(By.XPath("//div[contains(@class, 'settings-wrapper clickable flex-center')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            driver.FindElement(By.XPath("//div[contains(@class, 'main-container clickable')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            //Norwegian
            driver.FindElement(By.XPath("//div[contains(@class, 'dropdown-item clickable flex-cross-center ng-star-inserted')]/span[contains(text(), 'Norwegian')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.FindElement(By.XPath("//button[contains(@class, 'btnOutline btnDark')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.FindElement(By.XPath("//div[contains(@class, 'left-wrapper flex-center clickable')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.FindElement(By.XPath("//span[text()='Forespørsler']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("//span[text()='Gitt']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("//span[text()='Trukket tilbake']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.FindElement(By.XPath("//div[contains(@class, 'settings-wrapper clickable flex-center')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            driver.FindElement(By.XPath("//div[contains(@class, 'main-container clickable')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            //Swedish
            driver.FindElement(By.XPath("//div[contains(@class, 'dropdown-item clickable flex-cross-center ng-star-inserted')]/span[contains(text(), 'Swedish')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.FindElement(By.XPath("//button[contains(@class, 'btnOutline btnDark')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.FindElement(By.XPath("//div[contains(@class, 'left-wrapper flex-center clickable')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.FindElement(By.XPath("//span[text()='Förfrågningar']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("//span[text()='Avgivet']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("//span[text()='Återtaget']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.FindElement(By.XPath("//div[contains(@class, 'settings-wrapper clickable flex-center')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            driver.FindElement(By.XPath("//div[contains(@class, 'main-container clickable')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            //English
            driver.FindElement(By.XPath("//div[contains(@class, 'dropdown-item clickable flex-cross-center ng-star-inserted')]/span[contains(text(), 'english')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.FindElement(By.XPath("//button[contains(@class, 'btnOutline btnDark')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.FindElement(By.XPath("//div[contains(@class, 'left-wrapper flex-center clickable')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void changeContact()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(link);

            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//input[@placeholder='Username']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Username']")).SendKeys("allan.casey@mail.com");
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).Click();
            driver.FindElement(By.XPath("//input[@placeholder='Password']")).SendKeys("12345678");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("//span[contains(@class,'font-bold') and contains(text(), 'Log in')]")).Click();

            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//div[contains(@class, 'settings-wrapper clickable flex-center')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            driver.FindElement(By.XPath("//input[contains(@type, 'tel')]")).SendKeys("99924");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.FindElement(By.XPath("//button[contains(@class, 'btnOutline btnDark')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.Close();
            driver.Quit();
        }

        public void email_send()
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("rhobert.odoya@netzon.com.se");
            mail.To.Add("rhobert.odoya@netzon.com.se");
            mail.Subject = "Test Report";
            mail.Body = "mail with attachment";

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("rhobert.odoya@netzon.com.se", "dallasfuel24");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
    }
}
