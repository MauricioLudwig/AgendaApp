using System.Collections.Generic;
using AgendaApp.Data.Entities;
using AgendaApp.Services;
using AgendaApp.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.Controllers
{

    [Authorize]
    public class CategoryController : Controller
    {

        private ICategoryService categoryService;
        private IMapper mapper;
        private string userId;

        public CategoryController(ICategoryService categoryService, IMapper mapper, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
            var user = httpContextAccessor.HttpContext.User;
            userId = userManager.GetUserId(user);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = categoryService.GetAll();
            var model = mapper.Map<List<CategoryIndexVM>>(categories);

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddCategoryVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var category = mapper.Map<Category>(model);
            categoryService.Create(category);

            return RedirectToAction(nameof(CategoryController.Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = categoryService.GetById(id);
            var model = mapper.Map<EditCategoryVM>(category);

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(EditCategoryVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            categoryService.Update(model);

            return RedirectToAction(nameof(CategoryController.Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            categoryService.Delete(id);

            return RedirectToAction(nameof(CategoryController.Index));
        } 

    }
}