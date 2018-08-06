using AgendaApp.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.Controllers
{

    [Authorize]
    public class ItemController : Controller
    {

        private AgendaDbContext context;
        private IMapper mapper;

        public ItemController(AgendaDbContext context, IMapper mapper)
        {
            this.context = context;
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

    }
}