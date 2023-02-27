using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;

namespace e_Commerce.Final.Project.Utilities
{
    internal static class StaticHelpers
    {
        public static void WaitForElement(By locator, int timeToWaitInSeconds, IWebDriver driver)
        {
            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(timeToWaitInSeconds));
            wait2.Until(drv => drv.FindElement(locator).Displayed);
        }
        public static void ScrollDown(IWebDriver driver, int scrollAmountX)
        {
            Actions actionsscroll = new Actions(driver);
            actionsscroll.ScrollByAmount(scrollAmountX, 0);
            actionsscroll.Perform(); 
        }
        public static void ScrollUp(IWebDriver driver, int scrollAmount)
        {
            Actions actionscrollup = new Actions(driver);
            actionscrollup.ScrollByAmount(0, scrollAmount);
            actionscrollup.Perform();
        }
        public static void SetEnvironmentVariable(IWebDriver driver, By locator, string environmentVariable, string variable)
        {
            environmentVariable = Environment.GetEnvironmentVariable(variable);
            driver.FindElement(locator).SendKeys(environmentVariable);
        }
        public static void TakeScreenshotOfElement(IWebDriver driver, By locator, string filename)
        {
            IWebElement form = driver.FindElement(locator);
            ITakesScreenshot formss = form as ITakesScreenshot;
            var screenshotForm = formss.GetScreenshot();
            screenshotForm.SaveAsFile(@"C:\Users\NethmaWimalasuriya\OneDrive - nFocus Limited\Pictures\Screenshots\" + filename, ScreenshotImageFormat.Png);
            TestContext.WriteLine("Screenshot taken - see report");     //Could add a DateTime variable
            TestContext.AddTestAttachment(@"C:\Users\NethmaWimalasuriya\OneDrive - nFocus Limited\Pictures\Screenshots\" + filename);
        }

    }
}
