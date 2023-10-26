using google_cloud.tests.driver;
using google_cloud.tests.model;
using google_cloud.tests.page;
using google_cloud.tests.utils;
using OpenQA.Selenium;

namespace google_cloud.tests
{
    public class Tests
    {
        IWebDriver driver;
        IWebDriver tempDriver;
        [SetUp]
        public void Setup()
        {
            /*string driverType = TestContext.Parameters.Get("browser");
            Console.WriteLine(driverType);*/
            string driverType = "edge";
            driver = DriverSelector.GetDriver(driverType);
            tempDriver = DriverSelector.GetDriver(driverType);
        }

        [Test]
        public void PricingCalculatorTest()
        {
            TemporaryEmail temporaryEmail = new TemporaryEmail(tempDriver);
            string email = temporaryEmail.Email;
            //string email = "dasdasdasdasds@gmail.com";
            FormData formData = new FormData(XMLProvider.GetData());

            decimal websiteEstimation  = new GCMainPage(driver).SearchForPricingCalculator().CalculateEstimation(formData, email);
            Thread.Sleep(5000);
            decimal emailEstimation = temporaryEmail.EstimatedCost();

            Assert.That(emailEstimation, Is.EqualTo(websiteEstimation));
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            tempDriver.Quit();
        }

    }
}