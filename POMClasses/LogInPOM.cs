using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Final.Project.POMClasses
{
    internal class LogInPOM
    {

            private IWebDriver driver;

            //Declaring Environment variables and Test Parameters to be used from run settings
            string emailAddress = Environment.GetEnvironmentVariable("user_email_address");      //User's email address and password is located in the environment variable in the mysettings.runsettings
            string password = Environment.GetEnvironmentVariable("user_password");

            public LogInPOM(IWebDriver driver)
            {
                this.driver = driver;
            }

            //Locators
            public IWebElement MyAccount => driver.FindElement(By.LinkText("My account"));
            public IWebElement Username => driver.FindElement(By.CssSelector("#username"));
            public IWebElement Password => driver.FindElement(By.CssSelector("#password"));
            public IWebElement LogIn => driver.FindElement(By.Name("login"));


            //Service Method
            public LogInPOM GoMyAccount()
            {
                MyAccount.Click();
                return this;
            }
            public LogInPOM SetUsername()
            {
                Username.SendKeys(emailAddress);
                return this;
            }
            public LogInPOM SetPassword()
            {
                Password.Clear();
                Password.SendKeys(password);
                return this;
            }
            public LogInPOM GoLogIn()
            {
                LogIn.Submit();
                return this;
            }
        }
}
