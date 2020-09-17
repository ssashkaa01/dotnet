using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Shop.Entities;
using Web.Shop.Models;

namespace Web.Shop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly RoleManager<DbRole> _roleManager;
        private readonly IWebHostEnvironment _env;

        public AccountController(UserManager<DbUser> userManager,
            SignInManager<DbUser> signInManager,
            RoleManager<DbRole> roleManager,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        //await AuthenticateAsync(user.Email);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Дані вкажано не коректно");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            bool isEmailExist = await _userManager.FindByEmailAsync(model.Email) == null;
            //bool isEmailCorrect = model.Email != null && Regex.IsMatch(model.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmailExist)
            {
                ModelState.AddModelError("Email", "Така пошта уже є. Думай ...");
            }
            
            if (!isEmailExist)
            {
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var serverPath = _env.ContentRootPath; //Directory.GetCurrentDirectory(); //_env.WebRootPath;
                var folerName = "Uploads";
                var path = Path.Combine(serverPath, folerName); //
                if (!Directory.Exists(path))
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

                var user = new DbUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Image = fileName,
                };
                var result =  await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Щось пішло не так.");
                }
            }
            return View(model);
        }
    }
}
