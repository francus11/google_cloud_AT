using OpenQA.Selenium.DevTools.V116.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace google_cloud.tests.model
{
    internal class FormData
    {

        public Dictionary<string, string> Data { get; private set; }

        public FormData(Dictionary<string, string> data)
        {
            Data = data;
        }

        public string this[string key]
        {
            get { return Data[key]; }
            private set { Data[key] = value; }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is FormData other)
            {
                return this.Data.Equals(other.Data);
            }

            return false;

        }
    }
}
