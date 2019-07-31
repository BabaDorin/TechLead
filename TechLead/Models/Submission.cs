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
        public List<int> Scores { get; set; }
        public List<int> ExecutionTimeForEachTest { get; set; }
        public int SubmissionID { get; set; }
        public ApplicationUser User { get; set; }

        public string Exercise { get; set; }
        public int ExerciseId { get; set; }
        public int Points { get; set; }
        public string Time { get; set; }
        public string SourceCode { get; set; }
    }
}