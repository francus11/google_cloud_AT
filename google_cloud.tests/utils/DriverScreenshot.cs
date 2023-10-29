using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace google_cloud.tests.utils
{
    internal static class DriverScreenshot
    {
        public static void TakeScreenshot(IWebDriver driver)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            if (!Directory.Exists("screenshots"))
            {
                Directory.CreateDirectory("screenshots");
            }

            string screenshotPath = Path.Combine("screenshots", $"screenshot_{timestamp}.png");

            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
        }
    }
}
