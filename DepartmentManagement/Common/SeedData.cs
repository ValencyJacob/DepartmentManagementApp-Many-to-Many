using DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DepartmentManagement.Common
{
    public class SeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedData(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (_context.Database.GetPendingMigrations().Count() > 0)
            {
                _context.Database.Migrate();
            }

            if (_context.Roles.Any(x => x.Name == Common.AdminRole))
                return;

            _roleManager.CreateAsync(new IdentityRole(Common.AdminRole))
                .GetAwaiter().GetResult();

            _roleManager.CreateAsync(new IdentityRole(Common.UserRole))
                .GetAwaiter().GetResult();

            _userManager.CreateAsync(new IdentityUser
            {
                UserName = "admin@mail.com",
                Email = "admin@mail.com",
                EmailConfirmed = true
            }, "111111Test$").GetAwaiter().GetResult();

            IdentityUser user = _context.Users.FirstOrDefault(x => x.Email == "admin@mail.com");

            _userManager.AddToRoleAsync(user, Common.AdminRole).GetAwaiter().GetResult();
        }
    }
}
