using google_cloud.tests.driver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace google_cloud.tests.page
{
    internal class GCMainPage : AbstractPage
    {
        private readonly string _url = "https://cloud.google.com/ ";

        private By SearchBar { get; set; } = By.ClassName("mb2a7b");

        public GCMainPage(IWebDriver driver) : base(driver)
        {
            driver.Navigate().GoToUrl(_url);
        }

        public GCPricingCalculator SearchForPricingCalculator()
        {
            GoToPricingCalculator();
            return new GCPricingCalculator(driver);
        }

        private void GoToPricingCalculator()
        {
            IWebElement searchBar = driver.FindElement(SearchBar);
            searchBar.Click();
            string query = "Google Cloud Pricing Calculator";
            searchBar.SendKeys(query);
            searchBar.Submit();

            IWebElement link = driver.FindElement(By.PartialLinkText(query));
            link.Click();
        }
    }
}
