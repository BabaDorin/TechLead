using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    public class ErrorViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Code { get; set; }
        public string ImagePath { get; set; }
        public ErrorViewModel()
        {
            Code = 0;
        }
    }
}