using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(256)]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(256)]
        public string Alias { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}