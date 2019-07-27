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
        public ApplicationUser User { get; set; }

        public Exercise Exercise { get; set; }

        public int Points;
        public DateTime Time { get; set; }
    }
}