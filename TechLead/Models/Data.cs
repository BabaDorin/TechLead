using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    public class Data
    {
        //Here will be stored various data and constants.
        public string testCase_Delimitator = ";-;techleadDelimitator;-;-;x;x;x;x;x;x;x;TL'";
        public Test[] CreateTests(string inputCollection, string outputCollection)
        {
            string[] inputs = inputCollection.Split(new string[] { testCase_Delimitator }, StringSplitOptions.None);
            string[] outputs = outputCollection.Split(new string[] { testCase_Delimitator }, StringSplitOptions.None);
            Test[] tests = new Test[inputs.Length];
            for(int i=0; i<inputs.Length; i++)
            {
                tests[i].Input = inputs[i];
                tests[i].Output = outputs[i];
            }
            return tests;
        }
    }
}