using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    public class SuccesIndex
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Meme> Memes { get; set; }
    }
}