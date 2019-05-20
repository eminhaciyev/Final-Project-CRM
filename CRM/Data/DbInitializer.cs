using CRM.Models.InnerModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data
{
    public class DbInitializer
    {
        public async static Task Seed(CRMdb _context, UserManager<User> userManager,
                                                       RoleManager<IdentityRole> roleManager,
                                                       IConfiguration configuration
                                                       )
        {
            await _context.Database.EnsureCreatedAsync();
            if (!await roleManager.RoleExistsAsync(StaticData.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(StaticData.Admin));
            }
            if (!await roleManager.RoleExistsAsync(StaticData.Member))
            {
                await roleManager.CreateAsync(new IdentityRole(StaticData.Member));
            }

            if (!await roleManager.RoleExistsAsync(StaticData.Moderator))
            {
                await roleManager.CreateAsync(new IdentityRole(StaticData.Moderator));
            }
            if (await userManager.FindByEmailAsync("admin2@gmail.com") == null)
            {
                var admin = new User()
                {
                    FullName = "admin adminov",
                    UserName = "admin2@gmail.com",
                    BirthDate = Convert.ToDateTime("02-02-2000"),
                    Email = "admin2@gmail.com"

                };
                var result = await userManager.CreateAsync(admin, configuration["AdminSettings:Password"]);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, StaticData.Admin);
                }
            }
        }
    }
}
