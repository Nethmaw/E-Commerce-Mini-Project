using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Final.Project.POMClasses
{
    internal class ClearCartPOM
    {
        private IWebDriver driver;

        public ClearCartPOM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        public IWebElement Cart => driver.FindElement(By.LinkText("Cart"));
        public IWebElement RemoveItem => driver.FindElement(By.ClassName("remove"));

        //Service Method
        public ClearCartPOM GoCart()
        {
            Cart.Click();
            return this;
        }
        public ClearCartPOM GoRemoveItem()
        {
            RemoveItem.Click();
            return this;
        }
  
    }
}
