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
                try
                {
                    for (int k = 1; k <= 10; k++)
                    {
                        driver.FindElement(By.XPath("//div[contains(@class,'body-container flex')]/app-tr[" + k + "]/div/div/div/app-tc[3]/div")).Click();
                        Thread.Sleep(3000);
                        driver.FindElement(By.XPath("//div[contains(@class,'left-content flex-cross-center clickable faded')]/div")).Click();
                        Thread.Sleep(3000);
                    }
                }
                catch (NoSuchElementException)
                {

                }                
                try
                {
                    driver.FindElement(By.XPath("//div[contains(@class,'pagination-container flex-main-end ng-star-inserted')]"));
                    Thread.Sleep(2000);
                    for (int j = 1; j <= wholeNum; j++)
                    {
                        driver.FindElement(By.XPath("//ul[contains(@class,'ngx-pagination ng-star-inserted')]/li[contains(@class,'pagination-next ng-star-inserted')]")).Click();
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                    }
                }
                catch (NoSuchElementException)
                {

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
                    driver.FindElement(By.XPath("//div[contains(@class,'body-container flex')]/app-tr["+ j +"]/div/div/div/app-tc[4]/div")).Click();
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

            Random rnd = new Random();
            int rndNum = rnd.Next(10000);

            var rndfirstName = new List<string> {"Mark", "Carlo", "Kim", "Ashe" };
            var rndlastName = new List<string> {"Larned", "Shu", "Cartwright", "Ketchum" };

            int index = rnd.Next(rndfirstName.Count);
            int index2 = rnd.Next(rndlastName.Count);

            var firstName = rndfirstName[index];
            rndfirstName.RemoveAt(index);
            var lastName = rndlastName[index];
            rndlastName.RemoveAt(index);

            driver.FindElement(By.XPath("//app-input[contains(@formcontrolname, 'id_number')]/div/input")).SendKeys("" + rndNum);            
            driver.FindElement(By.XPath("//app-input[contains(@formcontrolname, 'first_name')]/div/input")).SendKeys("" + firstName);
            driver.FindElement(By.XPath("//app-input[contains(@formcontrolname, 'middle_name')]/div/input")).SendKeys("B.");
            driver.FindElement(By.XPath("//app-input[contains(@formcontrolname, 'last_name')]/div/input")).SendKeys("" + lastName);
            driver.FindElement(By.XPath("//app-input[contains(@formcontrolname, 'email_address')]/div/input")).SendKeys(""+ firstName + lastName + rndNum +"@mail.com");
            driver.FindElement(By.XPath("//app-input[contains(@formcontrolname, 'mobile_number')]/div/input")).SendKeys("987654321");

            driver.FindElement(By.XPath("//button[contains(@class,'label btnPrimary')]")).Click();
            Thread.Sleep(2000);

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void adminChangeLanguage()
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

            driver.FindElement(By.XPath("//div[contains(@class, 'nav-items flex-main-start')]/div[3]")).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//div[contains(@class,'main-container clickable')]/div")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//span[contains(text(),'Finnish')]")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//div[contains(@class,'body-wrapper')]/div[1]/div[contains(@class,'content-container ng-star-inserted')]/div/div[contains(@class,'button-container flex-main-end')]/app-button")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//div[contains(@class,'main-container clickable')]/div")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//span[contains(text(),'Norja')]")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//div[contains(@class,'body-wrapper')]/div[1]/div[contains(@class,'content-container ng-star-inserted')]/div/div[contains(@class,'button-container flex-main-end')]/app-button")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//div[contains(@class,'main-container clickable')]/div")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//span[contains(text(),'Svensk')]")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//div[contains(@class,'body-wrapper')]/div[1]/div[contains(@class,'content-container ng-star-inserted')]/div/div[contains(@class,'button-container flex-main-end')]/app-button")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//div[contains(@class,'main-container clickable')]/div")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//span[contains(text(),'Engelska')]")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//div[contains(@class,'body-wrapper')]/div[1]/div[contains(@class,'content-container ng-star-inserted')]/div/div[contains(@class,'button-container flex-main-end')]/app-button")).Click();
            Thread.Sleep(2000);

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void checkHealthGroups()
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

            driver.FindElement(By.XPath("//div[contains(@class,'profile-action clickable flex-center')]/i[contains(@class,'material-icons')]")).Click();
            Thread.Sleep(1000);

            IList<IWebElement> consentlist = driver.FindElements(By.XPath("//div[contains(@class,'dropdown-item clickable flex-cross-center ng-star-inserted')]"));
            int consent_count = consentlist.Count();          
            System.Diagnostics.Debug.WriteLine("Consents: " + consent_count);
            Thread.Sleep(2000);

            for (int j = 1; j <= consent_count; j++)
            {
                
                driver.FindElement(By.XPath("//div[contains(@class,'dropdown-container')]/div["+ j +"]")).Click();
                Thread.Sleep(5000);
                driver.FindElement(By.XPath("//div[contains(@class,'profile-action clickable flex-center')]/i[contains(@class,'material-icons')]")).Click();
                Thread.Sleep(1000);
            }

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void createConsent()
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

            driver.FindElement(By.XPath("//div[contains(@class,'right-content flex-cross-center clickable faded')]/div[2]")).Click();
            Thread.Sleep(2000);

            Random rnd = new Random();
            int rndName = rnd.Next(1000);

            driver.FindElement(By.XPath("//app-input[contains(@formcontrolname,'name')]/div/input")).SendKeys("Consent" + rndName);
            driver.FindElement(By.XPath("//app-textarea[contains(@formcontrolname,'terms')]/div/textarea")).SendKeys("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam rutrum, dui at pretium convallis, neque tellus elementum nisi, nec tincidunt nibh purus eget libero. Nulla mollis, justo sed accumsan elementum, quam odio commodo purus, sodales ornare diam turpis vel nisl. In leo nunc, pellentesque non cursus eu, bibendum vitae felis. Aenean sit amet aliquet nisi. Cras tempus tellus sed viverra luctus. Nullam mollis neque vitae elit tincidunt molestie. Proin nisl ante, convallis vitae dictum et, mollis sed massa. Fusce laoreet tellus et ante accumsan pharetra.\nNulla sit amet dolor pellentesque, consectetur ipsum a, ullamcorper sem. Sed in est eu massa aliquam aliquet. Integer non pulvinar urna, eu ullamcorper libero. Sed ac justo metus. Vestibulum placerat lorem sapien, a viverra diam euismod nec. Maecenas rhoncus, purus in sodales tincidunt, ante ipsum venenatis est, non lacinia leo nunc vel ligula. Phasellus rhoncus nec mi nec pulvinar. Nulla suscipit erat tincidunt est aliquet molestie. Donec auctor vel lacus ut tristique. Praesent sit amet urna sit amet mauris lacinia mollis sed quis lacus. Suspendisse congue eu nulla vel pretium. Lorem ipsum dolor sit amet, consectetur adipiscing elit.\nDuis convallis sed nisl eu auctor. Morbi commodo maximus vulputate. Quisque sollicitudin finibus nunc, in cursus risus dapibus vel. Vivamus vel felis eget sem gravida rutrum. Donec sed luctus odio, at vestibulum lacus. Nunc lacinia nibh vitae tortor accumsan dapibus volutpat vel dui. Sed a nisl vehicula purus placerat vestibulum. Pellentesque vestibulum urna sed nunc pellentesque, quis fringilla quam lacinia. Aliquam lacinia aliquet aliquam.\nPraesent quis magna vitae purus convallis viverra eget quis erat. Donec at fringilla ante. In egestas fermentum auctor. Fusce sodales risus a neque sollicitudin, nec laoreet lacus facilisis. Donec consectetur dui eget tortor rutrum dignissim. Aliquam vel mauris non justo egestas placerat sed sed sapien. Morbi varius nunc ullamcorper, mollis lectus ac, elementum enim. Suspendisse potenti. Quisque ac ultrices tortor. Aenean lorem sapien, pharetra sed tempus nec, vestibulum eu massa. Phasellus eget mauris egestas nunc faucibus lobortis. Vestibulum euismod erat commodo dictum venenatis. Fusce massa justo, pulvinar sit amet tincidunt ac, ornare quis sapien. Vestibulum aliquet, lectus vitae tincidunt dapibus, enim lacus rhoncus risus, ut eleifend elit massa id arcu. Quisque sit amet nisi ut erat fermentum gravida.\nPraesent laoreet ultricies tincidunt. In sagittis accumsan placerat. Pellentesque iaculis, purus vel laoreet dapibus, leo enim tristique nisl, sed tincidunt urna dolor dignissim nulla. Integer ac turpis leo. Nam at quam tortor. Donec neque sem, suscipit et sapien et, volutpat dignissim neque. Aliquam erat volutpat. Integer egestas consectetur tortor, ut scelerisque eros commodo in. Fusce hendrerit et erat quis bibendum. Aliquam consectetur nisl ut nisl hendrerit, at dictum magna malesuada. Sed lacinia sapien rhoncus gravida eleifend. Aliquam nibh eros, auctor in nisl a, tincidunt venenatis est. Etiam sit amet mauris nisi. Nunc nec lacus at sapien rutrum faucibus. Praesent rutrum in est sit amet fringilla. Vestibulum sem libero, iaculis finibus felis eget, porta viverra mi.");
            driver.FindElement(By.XPath("//div[contains(@formarrayname,'checkbox')]/div/app-input[contains(@formcontrolname,'name')]/div/input")).SendKeys("Lorem ipsum dolor sit amet.");
            driver.FindElement(By.XPath("//app-datepicker/div/span")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//div[contains(@class,'mat-calendar-body-cell-content mat-calendar-body-today')]")).Click();
            driver.FindElement(By.XPath("//app-input[contains(@formcontrolname,'push_text')]/div/input")).SendKeys("Lorem ipsum dolor sit amet.");
            driver.FindElement(By.XPath("//button[contains(@type,'submit')]")).Click();

            Thread.Sleep(2000);
            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void addConsentToPatient()
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
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//div[contains(@class,'body-container flex')]/app-tr[1]/div/div/div/app-tc[4]/div")).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//div[contains(@class,'actions-wrapper tiny')]/div[2]/div[2]")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//div[contains(@class,'main-container clickable')]")).Click();
            Thread.Sleep(1000);

            IList<IWebElement> consentlist = driver.FindElements(By.XPath("//app-dropdown/div/div/div"));
            int consent_count = consentlist.Count();
            System.Diagnostics.Debug.WriteLine("Consents: " + consent_count);
            Thread.Sleep(2000);

            for (int j=1; j <= consent_count; j++)
            {
                driver.FindElement(By.XPath("//app-dropdown/div/div[contains(@class,'dropdown-container sublabel')]/div[" +j+"]")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//button[contains(@class,'label btnPrimary')]")).Click();
                Thread.Sleep(2000);
               
                try
                {
                    driver.FindElement(By.XPath("//div[contains(text(),'Consent already added to the patient.')]"));
                    Thread.Sleep(2000);
                }
                catch (NoSuchElementException)
                {
                    driver.FindElement(By.XPath("//div[contains(@class,'left-content flex-cross-center clickable faded')]")).Click();
                    break;
                }
                finally
                {
                    driver.FindElement(By.XPath("//div[contains(@class,'main-container clickable')]")).Click();
                    Thread.Sleep(3000);
                }
            }

            driver.Close();
            driver.Quit();
        }
                 
        [TestMethod]
        public void inactivateConsent()
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
            System.Diagnostics.Debug.WriteLine("Consents: " + consent_count);

            Random rnd = new Random();
            int rndName = rnd.Next(10);

            driver.FindElement(By.XPath("//div[contains(@class,'body-container flex')]/app-tr[" + rndName + "]/div/div/div/app-tc[3]/div")).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//div[contains(@class,'right-content flex-cross-center clickable faded ng-star-inserted')]/div[2]")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//input[contains(@type,'text')]")).SendKeys("Lorem ipsum dolor sit amet.");
            driver.FindElement(By.XPath("//div[contains(@class,'checkbox flex-center cboxDanger')]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//app-button/button[contains(@class,'label btnDanger')]")).Click();
            Thread.Sleep(2000);

            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void checkInactivated()
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

            driver.FindElement(By.XPath("//div[contains(@class,'right-content flex-cross-center clickable faded')]/div[1]")).Click();
            Thread.Sleep(2000);

            IList<IWebElement> consentlist = driver.FindElements(By.XPath("//div[contains(@class,'body-container flex')]/app-tr/div/div/div/app-tc[3]/div"));
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
                int wholeNum = consent_count / 10;
                for (int k = 1; k <= 10; k++)
                {
                    try
                    {
                        driver.FindElement(By.XPath("//div[contains(@class,'body-container flex')]/app-tr[" + k + "]/div/div/div/app-tc[3]/div")).Click();
                        Thread.Sleep(3000);
                        driver.FindElement(By.XPath("//div[contains(@class,'left-content flex-cross-center clickable faded')]/div")).Click();
                        Thread.Sleep(3000);
                        driver.FindElement(By.XPath("//div[contains(@class,'right-content flex-cross-center clickable faded')]/div[1]")).Click();
                        Thread.Sleep(4000);
                    }
                    catch (NoSuchElementException)
                    {

                    }
                }
                try
                {
                    driver.FindElement(By.XPath("//div[contains(@class,'pagination-container flex-main-end ng-star-inserted')]"));
                    Thread.Sleep(2000);
                    for (int j = 1; j <= wholeNum; j++)
                    {
                        driver.FindElement(By.XPath("//ul[contains(@class,'ngx-pagination ng-star-inserted')]/li[contains(@class,'pagination-next ng-star-inserted')]")).Click();
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                    }
                }
                catch (NoSuchElementException)
                {

                }                
                driver.Close();
                driver.Quit();
            }
        }

        [TestMethod]
        public void searchPatient()
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

            Random rnd = new Random();
            var rndfirstName = new List<string> { "Mark", "Carlo", "Kim", "Ashe" };
            var rndlastName = new List<string> { "Larned", "Shu", "Cartwright", "Ketchum" };

            int index = rnd.Next(rndfirstName.Count);
            int index2 = rnd.Next(rndlastName.Count);

            var firstName = rndfirstName[index];
            rndfirstName.RemoveAt(index);            

            driver.FindElement(By.XPath("//div[contains(@class,'search-container date-to')]/app-searchbar/div/div[2]/input")).SendKeys(""+firstName);
            Thread.Sleep(2000);

            IList<IWebElement> consentlist = driver.FindElements(By.XPath("//app-tc[contains(@class,'email-cell flex')]"));
            int consent_count = consentlist.Count();
            System.Diagnostics.Debug.WriteLine("Consents: " + consent_count);

            if (consent_count > 0)
            {
                driver.FindElement(By.XPath("//div[contains(@class,'body-container flex')]/app-tr/div/div/div/app-tc[4]/div")).Click();
                Thread.Sleep(5000);                
            }
            driver.Close();
            driver.Quit();
        }
    }
}
