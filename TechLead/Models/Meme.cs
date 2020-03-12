using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechLead.Models
{
    public class Meme
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ImageBase64 { get; set; }
        public int SuccesIndexId { get; set; }

        [ForeignKey("SuccesIndexId")]
        public SuccesIndex SuccesIndex { get; set; }
        public ICollection<Submission> Submissions { get; set; }
    }
}