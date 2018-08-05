using System.Collections.Generic;
using System.Linq;
using AgendaApp.Data;
using AgendaApp.Data.Entities;
using AgendaApp.Models;
using AgendaApp.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.Controllers
{

    [Authorize]
    public class ProfileController : Controller
    {

        private AgendaDbContext context;
        private IAgendaService agendaService;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private IMapper mapper;
        private string userId { get; set; }

        public ProfileController(AgendaDbContext context, IAgendaService agendaService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.agendaService = agendaService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            var user = httpContextAccessor.HttpContext.User;
            userId = userManager.GetUserId(user);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var alias = context.Users.Find(userId).Alias;
            var model = new DashboardVM { Alias = alias };

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var user = context.ApplicationUsers.Find(userId);
            var model = mapper.Map<EditProfileVM>(user);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditProfileVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = context.ApplicationUsers.Find(userId);
            user.Alias = model.Alias;
            context.SaveChanges();

            return RedirectToAction(nameof(ProfileController.Index));
        }

        [HttpGet]
        public IActionResult GetAgendas()
        {
            var model = new List<DashboardAgendaVM>();

            var agendas = agendaService.GetAll();
            foreach (var agenda in agendas)
            {
                var agendaVM = new DashboardAgendaVM
                {
                    Id = agenda.Id,
                    Title = agenda.Title,
                    CreatedAt = agenda.CreatedAt,
                    Deadline = agenda.Deadline,
                    CompletedItemsCount = agenda.Items.Where(o => o.Completed == true).Count(),
                    TotalItemsCount = agenda.Items.Count(),
                    Categories = new List<DashboardCategoryVM>()
                };
                var categories = agenda.Items.GroupBy(o => o.Category);
                foreach (var category in categories)
                {
                    var categoryVM = new DashboardCategoryVM
                    {
                        Title = category.FirstOrDefault().Category,
                        Items = category.Select(o => new DashboardItemVM
                        {
                            Id = o.Id,
                            Description = o.Description,
                            Completed = o.Completed
                        })
                        .ToList()
                    };
                    agendaVM.Categories.Add(categoryVM);
                }
                model.Add(agendaVM);
            }

            return PartialView("_Agendas", model);
        }

    }
}