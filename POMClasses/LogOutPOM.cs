using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Final.Project.POMClasses
{
    internal class LogOutPOM
    {
        private IWebDriver driver;

        
        public LogOutPOM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        public IWebElement MyAccount => driver.FindElement(By.LinkText("My account"));
        public IWebElement LogOut => driver.FindElement(By.LinkText("Log out"));

        //Service Method
        public LogOutPOM GoMyAccount()
        {
            MyAccount.Click();
            return this;
        }
        public LogOutPOM ClickLogOut()
        {
            LogOut.Click();
            return this;
        }
        
    }
}
