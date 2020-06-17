using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SmartReport.BackEnd.CrossCuttingConcern.Entities;
using SmartReport.BackEnd.DataAccessLayer.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartReport.BackEnd.BusinessLogicLayer.DBInitializer
{
    public static class DatabaseInitializer
    {
        public static async System.Threading.Tasks.Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                IServiceProvider provider = scope.ServiceProvider;
                ApplicationDbContext context = provider.GetRequiredService<ApplicationDbContext>();
                UserManager<User> userManager = provider.GetRequiredService<UserManager<User>>();
                RoleManager<IdentityRole> roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

                await InitializeRolesAsync(userManager, roleManager);
            }
        }

        private static async System.Threading.Tasks.Task InitializeRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string name = "admin";
            string password = "a123456";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    Name = name
                };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }

            adminEmail = "first@gmail.com";
            name = "FirstUser";
            password = "a123456";
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    Name = name
                };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                }
            }
        }
    }
}
