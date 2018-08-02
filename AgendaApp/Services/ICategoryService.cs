using AgendaApp.Data.Entities;
using AgendaApp.ViewModels;
using AgendaApp.ViewModels.Category;
using System.Collections.Generic;

namespace AgendaApp.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Create(Category category);
        void Update(EditCategoryVM category);
        void Delete(int id);
    }
}