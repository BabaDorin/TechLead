using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechLead.Models
{
    public class JoinRequest
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }
        public int ClassId { get; set; }

        [ForeignKey("ClassId")]
        public Class Class { get; set; }
    }
}