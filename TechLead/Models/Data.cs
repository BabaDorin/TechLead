﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechLead.Models;
using System.IO;
using PagedList;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json.Linq;

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
            Debug.WriteLine("Initialising");
            Test[] tests = new Test[inputs.Length];

            Debug.WriteLine("Input ");
            foreach(string s in inputs)
            {
                Debug.Write(s + " ");
            }

            Debug.WriteLine("Output ");
            foreach (string s in outputs)
            {
                Debug.Write(s + " ");
            }

            Debug.WriteLine("Going to insert test cases. {0} test cases", inputs.Length);
            for(int i=0; i<inputs.Length; i++)
            {
                Debug.WriteLine("Test " + i);
                tests[i] = new Test();
                tests[i].Input = inputs[i];
                tests[i].Output = outputs[i];
            }
            Debug.WriteLine("Going to return array of test cases");
            return tests;
        }
    }
}