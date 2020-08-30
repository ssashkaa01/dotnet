namespace WebTestSecirity.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebTestSecirity.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebTestSecirity.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebTestSecirity.Models.ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context));

            // ������� ��� ����
            var roleAdmin = new IdentityRole { Name = "Admin" };
            var roleUser = new IdentityRole { Name = "User" };

            // ��������� ���� � ��
            roleManager.Create(roleAdmin);
            roleManager.Create(roleUser);

            string email = "admin@gmail.com";
            string password = "Qwerty1-";
            // ������� �������������
            var admin = new ApplicationUser { Email = email, UserName = email };
            var result = userManager.Create(admin, password);

            // ���� �������� ������������ ������ �������
            if (result.Succeeded)
            {
                // ��������� ��� ������������ ����
                userManager.AddToRole(admin.Id, roleAdmin.Name);
                userManager.AddToRole(admin.Id, roleUser.Name);
            }
        }
    }
}
