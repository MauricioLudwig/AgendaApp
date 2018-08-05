using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class EditProfileVM
    {
        [Required(ErrorMessage = "Alias is required")]
        [MaxLength(50, ErrorMessage = "Alias cannot exceed 50 characters")]
        public string Alias { get; set; }
    }
}