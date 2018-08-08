using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaApp.Data;
using AgendaApp.Data.Entities;
using AgendaApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {

        private AgendaDbContext context;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private IMapper mapper;
        private string userId;

        public AccountController(AgendaDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            var user = httpContextAccessor.HttpContext.User;
            userId = userManager.GetUserId(user);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }

}