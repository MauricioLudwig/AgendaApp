using System.Collections.Generic;
using System.Linq;
using AgendaApp.Data;
using AgendaApp.Data.Entities;
using AgendaApp.Models;
using AgendaApp.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.Controllers
{

    [Authorize]
    public class AgendaController : Controller
    {

        private AgendaDbContext context;
        private IAgendaService agendaService;
        private ICategoryService categoryService;
        private IItemService itemService;
        private IMapper mapper;

        public AgendaController(AgendaDbContext context, IAgendaService agendaService, ICategoryService categoryService, IItemService itemService, IMapper mapper)
        {
            this.context = context;
            this.agendaService = agendaService;
            this.categoryService = categoryService;
            this.itemService = itemService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetUserAgendas()
        {
            var model = agendaService.GetAll()
                .Select(o => mapper.Map<AgendaVM>(o))
                .OrderByDescending(o => o.CreatedAt)
                .ToList();

            return PartialView("_Agendas", model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAgendaVM model)
        {
            agendaService.Create(mapper.Map<Agenda>(model));

            return Ok();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var agenda = agendaService.GetById(id);

            var model = new EditAgendaIndexVM
            {
                Id = agenda.Id,
                Title = agenda.Title,
                Deadline = agenda.Deadline,
                Categories = categoryService.GetAllValues().ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditAgendaIndexVM model)
        {
            var agenda = context.Agendas.Find(model.Id);
            agenda.Title = model.Title;
            agenda.Deadline = model.Deadline;
            context.SaveChanges();

            return RedirectToAction(nameof(AgendaController.Edit), model.Id);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            agendaService.Remove(id);
            return RedirectToAction(nameof(AgendaController.Index));
        }

        [HttpPost]
        public IActionResult GetItems(int id)
        {
            var model = mapper.Map<List<ItemVM>>(itemService.GetAll(id));
            return PartialView("_Items", model);
        }

        [HttpPost]
        public IActionResult Agenda(int id)
        {
            return Ok();
        }

    }
}