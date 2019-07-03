using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public int SubmissionsAbove10Points { get; set; }
        public int SubmissionsUnder10Points { get; set; }
        public ApplicationUser Author { get; set; } 
        public IEnumerable<Domain> Domain { get; set; }
        public string Condition { get; set; }
        public string Imput1 { get; set; }
        public string Imput2 { get; set; }
        public string Imput3 { get; set; }
        public string Imput4 { get; set; }
        public string Imput5 { get; set; }
        public string Output1 { get; set; }
        public string Output2 { get; set; }
        public string Output3 { get; set; }
        public string Output4 { get; set; }
        public string Output5 { get; set; }

        public int Submissions()
        {
            return SubmissionsAbove10Points + SubmissionsUnder10Points;
        }
        public int SuccesRate()
        {
            return SubmissionsAbove10Points / (Submissions() / 100);
        }
    }
}