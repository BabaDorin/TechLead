using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    public class BestSubmission
    {
        public int ProblemID { get; set; }
        public string ProblemName { get; set; }
        public int SubmissionID { get; set; }
        public double TotalPoints { get; set; }
        public double MaxScoredPoints { get; set; }
    }
}