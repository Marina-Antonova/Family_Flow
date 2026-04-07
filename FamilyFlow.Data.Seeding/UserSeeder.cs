using FamilyFlow.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FamilyFlow.Data.Seeding
{
    public static class UserSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            PasswordHasher<ApplicationUser> hasher = new();

            ApplicationUser motherUser = CreateUser(
                Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                "mother@familyflow.com",
                "Mother123!",
                hasher);

            ApplicationUser fatherUser = CreateUser(
                Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                "father@familyflow.com",
                "Father123!",
                hasher);

            ApplicationUser sonUser = CreateUser(
                Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                "son@familyflow.com",
                "Son123!",
                hasher);

            ApplicationUser daughterUser = CreateUser(
                Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                "daughter@familyflow.com",
                "Daughter123!",
                hasher);

            modelBuilder.Entity<ApplicationUser>().HasData(
                motherUser,
                fatherUser,
                sonUser,
                daughterUser);
        }

        private static ApplicationUser CreateUser(
            Guid id,
            string email,
            string password,
            PasswordHasher<ApplicationUser> hasher)
        {
            ApplicationUser user = new()
            {
                Id = id,
                UserName = email,
                NormalizedUserName = email.ToUpperInvariant(),
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            user.PasswordHash = hasher.HashPassword(user, password);
            return user;
        }
    }
}
