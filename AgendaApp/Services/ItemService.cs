using AgendaApp.Data;
using AgendaApp.Data.Entities;
using AgendaApp.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Services
{
    public class ItemService : IItemService
    {

        private AgendaDbContext context;
        private IMapper mapper;

        public ItemService(AgendaDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<Item> GetAll(int id)
        {
            return context.Items.Where(o => o.AgendaId == id);
        }

        public void Create(CreateItemVM item)
        {
            var mappedItem = mapper.Map<Item>(item);
            mappedItem.CreatedAt = DateTime.Now;

            var agenda = context.Agendas.Find(item.AgendaId);
            agenda.Items.Add(mappedItem);

            context.SaveChanges();
        }

    }
}