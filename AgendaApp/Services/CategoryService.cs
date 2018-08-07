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
            var categories = context.Categories
                .Where(o => o.ApplicationUserId == userId)
                .OrderBy(o => o.Title);
            return categories;
        }

        public IEnumerable<string> GetAllValues()
        {
            return context.Categories
                .Where(o => o.ApplicationUserId == userId)
                .Select(o => o.Title)
                .OrderBy(o => o);
        }

        public Category GetById(int id)
        {
            var category = context.Categories.Find(id);
            return category;
        }

        public void Create(Category category)
        {
            category.ApplicationUserId = userId;
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void Update(EditCategoryVM category)
        {
            var selectedCategory = context.Categories.Find(category.Id);
            selectedCategory.Title = category.Title;
            context.SaveChanges();
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