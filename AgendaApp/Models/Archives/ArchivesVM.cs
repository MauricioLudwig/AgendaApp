using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class ArchivesVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ArchivedDate { get; set; }
        public int CompletedItemsCount { get; set; }
        public int TotalItemsCount { get; set; }
        public int Ratio { get; set; }
    }
}