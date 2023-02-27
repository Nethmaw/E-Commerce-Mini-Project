using OpenQA.Selenium;
using static e_Commerce.Final.Project.Utilities.StaticHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Final.Project.POMClasses
{
    internal class SelectItemOfClothingPOM
    {

        private IWebDriver driver;

        public SelectItemOfClothingPOM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        public IWebElement ItemOfClothing => driver.FindElement(By.CssSelector("[data-product_id='28']"));
        public IWebElement ItemOfClothingTwo => driver.FindElement(By.CssSelector("[data-product_id='32']"));
        public IWebElement ViewCart => driver.FindElement(By.CssSelector("[title='View cart']"));


        //Service Method
        public SelectItemOfClothingPOM ClickItemOfClothing(string locatorName)
        {
            switch (locatorName)
            {
                case "locator1":
                    ItemOfClothing.Click();
                    break;
                case "locator2":
                    ItemOfClothingTwo.Click();
                    break;
            }
            return this;
        }
        public SelectItemOfClothingPOM ClickViewCart()
        {
            ViewCart.Click();
            return this;
        }
    }
}
