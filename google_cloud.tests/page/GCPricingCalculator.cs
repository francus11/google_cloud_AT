using google_cloud.tests.model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace google_cloud.tests.page
{
    internal class GCPricingCalculator : AbstractPage
    {
        private By CostField { get; set; } = By.ClassName("cpc-cart-total");

        private By EstimateButton { get; set; } = By.ClassName("cpc-button");

        private By InstanceNumber { get; set; } = By.Id("input_99");

        private By OperatingSystemField { get; set; } = By.Id("select_112");
        private By VMClassField { get; set; } = By.Id("select_116");
        private By SeriesField { get; set; } = By.Id("select_124");
        private By InstanceTypeField { get; set; } = By.Id("select_126");
        private By AddGPUsCheckbox { get; set; } = By.XPath($"//*[contains(text(), '{"Add GPUs"}')]");
        private By GPUTypeField { get; set; } = By.XPath($"//*[contains(text(), '{"GPU type"}')]");
        private By NumberOfGPUsField { get; set; } = By.XPath($"//*[contains(text(), '{"Number of GPUs"}')]");
        private By LocalSSDsField { get; set; } = By.Id("select_465");
        private By DatacenterLocationField { get; set; } = By.Id("select_132");
        private By CommittedUsageField { get; set; } = By.Id("select_139");

        private By EstimationsField { get; set; } = By.Id("resultBlock");
        private By EmailEstimateButton { get; set; } = By.Id("Email Estimate");
        private By EmailInput { get; set; } = By.XPath("//input[@ng-model='emailQuote.user.email']");
        private By SendEmail { get; set; } = By.XPath("//button[@ng-click='emailQuote.emailQuote(true); emailQuote.$mdDialog.hide()']");

        public GCPricingCalculator(IWebDriver driver) : base(driver)
        {
            
        }

        public decimal CalculateEstimation(FormData formData, string email)
        {
            IWebElement iframe1 = driver.FindElement(By.TagName("iframe"));
            driver.SwitchTo().Frame(iframe1);
            IWebElement iframe2 = driver.FindElement(By.Id("myFrame"));
            driver.SwitchTo().Frame(iframe2);


            IWebElement instanceNumber = driver.FindElement(InstanceNumber);
            instanceNumber.SendKeys(formData["number_of_instances"]);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            SelectOption(js, OperatingSystemField, formData["operating_system"]);

            SelectOption(js, VMClassField, formData["vm_class"]);

            string[] parts = formData["instance_type"].Split('-');
            if (parts.Length > 0)
            {
                string left = parts[0].ToUpper();
                SelectOption(js, SeriesField, left);
            }

            SelectOption(js, InstanceTypeField, formData["instance_type"]);

            Click(js, AddGPUsCheckbox);

            SelectOption(js, GPUTypeField, formData["gpu_type"]);
            SelectOption(js, NumberOfGPUsField, formData["number_of_gpu"]);

            SelectOption(js, LocalSSDsField, formData["local_ssd"]);

            SelectOption(js, DatacenterLocationField, formData["datacenter_location"]);

            SelectOption(js, CommittedUsageField, formData["commited_usage"]);

            Click(js, EstimateButton);

            WebDriverWait waitForResult = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            Thread.Sleep(5000);

            string estimatedCostString = driver.FindElement(CostField).Text;
            Match match = Regex.Match(estimatedCostString, @"[\d.]+");
            estimatedCostString = match.Value;

            Click(js, EmailEstimateButton);
            driver.FindElement(EmailInput).SendKeys(email);

            Click(js, SendEmail);

            driver.SwitchTo().DefaultContent();
            return decimal.Parse(estimatedCostString);
        }

        private void SelectOption(IJavaScriptExecutor js, By by, string option)
        {
            Click(js, by);
            Click(js, ElementContainsText(option));
        }
        
        private void Click(IJavaScriptExecutor js, By by)
        {
            IWebElement element = driver.FindElement(by);
            js.ExecuteScript("arguments[0].click();", element);
        }

        private By ElementContainsText(string text)
        {
            return By.XPath($"//*[contains(text(), '{text}')]");
        }
    }
}
