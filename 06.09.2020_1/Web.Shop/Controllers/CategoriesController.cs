using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Web.Shop.Entities;
using Web.Shop.Models;
using Web.Shop.Repo.Interafaces;

namespace Web.Shop.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryRepo _categoryRepos;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoriesController(ILogger<CategoriesController> logger,
            ICategoryRepo categoryRepos, ApplicationDbContext context,
            IWebHostEnvironment env)
        {
            _logger = logger;
            _categoryRepos = categoryRepos;
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var list = _categoryRepos.GetAll().ToList();
            //await _categoryRepos.Add(
            //    new Category()
            //    {
            //        Id
            //    });
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryAddVM model)
        {
            if (ModelState.IsValid)
            {
                var serverPath = _env.ContentRootPath; //Directory.GetCurrentDirectory(); //_env.WebRootPath;
                var folerName = "Uploads";
                var path = Path.Combine(serverPath, folerName); //
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string ext = Path.GetExtension(model.Image.FileName);
                string fileName = Path.GetRandomFileName() + ext;

                string filePathSave = Path.Combine(path, fileName);

                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(filePathSave, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }

                Category category = new Category()
                {
                    Name = model.Name,
                    Image = fileName,
                    UrlSlug = model.UrlSlug,
                    DateCreate = DateTime.Now
                };
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            _categoryRepos.Delete(id);
        
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
          
            var category = _categoryRepos.GetById(id);

            if (category == null) return NotFound();

            return View(new CategoryEditVM
            {
                Name = category.Name,
                Image = category.Image,
                UrlSlug = category.UrlSlug
            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryEditVM model)
        {
            if (ModelState.IsValid)
            {
                var category = _categoryRepos.GetById(id);

                if (category == null) return NotFound();

                if (model.ImageUpload != null)
                {
                    var serverPath = _env.ContentRootPath; //Directory.GetCurrentDirectory(); //_env.WebRootPath;
                    var folerName = "Uploads";
                    var path = Path.Combine(serverPath, folerName); //
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string ext = Path.GetExtension(model.ImageUpload.FileName);
                    string fileName = Path.GetRandomFileName() + ext;

                    string filePathSave = Path.Combine(path, fileName);

                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(filePathSave, FileMode.Create))
                    {
                        await model.ImageUpload.CopyToAsync(fileStream);
                    }

                    category.Image = fileName;
                }

                category.Name = model.Name;
                category.UrlSlug = model.UrlSlug;
                category.DateModify = DateTime.Now;
              
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


    }
}
