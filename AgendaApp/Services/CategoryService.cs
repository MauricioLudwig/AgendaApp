using AgendaApp.Data;
using AgendaApp.Data.Entities;
using AgendaApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Services
{

    public class CategoryService : ICategoryService
    {

        private AgendaDbContext context;
        private string userId;

        public CategoryService(AgendaDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            var user = httpContextAccessor.HttpContext.User;
            userId = userManager.GetUserId(user);
        }

        public IEnumerable<Category> GetAll()
        {
            var categories = context.Categories.Where(o => o.ApplicationUserId == userId);
            return categories;
        }

        public Category GetById(int id)
        {
            var category = context.Categories.Find(id);
            return category;
        }

        public void Create(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void Update(EditCategoryVM category)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var category = context.Categories.Find(id);

            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

    }
}