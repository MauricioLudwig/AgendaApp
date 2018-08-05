using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class DashboardItemVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}