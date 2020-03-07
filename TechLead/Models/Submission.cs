using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechLead.Models
{
    public class Submission
    {
        public int SubmissionID { get; set; }
        public string SubmissionAuthorId { get; set; }

        public bool RestrictedMode { get; set; }
        public string SubmissionAuthorUserName { get; set; }

        //Date when the user submitted the solution.
        public DateTime Date { get; set; }

        public int ExerciseId { get; set; }
        public string ExerciseAuthorId { get; set; }

        //The name of the exercise
        public string Exercise { get; set; }

        //Total points scored by the program
        public double ScoredPoints { get; set; }

        public int NumberOfTestCases { get; set; }

        //There may be different number of test cases. For example, when the exercise has 100 points and 5 test cases,
        //each test case will get 20 points.
        public double DistributedPointsPerTestCase { get; set; }

        //Source code submitted by the user
        public string SourceCode { get; set; }

        //The user choose to make his source public or not.
        //If it's public, then it can be seen on 'Submission details' page.
        public bool MakeSourceCodePublic { get; set; }

        //All the inputs for a specific exercise delimited by the techlead delimitator
        public string InputCollection { get; set; }

        //All the outputs of the program the user had submitted
        public string OutputCollection { get; set; }

        //The collection of correct output for each input
        public string ExpectedOutput { get; set; }

        //Score for each test case
        public string PointsPerTestCase { get; set; }

        //Execution time in miliseconds for each test case
        public string ExecutionTimePerTestCase { get; set; }

        //Compile status for each test case  [Accepted / not accepted / Compilation Error etc]
        public string StatusPerTestCase { get; set; }

        //Error message
        public string ErrorMessage { get; set; }
    }
}