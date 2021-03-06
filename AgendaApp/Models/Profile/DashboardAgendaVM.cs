﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class DashboardAgendaVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Deadline { get; set; }
        public List<DashboardCategoryVM> Categories { get; set; }
        public int CompletedItemsCount { get; set; }
        public int TotalItemsCount { get; set; }
    }
}