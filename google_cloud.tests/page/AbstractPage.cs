using NUnit.Framework.Internal;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace google_cloud.tests.page
{
    internal abstract class AbstractPage
    {
        protected IWebDriver driver;
        protected Logger logger;
        protected AbstractPage(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
