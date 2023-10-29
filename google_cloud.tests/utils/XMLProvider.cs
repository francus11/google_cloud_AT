using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace google_cloud.tests.utils
{
    public static class XMLProvider
    {
        public static Dictionary<string, string> GetData()
        {
            string xmlFilePath = TestContext.Parameters.Get("testDataFile");
            //string xmlFilePath = "data/qa_testdata.xml";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            Dictionary<string, string> dataDictionary = new Dictionary<string, string>();

            XmlNodeList elements = xmlDoc.SelectNodes("/data/*");
            foreach (XmlNode element in elements)
            {
                string elementName = element.Name;
                string elementValue = element.InnerText;
                dataDictionary[elementName] = elementValue;
            }

            return dataDictionary;
        }
    }
}
