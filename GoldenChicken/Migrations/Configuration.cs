namespace GoldenChicken.Migrations
{
    using GoldenChicken.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GoldenChicken.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GoldenChicken.Models.ApplicationDbContext context)
        {
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Master" };
                manager.Create(role);
            }
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "master" , Email= "goldenchickenfoods@gmail.com" , EmailConfirmed=true };

                manager.Create(user, "master@00");
                manager.AddToRole(user.Id, "Master");
            }

        }
    }
}
