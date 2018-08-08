using AgendaApp.Data.Entities;
using AgendaApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AgendaApp.Controllers
{
    public class HomeController : Controller
    {

        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #region Sign in
        [HttpGet]
        public IActionResult Index()
        {
            var alreadySignedIn = signInManager.IsSignedIn(User);

            if (alreadySignedIn)
                return RedirectToAction("Index", "Profile");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginVM model)
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
                    return RedirectToAction("Index", "Profile");
            }

            // Check email and password sign-in
            if (emailExists != null)
            {
                var result = await signInManager.PasswordSignInAsync(emailExists, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Profile");
            }

            ModelState.AddModelError("", "Incorrect username or password");
            return View(model);
        }
        #endregion

        #region Sign up
        [HttpGet]
        public IActionResult Register()
        {
            var alreadySignedIn = signInManager.IsSignedIn(User);

            if (alreadySignedIn)
                return RedirectToAction("Index", "Profile");

            return View();
        }

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
        #endregion

    }
}