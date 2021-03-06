﻿using AgendaApp.Data.Entities;
using AgendaApp.Models;
using System.Collections.Generic;

namespace AgendaApp.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        IEnumerable<string> GetAllValues();
        Category GetById(int id);
        void Create(Category category);
        void Update(EditCategoryVM category);
        void Delete(int id);
    }
}