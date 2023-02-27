using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Final.Project.POMClasses
{
    internal class RegisterNewUserPOM
    {
        private IWebDriver driver;

        //Declaring Environment variables and Test Parameters to be used from run settings
        string emailAddress = Environment.GetEnvironmentVariable("user_email_address");      //User's email address and password is located in the environment variable in the mysettings.runsettings
        string password = Environment.GetEnvironmentVariable("user_password");

        public RegisterNewUserPOM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        public IWebElement MyAccount => driver.FindElement(By.LinkText("My account"));
        public IWebElement EmailAddress => driver.FindElement(By.CssSelector("#reg_email"));
        public IWebElement Password => driver.FindElement(By.CssSelector("#reg_password"));
        public IWebElement Enter => driver.FindElement(By.CssSelector("#customer_login > div.u-column2.col-2 > form > p:nth-child(4) > button"));
   
        //Service Method
        public RegisterNewUserPOM GoMyAccount()
        {
            MyAccount.Click();
            return this;
        }
        public RegisterNewUserPOM SetEmailAddress()
        {
            EmailAddress.SendKeys(emailAddress);
            return this;
        }
        public RegisterNewUserPOM SetPassword()
        {
            Password.Clear();
            Password.SendKeys(password);
            return this;
        }
        public RegisterNewUserPOM GoEnter()
        {
            Enter.Submit();
            return this;
        }

        //Helper
        public bool RegisterNewUserExpectSuccess()
        {
            GoMyAccount();
            SetEmailAddress();
            SetPassword();
            GoEnter();

            try
            {
                driver.FindElement(By.ClassName("entry-header"));                    
                Console.WriteLine("New User registered successfully");
                return true;
            }
            catch(Exception)
            {
                driver.FindElement(By.ClassName("woocommerce"));                    //Catching the alert at the top that displays the user has already been registered - check with new email address
                Console.WriteLine("Existing User already registered");
            }
            return false;
        }


    }
}
