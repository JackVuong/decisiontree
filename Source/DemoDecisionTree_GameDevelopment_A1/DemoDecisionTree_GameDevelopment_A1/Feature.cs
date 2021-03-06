﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDecisionTree_GameDevelopment_A1
{
    public class Feature
    {
        List<string> values;
        string name;
        string label;

        public List<string> Values
        {
            get { return values; }
            set { values = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        public Feature()
        {
            this.Name = "";
            this.Label = "";
            this.Values = new List<string>();
        }

        public Feature(List<string> Value, string Name)
        {
            this.Values = Value;
            this.Name = Name;
            this.Label = "";
        }

        public Feature(string Label)
        {
            this.Label = Label;
            this.Name = string.Empty;
            Values = new List<string>();
        }

        public void AddValue(string Value)
        {
            if (!values.Contains(Value))
                values.Add(Value);
        }
    }
}
