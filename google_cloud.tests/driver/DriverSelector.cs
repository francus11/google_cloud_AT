using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace google_cloud.tests.driver
{
    internal class DriverSelector
    {
        public static IWebDriver GetDriver(string select)
            => select switch
            {
                "chrome" => new ChromeDriver(),
                "edge" => new EdgeDriver(),
                _ => new EdgeDriver()
            };
    }
}
