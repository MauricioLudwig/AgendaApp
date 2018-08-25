using AgendaApp.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Helpers
{

    public class Validators
    {

        private UserManager<ApplicationUser> userManager;
        private string currentUserId;

        public Validators(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            var user = httpContextAccessor.HttpContext.User;
            currentUserId = userManager.GetUserId(user);
        }

        public bool ValidateUserId(string userId)
        {
            return string.Equals(currentUserId, userId);
        }

    }
}