using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class DashboardCategoryVM
    {
        public string Title { get; set; }
        public List<DashboardItemVM> Items { get; set; }
    }
}