using FamilyFlow.Data.Models;
using FamilyFlow.Data.Seeding.Interfaces;
using FamilyFlow.GCommon.Enums;
using Microsoft.AspNetCore.Identity;

namespace FamilyFlow.Data.Seeding
{
    public class IdentitySeeder : IIdentitySeeder
    {
        private readonly string[] applicationRoles =
        [
            "Admin",
            "User"
        ];

        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public IdentitySeeder(
            RoleManager<IdentityRole<Guid>> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task SeedRolesAsync()
        {
            foreach (string role in applicationRoles)
            {
                bool roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    IdentityRole<Guid> newRole = new(role);
                    IdentityResult result = await roleManager.CreateAsync(newRole);

                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create role: {role}");
                    }
                }
            }

            await SeedUserRolesAsync();
        }

        private async Task SeedUserRolesAsync()
        {
            await AssignRoleIfUserExistsAsync("mother@familyflow.com", "Admin");
            await AssignRoleIfUserExistsAsync("father@familyflow.com", "Admin");
            await AssignRoleIfUserExistsAsync("son@familyflow.com", "User");
            await AssignRoleIfUserExistsAsync("daughter@familyflow.com", "User");
        }

        private async Task AssignRoleIfUserExistsAsync(string email, string targetRole)
        {
            ApplicationUser? user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return;
            }

            if (await userManager.IsInRoleAsync(user, targetRole))
            {
                return;
            }

            IdentityResult result = await userManager.AddToRoleAsync(user, targetRole);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to add role '{targetRole}' to '{email}'.");
            }
        }
    }
}