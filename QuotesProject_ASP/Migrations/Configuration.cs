namespace QuotesProject_ASP.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QuotesProject_ASP.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }


        protected override void Seed(QuotesProject_ASP.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Administrator" };
                manager.Create(role);
                context.SaveChanges();
            }

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "User" };
                manager.Create(role);
                context.SaveChanges();
            }

            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                context.Users.AddOrUpdate(u => u.Email, new ApplicationUser {
                    Email ="admin@ww.pl",
                    UserName = "admin@ww.pl",
                    BirthDate =DateTime.Now
                });
                context.SaveChanges();
                manager.AddPassword(manager.Users.ToList()[0].Id, "Wojtek/123");
                manager.AddToRole(manager.Users.ToList()[0].Id, "Administrator");
                context.SaveChanges();
            }
           
        }
    }
}
