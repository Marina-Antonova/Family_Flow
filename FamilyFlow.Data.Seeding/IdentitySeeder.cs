using FamilyFlow.Data.Seeding.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FamilyFlow.Data.Seeding
{
    public class IdentitySeeder : IIdentitySeeder
    {
        private readonly string[] ApplicationRoles = new[]
        {
            "Admin",
            "User"
        };

        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        public IdentitySeeder(RoleManager<IdentityRole<Guid>> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task SeedRolesAsync()
        {
            foreach (string role in ApplicationRoles)
            {
                bool roleExists = await roleManager.RoleExistsAsync(role);

                Console.WriteLine($"Checking role: {role} Exists: {roleExists}");

                if (!roleExists)
                {
                    IdentityRole<Guid> newRole = new IdentityRole<Guid>(role);
                    IdentityResult result = await roleManager.CreateAsync(newRole);
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create role: {role}");
                    }
                }
            }
        }
    }
}
