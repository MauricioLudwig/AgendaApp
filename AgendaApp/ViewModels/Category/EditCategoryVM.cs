﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.ViewModels.Category
{
    public class EditCategoryVM
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50)]
        public string Title { get; set; }
    }
}
