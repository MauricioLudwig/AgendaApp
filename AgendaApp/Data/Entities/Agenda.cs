using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgendaApp.Data.Entities
{
    public class Agenda
    {
        [Key]
        public int Id { get; set; }
        [StringLength(256)]
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Deadline { get; set; }
        public bool Archived { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public List<Item> Items { get; set; }
    }
}