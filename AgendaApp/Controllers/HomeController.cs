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

    }
}