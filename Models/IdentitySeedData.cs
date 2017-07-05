using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace WebApplication7.Models
{
    public class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "T@n537198";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<ApplicationUser> userManager =
                app.ApplicationServices.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new ApplicationUser {UserName = adminUser};
                var result = await userManager.CreateAsync(user, adminPassword);
                if (result.Succeeded)
                    Console.WriteLine("Create user" + adminUser + "successfully");
                else
                    Console.WriteLine(result.Errors.ToString());
            }
        }
    }
}