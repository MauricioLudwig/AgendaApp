using AgendaApp.Data.Entities;
using AgendaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Services
{
    public interface IItemService
    {
        void Create(CreateItemVM item);
        void Delete(int id);
        IEnumerable<Item> GetAll(int id);
    }
}