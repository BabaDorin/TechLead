using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


        public int DifficulyId { get; set; }


        public IEnumerable<Difficulty> Difficulty { get; set; }


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

        //Backend test cases------------------------------------------------

        public string TestImput1 { get; set; }


        public string TestImput2 { get; set; }

        public string TestImput3 { get; set; }
        public string TestImput4 { get; set; }
        public string TestImput5 { get; set; }
        public string TestImput6 { get; set; }
        public string TestImput7 { get; set; }
        public string TestImput8 { get; set; }
        public string TestImput9 { get; set; }
        public string TestImput10 { get; set; }

        public string TestOutput1 { get; set; }

        public string TestOutput2 { get; set; }


        public string TestOutput3 { get; set; }

        public string TestOutput4 { get; set; }
        public string TestOutput5 { get; set; }
        public string TestOutput6 { get; set; }
        public string TestOutput7 { get; set; }
        public string TestOutput8 { get; set; }
        public string TestOutput9 { get; set; }
        public string TestOutput10 { get; set; }

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