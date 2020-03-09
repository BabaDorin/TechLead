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
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TechLead.Custom_Exceptions;

namespace TechLead.Models
{
    public class Data
    {
        //Here will be stored various data like constants, some methods etc.
        public static Data data = new Data();
        public string Delimitator = ";-;techleadDelimitator;-;-;x;x;x;x;x;x;x;TL'";
        public Test[] CreateTests(string inputCollection, string outputCollection)
        {
            string[] inputs = inputCollection.Split(new string[] { Delimitator }, StringSplitOptions.None);
            string[] outputs = outputCollection.Split(new string[] { Delimitator }, StringSplitOptions.None);
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
        
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public BestSubmission[] ConvertBestSubmissionFromStringToArray(string usersBestSubmissions)
        {
            if (usersBestSubmissions == null) return null;
            string[] submissionsData = usersBestSubmissions.Split(new string[] { Delimitator }, StringSplitOptions.None);
            BestSubmission[] bestSubmissions = new BestSubmission[submissionsData.Length / 5];
            if (bestSubmissions.Length > 0)
            {
                int submissionsDataIndex = 0;
                for (int i = 0; i < bestSubmissions.Length; i++)
                {
                    bestSubmissions[i] = new BestSubmission
                    {
                        ProblemID = int.Parse(submissionsData[submissionsDataIndex++]),
                        ProblemName = submissionsData[submissionsDataIndex++],
                        SubmissionID = int.Parse(submissionsData[submissionsDataIndex++]),
                        TotalPoints = double.Parse(submissionsData[submissionsDataIndex++]),
                        MaxScoredPoints = double.Parse(submissionsData[submissionsDataIndex++])
                    };
                }
            }
            return bestSubmissions;
        }

        public string ConvertBestSubmissionFromArrayToString(BestSubmission[] bestSubmission)
        {
            string result = "";
            for (int i = 0; i < bestSubmission.Length; i++)
            {
                if (i != 0)
                {
                    result += Delimitator;
                }
                result += bestSubmission[i].ProblemID + Delimitator + bestSubmission[i].ProblemName + Delimitator +
                    +bestSubmission[i].SubmissionID + Delimitator + bestSubmission[i].TotalPoints + Delimitator + bestSubmission[i].MaxScoredPoints;
            }

            return result;
        }
    }
}