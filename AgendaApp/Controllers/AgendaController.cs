using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.Controllers
{
    public class AgendaController : Controller
    {

        private AgendaDbContext context;

        public AgendaController(AgendaDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Archive(int id)
        {
            return Ok();
        }

    }
}