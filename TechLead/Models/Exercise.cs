using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ApplicationUser Author { get; set; } 
        public Domain Domain { get; set; }
    }
}