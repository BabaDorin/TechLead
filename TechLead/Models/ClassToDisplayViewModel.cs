using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    public class ClassToDisplayViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public int NumberOfMembers { get; set; }
    }
}