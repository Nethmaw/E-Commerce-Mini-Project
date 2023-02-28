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

            //Registering a New User with RegisterNewUser POM class                    
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

            //Log in as a Registered User with LogIn POM class
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
            Console.WriteLine("Successfully navigated to the Shop page");

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
            Console.WriteLine("The coupon code (edgewords) has been applied");

            //Calculating the correct discount
            string couponDiscount = driver.FindElement(By.CssSelector("[data-title='Coupon\\: edgewords'] .woocommerce-Price-amount")).Text;
            string subtotal = driver.FindElement(By.CssSelector(".product-subtotal  bdi")).Text;                      
            int startIndex = 1;                                         
            int length = 2;
            string subtotalInteger = subtotal.Substring(startIndex, length);                                     //Use of sub string method to extract only the integer from the subtotal string
            Console.WriteLine("The subtotal is £" + subtotalInteger + ".00");
            decimal correctPercentage = 0.15m;
            decimal result = correctPercentage * int.Parse(subtotalInteger);
            Console.WriteLine("Correct discount calculated with the coupon should be £" + result);

            //Calculating the wrong discount
            decimal wrongPercentage = 0.1m;
            decimal incorrectResult = wrongPercentage * int.Parse(subtotalInteger);
            Console.WriteLine("Incorrect discount calculated with the coupon is £" + incorrectResult + "0");

            //Validating the correct discount has been applied
            Assert.That(couponDiscount == "£" + result, "Correct discount has not been applied");                         
            try
            {
                Assert.That(couponDiscount, Is.EqualTo("£" + result).IgnoreCase, "Correct discount has not been applied");
                Console.WriteLine("The correct discount has been applied - " + couponDiscount + " has been deducted");
            }
            catch (AssertionException) 
            {
                Assert.That(couponDiscount, Is.EqualTo("£" + incorrectResult).IgnoreCase, "Correct discount has been applied");
                Console.WriteLine("The correct discount has not been applied - amount deducted should state £" + couponDiscount + ", rather than £" + incorrectResult + "0" );

            }

            //Verifying total amount calculated after coupon and shipping is correct
            ScrollDown(driver, 300);
            char[] charsToTrim = { '£' };                                                                   //Use of Trim method to get the coupon discount in integer format
            string couponDiscountInteger = couponDiscount.TrimStart(charsToTrim);                                               
            string shippingCosts = driver.FindElement(By.CssSelector("label  bdi")).Text[1..];              //Use of string slicing/range indexing method to get the shipping costs in integer format
            Console.WriteLine("The subtotal is £" + subtotalInteger +".00, the coupon discount is £" + couponDiscountInteger + ", and the shipping cost is £" + shippingCosts);
            float totalCalculated = int.Parse(subtotalInteger) + float.Parse(shippingCosts) - float.Parse(couponDiscountInteger);
            string total = driver.FindElement(By.CssSelector("strong > .amount.woocommerce-Price-amount > bdi")).Text;
            Assert.That(total, Is.EqualTo("£" + totalCalculated + "0"), "Total amount charged is wrong");
            try
            {
                Assert.That(total, Is.EqualTo("£" + totalCalculated + "0").IgnoreCase, "Total amount charged is wrong");
                Console.WriteLine("The total amount charged (£" + totalCalculated + "0) is correct");
            }
            catch
            {
                Console.WriteLine("Correct total has not been calculated");
            }
            Thread.Sleep(3000);

            //Log Out with LogOut POM class
            ScrollDown(driver, 300);
            LogOutPOM logOut = new LogOutPOM(driver);
            logOut.GoMyAccount().ClickLogOut();
            Thread.Sleep(3000);
            Console.WriteLine("User has successfully logged out");

            //Clear the cart with ClearCart POM class
            existingUser.GoMyAccount().SetUsername().SetPassword().GoLogIn();
            ClearCartPOM clear = new ClearCartPOM(driver);
            clear.GoCart().GoRemoveItem();
            Thread.Sleep(3000);
            Console.WriteLine("Cart is cleared");
        } 
    }
}
         