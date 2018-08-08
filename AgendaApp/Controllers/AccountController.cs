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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            var alreadySignedIn = signInManager.IsSignedIn(User);
            if (alreadySignedIn)
                return RedirectToAction("Index", "Profile");

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userNameExists = await userManager.FindByNameAsync(model.UserName);
            if (userNameExists != null)
                ModelState.AddModelError("", "The username you have entered already exists.");

            var emailExists = await userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                ModelState.AddModelError("", "The email you have entered already exists.");

            if (userNameExists != null || emailExists != null)
                return View();

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }

}