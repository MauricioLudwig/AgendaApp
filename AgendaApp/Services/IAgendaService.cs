﻿using AgendaApp.Data.Entities;
using AgendaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Services
{
    public interface IAgendaService
    {
        Agenda GetById(int id);
        IEnumerable<Agenda> GetAll();
        void Create(Agenda agenda);
        void Remove(int id);
    }
}