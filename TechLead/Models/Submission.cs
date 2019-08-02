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

        //Scores and execution time for each test
        public int Score1 { get; set; } = -1;
        public int ExecutionTime1 { get; set; } = -1;
        public int Score2 { get; set; } = -1;
        public int ExecutionTime2 { get; set; } = -1;
        public int Score3 { get; set; } = -1;
        public int ExecutionTime3 { get; set; } = -1;
        public int Score4 { get; set; } = -1;
        public int ExecutionTime4 { get; set; } = -1;
        public int Score5 { get; set; } = -1;
        public int ExecutionTime5 { get; set; } = -1;
        public int Score6 { get; set; } = -1;
        public int ExecutionTime6 { get; set; } = -1;
        public int Score7 { get; set; } = -1;
        public int ExecutionTime7 { get; set; } = -1;
        public int Score8 { get; set; } = -1;
        public int ExecutionTime8 { get; set; } = -1;
        public int Score9 { get; set; } = -1;
        public int ExecutionTime9 { get; set; } = -1;
        public int Score10 { get; set; } = -1;
        public int ExecutionTime10 { get; set; } = -1;

        public int SubmissionID { get; set; }
        public ApplicationUser User { get; set; }
        public string Exercise { get; set; }
        public int ExerciseId { get; set; }
        public int Points { get; set; }
        public string Time { get; set; }
        public string SourceCode { get; set; }
    }
}