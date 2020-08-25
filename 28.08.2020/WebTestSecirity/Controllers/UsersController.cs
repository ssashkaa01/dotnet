using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTestSecirity.Models;

namespace WebTestSecirity.Controllers
{
    [Authorize(Roles = "Admin")]
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
        
    }
}