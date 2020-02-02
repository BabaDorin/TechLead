using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLead.Models;
using System.Diagnostics;

namespace Assembly
{
    public class Problem
    {
        public string Delimitator = ";-;techleadDelimitator;-;-;x;x;x;x;x;x;x;TL'";

        //General methods related to processing / parsing / checking data about problems

        public Test[] CreateTests(string inputCollection, string outputCollection)
        {
            string[] inputs = inputCollection.Split(new string[] { Delimitator }, StringSplitOptions.None);
            string[] outputs = outputCollection.Split(new string[] { Delimitator }, StringSplitOptions.None);
            Debug.WriteLine("Initialising");
            Test[] tests = new Test[inputs.Length];

            Debug.WriteLine("Input ");
            foreach (string s in inputs)
            {
                Debug.Write(s + " ");
            }

            Debug.WriteLine("Output ");
            foreach (string s in outputs)
            {
                Debug.Write(s + " ");
            }

            Debug.WriteLine("Going to insert test cases. {0} test cases", inputs.Length);
            for (int i = 0; i < inputs.Length; i++)
            {
                Debug.WriteLine("Test " + i);
                tests[i] = new Test();
                tests[i].Input = inputs[i];
                tests[i].Output = outputs[i];
            }
            Debug.WriteLine("Going to return array of test cases");
            return tests;
        }

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    }
}
