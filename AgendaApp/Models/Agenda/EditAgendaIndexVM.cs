using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class EditAgendaIndexVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? Deadline { get; set; }
        public List<string> Categories { get; set; }
    }
}