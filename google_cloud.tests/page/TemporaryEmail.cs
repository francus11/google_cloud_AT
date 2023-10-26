using google_cloud.tests.model;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace google_cloud.tests.page
{
    internal class TemporaryEmail : AbstractPage
    {
        private readonly string _url = "https://yopmail.com/";
        
        public string Email { get; private set; }

        private By AcceptPopUp { get; set; } = By.Id("accept");
        private By CreateEmailButton { get; set; } = By.XPath("//div[@id='listeliens']/a");
        private By EmailField { get; set; }  = By.Id("geny");
        private By ReceivedEmail { get; set; } = By.ClassName("lm");
        private By MailBox { get; set; }  = By.XPath("//button[@onclick='egengo();']");
        private By Refresh { get; set; } = By.Id("refresh");
        private By CostBox { get; set; }  = By.TagName("h2");

        public TemporaryEmail(IWebDriver driver) : base(driver) 
        {
            driver.Navigate().GoToUrl(_url);
            driver.FindElement(AcceptPopUp).Click();
            GenerateTempEmail();
            Email = GetEmail();
            GoToMailBox();
        }

        public decimal EstimatedCost()
        {
            driver.FindElement(Refresh).Click();

            Thread.Sleep(1000);

            IWebElement iframe1 = driver.FindElement(By.Id("ifinbox"));
            driver.SwitchTo().Frame(iframe1);

            driver.FindElement(ReceivedEmail).Click();
            driver.SwitchTo().DefaultContent();

            IWebElement iframe2 = driver.FindElement(By.Id("ifmail"));
            driver.SwitchTo().Frame(iframe2);

            string estimatedCostString = driver.FindElement(CostBox).Text;

            Match match = Regex.Match(estimatedCostString, @"[\d.]+");
            estimatedCostString = match.Value;

            driver.SwitchTo().DefaultContent();


            return decimal.Parse(estimatedCostString);
        }

        private string GetEmail()
        {
            return driver.FindElement(EmailField).Text;
        }

        private void GenerateTempEmail()
        {
            driver.FindElement(CreateEmailButton).Click();
        }

        private void GoToMailBox()
        {
            driver.FindElement(MailBox).Click();
        }
    }
}
