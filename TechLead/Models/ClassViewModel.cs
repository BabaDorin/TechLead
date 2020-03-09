using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TechLead.Models
{
    public class ClassViewModel
    {
        public int ClassID { get; set; }

        [Required(ErrorMessage = "This fiels is required")]
        public string ClassName { get; set; }

        //Some additional info for the class
        public string ClassInfo { get; set; }
        public DateTime CreationDate { get; set; }
        public string ClassCreatorID { get; set; }

        //A unique 6 characters string randomly generated via Guid
        public string ClassInvittionCode { get; set; }

        public List<DisplayExerciseGeneralInfoViewModel> Exercises { get; set; }
        public List<DisplayApplicationUserViewModel> Members { get; set; }
    }
}