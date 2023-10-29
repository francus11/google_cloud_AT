using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

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
        /*public static IWebDriver GetDriver(string select)
        {
            switch(select)
            {
                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    return new EdgeDriver();
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    return new ChromeDriver();
                default:
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    return new EdgeDriver();
            }
        }*/
    }
}
