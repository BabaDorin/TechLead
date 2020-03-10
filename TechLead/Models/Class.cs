using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TechLead.Models
{
    public class Class
    {
        public Class()
        {
            Exercises = new HashSet<Exercise>();
            Members = new HashSet<ApplicationUser>();
        }

        public int ClassID { get; set; }

        [Required]
        public string ClassName { get; set; }

        //Some additional info for the class
        public string ClassInfo { get; set; }
        public DateTime CreationDate { get; set; }
        public string ClassCreatorID { get; set; }

        //A unique 6 characters string randomly generated via Guid
        public string ClassInvitationCode { get; set; }

        //The members of the class
        public virtual ICollection<ApplicationUser> Members { get; set; }

        //The collection of exercises within the class
        public virtual ICollection<Exercise> Exercises { get; set; }

        //The collection of join requests
        public virtual ICollection<JoinRequest> JoinRequests { get; set; }


    }
}