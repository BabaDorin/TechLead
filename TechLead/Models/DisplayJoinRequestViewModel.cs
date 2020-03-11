using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    public class DisplayJoinRequestViewModel
    {
        //The id of the request
        public int Id { get; set; }

        //Info about the user who made the request
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}