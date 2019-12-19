using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    public class Test
    {
        public string TestNumber { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }

    }
    public class ExerciseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, enter a Name for this exercise")]
        public string Name { get; set; }

        [Required]
        [Range(10, 400, ErrorMessage = "Range for the points property is 10-400. Insert a number between 10 and 400")]
        public int Points { get; set; }

        public int SubmissionsAbove10Points { get; set; }

        public int SubmissionsUnder10Points { get; set; }

        public ApplicationUser Author { get; set; }

        [Required(ErrorMessage = "Please, check a difficulty level for the problem")]
        public int DifficulyId { get; set; }


        public IEnumerable<Difficulty> Difficulty { get; set; }

        [Required(ErrorMessage = "Please, enter a condition for your problem")]
        public string Condition { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string ImputFormat { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string OutputFormat { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Constraints { get; set; }



        [Required(ErrorMessage = "This field is required")]
        public string Imput1 { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Explanation1 { get; set; }

        public string Imput2 { get; set; }
        public string Explanation2 { get; set; }

        public string Imput3 { get; set; }
        public string Explanation3 { get; set; }

        public string Imput4 { get; set; }
        public string Explanation4 { get; set; }

        public string Imput5 { get; set; }
        public string Explanation5 { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Output1 { get; set; }

        public string Output2 { get; set; }

        public string Output3 { get; set; }

        public string Output4 { get; set; }

        public string Output5 { get; set; }

        //Backend test cases------------------------------------------------
        public int NumberOfTests { get; set; }
        public Test[] Test { get; set; }
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