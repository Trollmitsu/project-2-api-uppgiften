using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Project_2_Mvc_homepage.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DataInitializer(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public void SeedData()
        {
            _context.Database.Migrate();
            
            SeedRoles();
            SeedUsers();
        }
        private void SeedUsers()
        {
            CreateRolesIfNotExists("Danish.gill@hotmail.com", "Hejsan123#",
                new[] { "Admin" });

        }

        private void CreateRolesIfNotExists(string email, string password, string[] roles)
        {
            if (_userManager.FindByEmailAsync(email).Result != null) return;

            var user = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };
            _userManager.CreateAsync(user, password).Wait();
            _userManager.AddToRolesAsync(user, roles).Wait();
        }

        private void SeedRoles()
        {
            CreateRolesIfNotExists("Admin");
            
        }

        private void CreateRolesIfNotExists(string rolename)
        {
            if (_context.Roles.Any(r => r.Name == rolename))
                return;
            _context.Roles.Add(new IdentityRole { Name = rolename, NormalizedName = rolename });
            _context.SaveChanges();
        }

    }
}
