using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class AgendaVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Deadline { get; set; }
        public int ItemCount { get; set; }
    }
}