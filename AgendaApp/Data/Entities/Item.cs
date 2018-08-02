using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaApp.Data.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(1024)]
        public string Description { get; set; }
        [StringLength(256)]
        public string Category { get; set; }
        public bool Completed { get; set; }
        public DateTime CreatedAt { get; set; }

        public int AgendaId { get; set; }
        public Agenda Agenda { get; set; }
    }
}