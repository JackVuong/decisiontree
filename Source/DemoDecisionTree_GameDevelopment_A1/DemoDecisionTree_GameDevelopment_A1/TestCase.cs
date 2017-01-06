using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDecisionTree_GameDevelopment_A1
{
    class TestCase
    {
        public string featureName;
        public string value;
        public TestCase()
        {
            featureName = "default";
            value = "default";
        }

        public TestCase(string featureName,string value)
        {
            this.featureName = featureName;
            this.value = value;
        }
    }
}
