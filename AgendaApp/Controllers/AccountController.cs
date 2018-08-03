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
        private string userId { get; set; }

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
        public IActionResult Login(string returnUrl = "")
        {
            var alreadySignedIn = signInManager.IsSignedIn(User);

            if (alreadySignedIn)
                return RedirectToAction("", "");

            var model = new LoginVM
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userNameExists = await userManager.FindByNameAsync(model.UserName);
            var emailExists = await userManager.FindByEmailAsync(model.UserName);

            // check username and password sign-in
            if (userNameExists != null)
            {
                var result = await signInManager.PasswordSignInAsync(userNameExists, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    else
                        return RedirectToAction("", "");
                }
            }

            // Check email and password sign-in
            if (emailExists != null)
            {
                var result = await signInManager.PasswordSignInAsync(emailExists, model.Password, model.RememberMe, false);
                
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    else
                        return RedirectToAction("", "");
                }
            }

            ModelState.AddModelError("", "Incorrect username or password");
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            var alreadySignedIn = signInManager.IsSignedIn(User);
            if (alreadySignedIn)
                return RedirectToAction("", "");

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
                return RedirectToAction("", "");
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
            return RedirectToAction("", "");
        }

    }

}
