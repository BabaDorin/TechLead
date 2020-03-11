using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        //If true, it will be deleted, 'kinda'. Only it's creator will be able to see it.
        public bool isArchieved { get; set; }
        
        //If this parameter is set to true, then the problem won't be public after publishing and will be accesible
        //only for the members of the class the problem was assigned to.
        public bool AvailableOnlyForTheClass { get; set; }
        public bool MakeItPublic { get; set; }

        //if AvailableOnlyForTheClass is true then the problem will be accesible
        //only within the mother class (The class where it has been created) having id = MotherClassID 
        public int MotherClassID { get; set; }

        //When restricted mode is on, the user will be able to see only his submissions.
        //The author of the problem and the admins will be able to see all the submissions
        //Inputs and outputs won't be shown to the user (On submission details page)
        public bool RestrictedMode { get; set; }
        public bool NormalMode { get; set; }

        [Required(ErrorMessage = "Please, enter a Name for this exercise")]
        public string Name { get; set; }

        [Required]
        [Range(10, 400, ErrorMessage = "Range for the points property is 10-400. Insert a number between 10 and 400")]
        public int Points { get; set; }

        //Execution time, in miliseconds
        [Required]
        [Range(0, 9999999, ErrorMessage = "Please, enter a valid value or leave it 0 if you do not want to set any time limit")]
        public int ExecutionTime { get; set; }

        //Memory limit, in kb
        [Required]
        [Range(0, 9999999, ErrorMessage = "Please, enter a valid value or leave it 0 if you do not want to set any memory limit")]
        public int MemoryLimit { get; set; }

        public int SubmissionsAbove10Points { get; set; }

        public int SubmissionsUnder10Points { get; set; }

        public string Author { get; set; }

        [DisplayName("Make your source code public?")]
        public bool MakeSourceCodePublic { get; set; }

        public string AuthorID { get; set; }

        [Required(ErrorMessage = "Please, check a difficulty level for the problem")]
        public int DifficultyId { get; set; }


        public IEnumerable<Difficulty> Difficulty { get; set; }

        [Required(ErrorMessage = "Please, enter a condition for your problem")]
        public string Condition { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string InputFormat { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string OutputFormat { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Constraints { get; set; }



        [Required(ErrorMessage = "This field is required")]
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

        //Backend test cases------------------------------------------------
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