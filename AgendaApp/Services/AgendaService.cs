﻿using AgendaApp.Data;
using AgendaApp.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Services
{
    public class AgendaService : IAgendaService
    {

        private AgendaDbContext context;
        private string userId;

        public AgendaService(AgendaDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            var user = httpContextAccessor.HttpContext.User;
            userId = userManager.GetUserId(user);
        }

        public IEnumerable<Agenda> GetAll()
        {
            return context.Agendas.Where(o => o.ApplicationUserId == userId);
        }

        public void Create(Agenda agenda)
        {
            agenda.ApplicationUserId = userId;
            context.Agendas.Add(agenda);
            context.SaveChanges();
        }

    }
}