using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Final.Project.POMClasses
{
    internal class InputBillingDetailsPOM
    {
        private IWebDriver driver;

        //Declaring Environment variables and Test Parameters to be used from run settings
        string firstName = Environment.GetEnvironmentVariable("user_first_name");      //User's personal details is located in the environment variable in the mysettings.runsettings
        string lastName = Environment.GetEnvironmentVariable("user_last_name");
        string companyName = Environment.GetEnvironmentVariable("company_name");
        string streetAddress = Environment.GetEnvironmentVariable("street_address");
        string houseUnit = Environment.GetEnvironmentVariable("house_unit");
        string townCity = Environment.GetEnvironmentVariable("town_city");
        string county = Environment.GetEnvironmentVariable("county");
        string postcode = Environment.GetEnvironmentVariable("postcode");
        string phone = Environment.GetEnvironmentVariable("phone");

        public InputBillingDetailsPOM(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        public IWebElement FirstName => driver.FindElement(By.Name("billing_first_name"));
        public IWebElement LastName => driver.FindElement(By.Name("billing_last_name"));
        public IWebElement CompanyName => driver.FindElement(By.Name("billing_company"));
        public IWebElement StreetAddress => driver.FindElement(By.Name("billing_address_1"));
        public IWebElement HouseUnit => driver.FindElement(By.Name("billing_address_2"));
        public IWebElement TownCity => driver.FindElement(By.Name("billing_city"));
        public IWebElement County => driver.FindElement(By.Name("billing_state"));
        public IWebElement Postcode => driver.FindElement(By.Name("billing_postcode"));
        public IWebElement Phone => driver.FindElement(By.Name("billing_phone"));
        public IWebElement CountryRegion => driver.FindElement(By.CssSelector("#billing_country_field > span > span > span.selection > span > span.select2-selection__arrow"));
        public IWebElement CountryRegionChoice => driver.FindElement(By.CssSelector("#billing_country_field > span > span > span.selection > span"));

        //Service Method
        public InputBillingDetailsPOM ClickFirstName()
        {
            FirstName.Click();
            FirstName.Clear();
            FirstName.SendKeys(firstName);
            return this;
        }
        public InputBillingDetailsPOM ClickLastName()
        {
            LastName.Click();
            LastName.Clear();
            LastName.SendKeys(lastName);
            return this;
        }
        public InputBillingDetailsPOM ClickCompanyName()
        {
            CompanyName.Click();
            CompanyName.Clear();
            CompanyName.SendKeys(companyName);
            Thread.Sleep(3000);
            return this;
        }
        public InputBillingDetailsPOM ClickStreetAddress()
        {
            StreetAddress.Click();
            StreetAddress.Clear();
            StreetAddress.SendKeys(streetAddress);
            return this;
        }
        public InputBillingDetailsPOM ClickHouseUnit()
        {
            HouseUnit.Click();
            HouseUnit.Clear();
            HouseUnit.SendKeys(houseUnit);
            return this;
        }
        public InputBillingDetailsPOM ClickTownCity()
        {
            TownCity.Click();
            TownCity.Clear();
            TownCity.SendKeys(townCity);
            return this;
        }
        public InputBillingDetailsPOM ClickCounty()
        {
            County.Click();
            County.Clear();
            County.SendKeys(county);
            Thread.Sleep(3000);
            return this;
        }
        public InputBillingDetailsPOM ClickPostcode()
        {
            Postcode.Click();
            Postcode.Clear();
            Postcode.SendKeys(postcode);
            return this;
        }
        public InputBillingDetailsPOM ClickPhone()
        {
            Phone.Click();
            Phone.Clear();
            Phone.SendKeys(phone);
            return this;
        }

        public InputBillingDetailsPOM ClickCountryRegion()
        {
            CountryRegion.Click();
            CountryRegionChoice.Click();
            return this;
        }


    }
}
