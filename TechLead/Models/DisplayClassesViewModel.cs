using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    public class DisplayClassesViewModel
    {
        public DisplayClassesViewModel()
        {
            Classes_Joined = new List<ClassToDisplayViewModel>();
            Classes_Owned = new List<ClassToDisplayViewModel>();
        }
        //List that will store the classes in which the user is a simple member
        public List<ClassToDisplayViewModel> Classes_Joined { get; set; }

        //List that will store the classes the user owns
        public List<ClassToDisplayViewModel> Classes_Owned { get; set; }
    }
}