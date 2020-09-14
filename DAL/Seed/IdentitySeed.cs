using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace DAL.Seed
{
    public partial class TestSeed
    {
        public static void SeedIdentity(UserManager<IdentityUser> userManager)
        {
            if (!userManager.Users.Any())
                SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            foreach (var user in Users)
            {
                userManager.CreateAsync(user, "f1test2018").Wait();
            }
        }

        static List<IdentityUser> Users => new List<IdentityUser>
        {
            new IdentityUser { UserName = "admin", Email = "admin@encosoft.hu", EmailConfirmed = true }
        };
    }
}