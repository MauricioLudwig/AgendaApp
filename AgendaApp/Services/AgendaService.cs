using AgendaApp.Data;
using AgendaApp.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public Agenda GetById(int id)
        {
            return context.Agendas.Find(id);
        }

        public IEnumerable<Agenda> GetAll()
        {
            return context.Agendas
                .Include(o => o.Items)
                .Where(o => o.ApplicationUserId == userId);
        }

        public IEnumerable<Agenda> GetAllOpen()
        {
            return context.Agendas
                .Include(o => o.Items)
                .Where(o => o.ApplicationUserId == userId && o.Archived == false);
        }

        public void Create(Agenda agenda)
        {
            agenda.ApplicationUserId = userId;
            agenda.CreatedAt = DateTime.Now;
            context.Agendas.Add(agenda);
            context.SaveChanges();
        }

        public void Remove(int id)
        {
            var agenda = context.Agendas.Find(id);
            context.Agendas.Remove(agenda);
            context.SaveChanges();
        }

        public void AddToArchive(int id)
        {
            var agenda = context.Agendas.Find(id);
            agenda.Archived = true;
            context.SaveChanges();
        }

    }
}