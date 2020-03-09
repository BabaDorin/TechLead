using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TechLead.Models
{
    public class ImportExerciseViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public int ExerciseId { get; set; }
        public int ClassId { get; set; }
    }
}