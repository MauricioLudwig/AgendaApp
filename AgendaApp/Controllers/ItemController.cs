using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.Controllers
{

    [Authorize]
    public class ItemController : Controller
    {

        private AgendaDbContext context;

        public ItemController(AgendaDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult Check(int id)
        {
            var item = context.Items.Find(id);
            item.Completed = !item.Completed;
            context.SaveChanges();
            return Ok();
        }

    }
}