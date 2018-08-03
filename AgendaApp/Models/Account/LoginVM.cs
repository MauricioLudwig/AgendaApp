using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username or email is required")]
        [Display(Name = "Username or email")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}