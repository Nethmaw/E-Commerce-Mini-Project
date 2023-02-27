using e_Commerce.Final.Project.POMClasses;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static e_Commerce.Final.Project.Utilities.StaticHelpers;

namespace e_Commerce.Final.Project.TestCases
{
    internal class TestCaseTwo : Utilities.BaseTest
    {
        [Test]
        public void TestCase2()
        {
            //Navigate to the homepage
            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/";

            //Dismiss the bottom warning
            driver.FindElement(By.CssSelector("body > p > a")).Click();

            //Log in as a Registered User with associated POM class
            Console.WriteLine("Attempt to login a registered user");
            LogInPOM existingUser = new LogInPOM(driver);
            existingUser.GoMyAccount().SetUsername().SetPassword().GoLogIn();
            WaitForElement(By.CssSelector(".entry-title"), 3, driver);                                      //Use of static helper
            string headingText = driver.FindElement(By.CssSelector(".entry-title")).Text;
            Assert.IsTrue(headingText == "My account", "User not logged in");
            //headingText.Should().Contain("My account");                                   //Fluent assertion?
            Console.WriteLine("Login Success");

            //Entering the Shop
            driver.FindElement(By.LinkText("Shop")).Click();
            driver.FindElement(By.CssSelector("html")).Click();
            WaitForElement(By.CssSelector("html"), 3, driver);

            //Selecting an item of clothing to be added to the Cart with associated POM class
            Console.WriteLine("Starting search for an item of clothing");
            ScrollDown(driver, 1000);
            Console.WriteLine("Scrolling down to view the list of products");
            SelectItemOfClothingPOM clothingItem = new SelectItemOfClothingPOM(driver);
            clothingItem.ClickItemOfClothing("locator2").ClickViewCart();
            WaitForElement(By.CssSelector(".entry-title"), 3, driver);
            string cartText = driver.FindElement(By.CssSelector(".entry-title")).Text;
            Assert.IsTrue(cartText == "Cart", "User has not proceeded to cart");
            Console.WriteLine("The selected item of clothing (Hoodie with Pocket) has been added to the Cart");

            //Navigating to checkout
            ScrollDown(driver, 600);
            driver.FindElement(By.LinkText("Proceed to checkout")).Click();
            Thread.Sleep(3000);
            Console.WriteLine("Checkout page is displayed");

            //Inputting Billing details with associated POM class
            InputBillingDetailsPOM billingDetails = new InputBillingDetailsPOM(driver);
            billingDetails.ClickFirstName().ClickLastName().ClickCompanyName().ClickStreetAddress().ClickHouseUnit().ClickTownCity().ClickCounty().ClickPostcode().ClickPhone().ClickCountryRegion();
            WaitForElement(By.CssSelector("#post-6 > div > div"), 3, driver);
            Console.WriteLine("All billing details have been correctly inputted");

            //Selecting payment method
            ScrollDown(driver, 600);
            driver.FindElement(By.CssSelector("#payment > ul > li.wc_payment_method.payment_method_cheque > label")).Click();
            Console.WriteLine("User has selected preferred payment method - 'check payments'");

            //Placing the order
            driver.FindElement(By.CssSelector("#place_order")).Submit();
            Thread.Sleep(3000);
            Console.WriteLine("Order has successfully been placed");

            //Capturing order number
            string orderNumber = driver.FindElement(By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__order.order > strong")).Text;
            Console.WriteLine("Order number is : " + orderNumber);
            Thread.Sleep(3000);

            //Screenshot of placed order
            TakeScreenshotOfElement(driver, By.Id("primary"), "Screenshot-OrderConfirmationPage.png");          //Use of static helper

            //Check order is displayed in My account
            driver.FindElement(By.LinkText("My account")).Click();
            driver.FindElement(By.CssSelector(".woocommerce-MyAccount-navigation-link--orders [href]")).Click();
            string displayedOrderNumber = driver.FindElement(By.CssSelector("#post-7 > div > div > div > table > tbody > tr:nth-child(1) > td.woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a")).Text;
            Assert.That(displayedOrderNumber == "#" + orderNumber, "Order number not placed");
            WaitForElement(By.CssSelector("#post-7 > div > div > div > table"), 3, driver);
            Console.WriteLine("Order number is correctly displayed in the orders page under My account");

            //Logout with associated POM class
            LogOutPOM logOut = new LogOutPOM(driver);
            logOut.GoMyAccount().ClickLogOut();
            WaitForElement(By.CssSelector("#post-7 > header > h1"), 3, driver);
            Console.WriteLine("User has successfully logged out");
        }
    }
}

