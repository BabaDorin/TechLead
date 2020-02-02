using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
namespace TechLead.Models
{
    public class ProfileViewModel
    {
        public byte[] ProfilePhoto { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string About { get; set; }
        public double TotalPoints { get; set; }
        public DateTime FirstRegistration { get; set; }
        public string Job { get; set; }
        public string Email { get; set; }
        public BestSubmission[] bestSubmissions;
        public ProfileViewModel(string UserName, string PhoneNumber, string About, double TotalPoints,
            DateTime FirstRegistration, string Job, string Email, byte[] ProfilePhoto)
        {
            this.UserName = UserName;
            this.PhoneNumber = PhoneNumber;
            this.About = About;
            this.TotalPoints = TotalPoints;
            this.FirstRegistration = FirstRegistration;
            this.Job = Job;
            this.Email = Email;
            this.ProfilePhoto = ProfilePhoto;
        }
    }
}