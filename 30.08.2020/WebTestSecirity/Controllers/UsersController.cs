using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebTestSecirity.Entities;
using WebTestSecirity.Models;

namespace WebTestSecirity.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private ApplicationUserManager _userManager;

        public UsersController()
        {
        }

        public UsersController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Users

        public ActionResult Index()
        {
            var list = UserManager.Users
                .Select(u => new UserVM
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.UserProfile.FirstName
                }).ToList();

            return View(list);
        }

        // GET: /Account/Register
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserProfile userProfile = new UserProfile()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Patronymic = model.Patronymic,
                    Gender = Gender.Female,
                    Hobby = model.Hobby
                };
                var user = new ApplicationUser
                {
                    EmailConfirmed = true,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.Telephone,
                    UserProfile = userProfile
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Users");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}