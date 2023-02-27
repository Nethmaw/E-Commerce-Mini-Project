using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;

namespace e_Commerce.Final.Project.Utilities
{
    public class BaseTest
    {
        protected IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            //Declaring Environment variables and Test Parameters to be used from run settings
            string browser = TestContext.Parameters["browser"];                                     //Browser and url variable is located in the test run parameters in the mysettings.runsettings
            string url = TestContext.Parameters["url"];

            //Run parameters from the command line (in command prompt)
            browser = TestContext.Parameters["browser"];                                            //To run test in cmd, type in: dotnet test --results-directory "Results" --logger:trx --settings Utilities\mysettings.runsettings         
            browser = browser.ToLower().Trim();                                                     //To run test in developer powershell, type in: vstest.console.exe e-Commerce.Final.Project\e-Commerce.Final.Project\bin\Debug\net6.0\e-Commerce.Final.Project.dll /Settings:e-Commerce.Final.Project\e-Commerce.Final.Project\Utilities\mysettings.runsettings /Logger:trx
            Console.WriteLine("Tests reports produced when ran in cmd and developer powershell");   //See reports pages 

            //Choosing the browser
            switch (browser)
            {
                case "firefox":

                    //Locating Firefox browser binary location and maximising screen
                    FirefoxOptions optionf = new FirefoxOptions();
                    optionf.BrowserExecutableLocation = "C:\\Users\\NethmaWimalasuriya\\AppData\\Local\\Mozilla Firefox\\firefox.exe";
                    driver = new FirefoxDriver(optionf);
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.Manage().Window.Maximize();
                    Console.WriteLine("Firefox Browser has opened");
                    break;

                case "chrome":

                    //Maximising Chrome screen when launching
                    ChromeOptions option = new ChromeOptions();
                    option.AddArgument("start-maximized");
                    driver = new ChromeDriver(option);
                    Console.WriteLine("Chrome browser has opened");
                    break;

                default:

                    //Setting unknown browser to be a chrome browser
                    Console.WriteLine("Unknown browser - starting chrome");
                    ChromeOptions optionm = new ChromeOptions();
                    optionm.AddArgument("start-maximized");
                    driver = new ChromeDriver(optionm);
                    break;
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
