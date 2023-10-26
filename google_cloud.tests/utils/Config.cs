using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace google_cloud.tests.utils
{
    internal class Config
    {
        static private Config _instance;

        public string Driver { get; }
        public static Config Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Config();
                }

                return _instance;
            }
        }

        private Config() 
        {
            
        }

        
    }
}
