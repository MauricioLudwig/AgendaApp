﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class EditAgendaIndexVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }
        public List<string> Categories { get; set; }
    }
}