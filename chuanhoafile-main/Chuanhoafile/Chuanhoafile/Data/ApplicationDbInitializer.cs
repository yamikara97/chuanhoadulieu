
using Chuanhoafile.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chuanhoafile.Data
{
    public class ApplicationDbInitializer
    {
        public static void SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
        {
            string[] roles = new string[] { "Admin", "User", ""};

            foreach (var role in roles)
            {
                if (roleManager.FindByNameAsync(role).Result == null)
                {
                    var result = roleManager.CreateAsync(new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = role }).Result;
                }
            }
        }
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                var adminUser = new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com    ",
                    FullName = "Administrator",
                    IsAdmin = true
                };

                var result = userManager.CreateAsync(adminUser, "Admin@123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                }
            }
        }
    }
}
