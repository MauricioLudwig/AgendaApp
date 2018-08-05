using AgendaApp.Data.Entities;
using AgendaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Services
{
    public interface IAgendaService
    {
        IEnumerable<Agenda> GetAll();
        void Create(Agenda agenda);
    }
}