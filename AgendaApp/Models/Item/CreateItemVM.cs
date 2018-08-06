using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class CreateItemVM
    {
        public int AgendaId { get; set; }
        [Required]
        [StringLength(1024)]
        public string Description { get; set; }
        [StringLength(256)]
        public string Category { get; set; }
    }
}