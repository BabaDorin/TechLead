﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    public class ProblemTrashCan
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, enter a Name for your problem")]
        public string Name { get; set; }

        [Required]
        [Range(10, 400, ErrorMessage = "Range for the points property is 10-400. Insert a number between 10 and 400")]
        public int Points { get; set; }

        public int SubmissionsAbove10Points { get; set; }

        public int SubmissionsUnder10Points { get; set; }

        public string Author { get; set; }
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

        public string InputColection { get; set; }

        public string OutputColection { get; set; }

        public int NumberOfTests { get; set; }
    }
}