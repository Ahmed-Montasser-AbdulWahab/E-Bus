using E_Bus.Entities.DbContext;
using E_Bus.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace E_Bus.Presentation.SeedDataHelpers
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider,string adminUser,string adminPassword)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {

                var adminID = await EnsureUser(serviceProvider, adminPassword, adminUser);
                await EnsureRole(serviceProvider, adminID, Constants.Super_Admin.ToString());


            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            
                var user = await userManager.FindByNameAsync(UserName);
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = UserName
                    };
                    var result = await userManager.CreateAsync(user, testUserPw);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Super Admin not created.");
                    }

                }
                return user.Id.ToString();
            
            
            
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new ApplicationRole() { Name = role });
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            //if (userManager == null)
            //{
            //    throw new Exception("userManager is null");
            //}

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}
