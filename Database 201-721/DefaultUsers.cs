using Database_201_721.Models;
using Microsoft.AspNetCore.Identity;

namespace Database_201_721
{
    public class DefaultUsers
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@mail.ru";
            string adminPassword = "12341234";

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            if (await roleManager.FindByNameAsync("student") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("student"));
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User {Email = adminEmail, Year = 2002, UserName = adminEmail, ChangeRequestYear = null};
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
