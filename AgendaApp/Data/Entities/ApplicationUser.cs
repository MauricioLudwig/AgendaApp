using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgendaApp.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(256)]
        public string Alias { get; set; }

        public List<Agenda> Agendas { get; set; }
        public List<Category> Categories { get; set; }
    }
}