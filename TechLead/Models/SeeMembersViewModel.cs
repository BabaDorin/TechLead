using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    public class SeeMembersViewModel
    {
        public int ClassId { get; set; }
        public string Name { get; set; }
        public string userID { get; set; }
        public DateTime JoinDate { get; set; }
    }
}