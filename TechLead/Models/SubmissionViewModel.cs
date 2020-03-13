using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    public class SubmissionViewModel
    {
        public int SubmissionID { get; set; }
        public string SubmissionAuthorId { get; set; }

        public bool RestrictedMode { get; set; }
        public string SubmissionAuthorUserName { get; set; }
        public int? classId { get; set; }

        //Date when the user submitted the solution
        public DateTime Date { get; set; }

        public int ExerciseId { get; set; }

        //The name of the exercise
        public string Exercise { get; set; }

        //Total points scored by the program
        public double ScoredPoints { get; set; }

        public int NumberOfTestCases { get; set; }

        //There may be different number of test cases. For example, when the exercise has 100 points and 5 test cases,
        //each test case will get 20 points.
        public double DistributedPointsPerTestCase { get; set; }

        public string SourceCode { get; set; }
        public bool MakeSourceCodePublic { get; set; }

        //All the inputs for a specific exercise delimited by the techlead delimitator
        public string[] Inputs { get; set; }

        //All the outputs of the program the user had submitted
        public string[] Outputs { get; set; }

        //The collection of correct output for each input
        public string[] ExpectedOutputs { get; set; }

        //Score for each test case
        public double[] Points { get; set; }

        //Execution time in miliseconds for each test case
        public int[] ExecutionTime { get; set; }

        //Compile status for each test case  [Accepted / not accepted / Compilation Error etc]
        public string[] Status { get; set; }

        //Error message
        public string[] ErrorMessage { get; set; }
        
        public string[] AllMedia { get; set; }
    }
}