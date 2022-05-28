using CourseWork.Models;
using Microsoft.AspNetCore.Identity;

namespace CourseWork.Database;

public static class IdentitySeedData
{
    public static void InitializeRoles(IServiceProvider serviceProvider, IConfiguration configuration) {
        InitializeRolesAsync(serviceProvider, configuration).Wait();
    }

    private static async Task InitializeRolesAsync(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        serviceProvider = serviceProvider.CreateScope().ServiceProvider;
        UserManager<User> userManager =
            serviceProvider.GetRequiredService<UserManager<User>>();
        RoleManager<IdentityRole> roleManager =
            serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        if (await roleManager.FindByNameAsync("student") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("student"));
        }
        if (await roleManager.FindByNameAsync("tutor") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("tutor"));
        }
        
        string username = configuration["Data:AdminUser:Name"] ?? "admin";
        string email = configuration["Data:AdminUser:Email"] ?? "admin@example.com";
        string password = configuration["Data:AdminUser:Password"] ?? "root";
        string role = configuration["Data:AdminUser:Role"] ?? "admin";
        
        if (await userManager.FindByNameAsync(username) == null)
        {
            if (await roleManager.FindByNameAsync(role) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            User user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = username,
                Email = email
            };
            
            IdentityResult result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}