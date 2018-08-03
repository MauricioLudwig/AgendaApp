using AgendaApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Services
{
    public interface IAgendaService
    {
        void Create(Agenda agenda);
    }
}