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
        private IMapper mapper;

        public AgendaController(AgendaDbContext context, IAgendaService agendaService, IMapper mapper)
        {
            this.context = context;
            this.agendaService = agendaService;
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

            return View();
        }

        [HttpPost]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agenda(int id)
        {
            return Ok();
        }

    }
}