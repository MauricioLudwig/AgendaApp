using AgendaApp.Data;
using AgendaApp.Models;
using AgendaApp.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AgendaApp.Controllers
{

    [Authorize]
    public class ItemController : Controller
    {

        private AgendaDbContext context;
        private IItemService itemService;
        private IMapper mapper;

        public ItemController(AgendaDbContext context, IItemService itemService, IMapper mapper)
        {
            this.context = context;
            this.itemService = itemService;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Check(int id)
        {
            var item = context.Items.Find(id);
            item.Completed = !item.Completed;
            context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CreateItemVM model)
        {
            itemService.Create(model);
            return Ok();
        }

    }
}