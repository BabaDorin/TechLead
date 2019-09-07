using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
namespace TechLead.Models
{
    public class ProfileViewModel
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string About { get; set; }
        public double TotalPoints { get; set; }
        public DateTime FirstRegistration { get; set; }
        public string Job { get; set; }
        public string Email { get; set; }
    }
}