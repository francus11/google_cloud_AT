using google_cloud.tests.driver;
using google_cloud.tests.model;
using google_cloud.tests.page;
using google_cloud.tests.utils;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;

namespace google_cloud.tests
{
    [TestFixture]
    public class Tests
    {
        IWebDriver driver;
        IWebDriver tempDriver;
        [SetUp]
        public void Setup()
        {
            string driverType = TestContext.Parameters.Get("browser");
            Console.WriteLine(driverType);
            //string driverType = "edge";
            driver = DriverSelector.GetDriver(driverType);
            driver.Manage().Window.Maximize();
            tempDriver = DriverSelector.GetDriver(driverType);
        }

        [Test]
        public void PricingCalculatorTest()
        {
            try
            {
                TemporaryEmail temporaryEmail = new TemporaryEmail(tempDriver);
                string email = temporaryEmail.Email;
                FormData formData = new FormData(XMLProvider.GetData());

                decimal websiteEstimation = new GCMainPage(driver).SearchForPricingCalculator().CalculateEstimation(formData, email);
                Thread.Sleep(5000);
                decimal emailEstimation = temporaryEmail.EstimatedCost();

                Assert.That(emailEstimation, Is.EqualTo(websiteEstimation));
            }
            catch(Exception ex)
            {
                DriverScreenshot.TakeScreenshot(driver);
                Assert.Fail("Test failed: " + ex.Message);
                throw;
            }
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            tempDriver.Quit();
        }

    }
}