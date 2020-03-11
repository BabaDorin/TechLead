using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    public class Exercise
    {
        public Exercise()
        {
            this.Classes = new HashSet<Class>();
        }

        public int Id { get; set; }

        //If true, it will be deleted, 'kinda'. Only it's creator will be able to see it.
        public bool isArchieved { get; set; }

        //When restricted mode is on, the user will be able to see only his submissions.
        //The author of the problem and the admins will be able to see all the submissions
        //Inputs and outputs won't be shown to the user (On submission details page)
        public bool RestrictedMode { get; set; }
        
        //When an exercise is created within a class, then it won't be displayed in the beginner
        // / Advanced / Intermediate branch. The class creator will decide if the exercise will go
        //public or not.
        public bool AvailableOnlyForTheClass { get; set; }

        public int MotherClassID { get; set; }

        [Required(ErrorMessage ="Please, enter a Name for your problem")]
        public string Name { get; set; }

        [Required]
        [Range(10, 400, ErrorMessage = "Range for the points property is 10-400. Insert a number between 10 and 400")]
        public int Points { get; set; }

        //Execution time, in miliseconds
        public int ExecutionTime { get; set; }

        //Memory limit, int kb
        public int MemoryLimit { get; set; }

        public int SubmissionsAbove10Points { get; set; }

        public int SubmissionsUnder10Points { get; set; }

        public string Author { get; set; }
        public string AuthorID { get; set; }

        [Required(ErrorMessage ="Please, check a difficulty level for the problem")]
        public int DifficultyId { get; set; }


        public IEnumerable<Difficulty> Difficulty { get; set; }

        [Required(ErrorMessage ="Please, enter a condition for your problem")]
        public string Condition { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string InputFormat { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string OutputFormat { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Constraints { get; set; }



        [Required(ErrorMessage ="This field is required")]
        public string Input1 { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Explanation1 { get; set; }

        public string Input2 { get; set; }
        public string Explanation2 { get; set; }

        public string Input3 { get; set; }
        public string Explanation3 { get; set; }

        public string Input4 { get; set; }
        public string Explanation4 { get; set; }

        public string Input5 { get; set; }
        public string Explanation5 { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Output1 { get; set; }

        public string Output2 { get; set; }

        public string Output3 { get; set; }

        public string Output4 { get; set; }

        public string Output5 { get; set; }
        public string InputColection { get; set; }
        public string OutputColection { get; set; }
        public int NumberOfTests { get; set; }
        
        public ICollection<Class> Classes { get; set; }
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