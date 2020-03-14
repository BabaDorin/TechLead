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
        public List<DisplayExerciseGeneralInfoViewModel> usersExercises { get; set; }

        public ProfileViewModel(ApplicationUser User)
        {
            UserName = User.UserName;
            PhoneNumber = User.PhoneNumber;
            About = User.About;
            TotalPoints = User.TotalPoints;
            FirstRegistration = User.FirstRegistration;
            Job = User.Job;
            Email = User.Email;
            ProfilePhoto = User.ProfilePhoto;
        }
    }
}