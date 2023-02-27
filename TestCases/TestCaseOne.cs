using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support;
using NUnit.Framework;
using e_Commerce.Final.Project.Utilities;
using static e_Commerce.Final.Project.Utilities.StaticHelpers;
using System.Net.Mail;
using System;
using OpenQA.Selenium.Interactions;
using System.Data;
using System.Globalization;
using e_Commerce.Final.Project.POMClasses;
using System.Transactions;


namespace e_Commerce.Final.Project
{
    internal class TestCaseOne : Utilities.BaseTest
    {

        
        [Test]
        public void TestCase1()
        {
            //Declaring Environment variables and Test Parameters to be used from run settings
            string browser = TestContext.Parameters["browser"];                                     //Browser and url variable is located in the test run parameters in the mysettings.runsettings
            string url = TestContext.Parameters["url"];

            //Registering a New User with associated POM class                    
            driver.Url = url;
            RegisterNewUserPOM newUser = new RegisterNewUserPOM(driver);
            newUser.GoMyAccount().SetEmailAddress().SetPassword().GoEnter();
            MyHelpers help = new MyHelpers(driver);                                                 //Use of an instance helper class
            help.WaitForElement(By.LinkText("My account"), 3);
            bool didRegisterNewUser = newUser.RegisterNewUserExpectSuccess();
            Assert.That(didRegisterNewUser, Is.True, "Failed to register New User");
            Thread.Sleep(3000);

            //Navigate to the homepage
            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/";

            //Dismiss the bottom warning
            driver.FindElement(By.CssSelector("body > p > a")).Click();

            //Log in as a Registered User with associated POM class
            Console.WriteLine("Attempt to login a registered user");
            LogInPOM existingUser = new LogInPOM(driver);
            existingUser.GoMyAccount().SetUsername().SetPassword().GoLogIn();
            WaitForElement(By.CssSelector(".entry-title"), 3, driver);
            string headingText = driver.FindElement(By.CssSelector(".entry-title")).Text;
            Assert.IsTrue(headingText == "My account", "User not logged in");
            Console.WriteLine("Login Success");
                                                                             
            //Entering the Shop
            driver.FindElement(By.LinkText("Shop")).Click();
            driver.FindElement(By.CssSelector("html")).Click();
            WaitForElement(By.CssSelector("html"), 3, driver);
            Console.WriteLine("On the Shop page");

            //Selecting an item of clothing to be added to the Cart with associated POM class
            Console.WriteLine("Starting search for an item of clothing");
            ScrollDown(driver, 300);                                                                //Use of static helper to scroll down
            Thread.Sleep(2000);                                                                     
            Console.WriteLine("Scrolling down to view the list of products");
            SelectItemOfClothingPOM clothingItem = new SelectItemOfClothingPOM(driver);
            clothingItem.ClickItemOfClothing("locator1").ClickViewCart();
            WaitForElement(By.CssSelector(".entry-title"), 3, driver);
            string titleText = driver.FindElement(By.CssSelector(".entry-title")).Text;
            Assert.IsTrue(titleText == "Cart", "User has not viewed cart");
            Console.WriteLine("The selected item of clothing (Belt) has been added to the Cart");

            //Applying the coupon code
            ScrollDown(driver, 200);
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("#coupon_code")).SendKeys("edgewords");
            driver.FindElement(By.CssSelector("#post-5 > div > div > form > table > tbody > tr:nth-child(2) > td > div > button")).Submit();
            Thread.Sleep(3000);
            Console.WriteLine("The coupon has been applied");

            //Calculating the correct discount
            string couponDiscount = driver.FindElement(By.CssSelector("[data-title='Coupon\\: edgewords'] .woocommerce-Price-amount")).Text;
            string subtotal = driver.FindElement(By.CssSelector(".product-subtotal  bdi")).Text;
            char[] charsToTrim = { '£', '.' };
            string cleanSubtotal = subtotal.Trim(charsToTrim);                                                                      //Use of trim string method and sub string method to extract number from the string
            int startIndex = 0;
            int length = 2;
            string substring = cleanSubtotal.Substring(startIndex, length);
            Console.WriteLine(substring);
            decimal decimalValue = 0.15m;
            decimal result = decimalValue * int.Parse(substring);
            string resultText = result.ToString();
            Console.WriteLine("Actual discount calculated should be " + resultText);

            //Validating the correct discount has been applied
            Assert.That(couponDiscount == "£" + resultText, "Correct discount has not been applied");                         
            try
            {
                Assert.That(couponDiscount, Is.EqualTo("£" + resultText).IgnoreCase, "Correct discount has not been applied");
                Console.WriteLine("The correct discount has been applied - £8.25 has been deducted");
            }
            catch (AssertionException) 
            {
                Assert.That(couponDiscount, Is.EqualTo("£5.50").IgnoreCase, "Correct discount has been applied");
                Console.WriteLine("The correct discount has not been applied - amount deducted should state £8.50, rather than £5.50");

            }

            //Verifying total amount calculated after coupon and shipping is correct
            ScrollDown(driver, 300);
            string total = driver.FindElement(By.CssSelector("strong > .amount.woocommerce-Price-amount > bdi")).Text;
            Assert.That(total == "£50.70", "Total amount charged is wrong");
            try
            {
                Assert.That(total, Is.EqualTo("£50.70").IgnoreCase, "Total amount charged is wrong");
                Console.WriteLine("The total amount charged (including shipping costs and coupon discount) is correct");
            }
            catch
            {
                Console.WriteLine("Correct total has not been calculated");
            }
            Thread.Sleep(3000);

            //Log Out
            ScrollDown(driver, 300);
            LogOutPOM logOut = new LogOutPOM(driver);
            logOut.GoMyAccount().ClickLogOut();
            Thread.Sleep(3000);
            Console.WriteLine("Successfully logged out");

            //Clear the cart
            existingUser.GoMyAccount().SetUsername().SetPassword().GoLogIn();
            ClearCartPOM clear = new ClearCartPOM(driver);
            clear.GoCart().GoRemoveItem();
            Thread.Sleep(3000);
            Console.WriteLine("Cart is cleared");
        } 
    }
}
         