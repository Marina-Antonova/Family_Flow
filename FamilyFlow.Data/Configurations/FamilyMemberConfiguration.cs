using FamilyFlow.Data.Models;
using FamilyFlow.GCommon.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyFlow.Data.Configurations
{
    public class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMember>
    {
        private readonly IEnumerable<FamilyMember> Members = new List<FamilyMember>()
        {
                new FamilyMember
                {
                    Id = 11,
                    Name = "Alice",
                    Role = FamilyRole.Mother,
                    Age = 40,
                    FamilyId = 10,
                    UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    LinkedUserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
                },
                new FamilyMember
                {
                    Id = 12,
                    Name = "Bob",
                    Role = FamilyRole.Father,
                    Age = 42,
                    FamilyId = 10,
                    UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    LinkedUserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")
                },
                new FamilyMember
                {
                    Id = 13,
                    Name = "Charlie",
                    Role = FamilyRole.Son,
                    Age = 6,
                    FamilyId = 10,
                    UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    LinkedUserId = null
                },
                new FamilyMember
                {
                    Id = 14,
                    Name = "Daisy",
                    Role = FamilyRole.Daughter,
                    Age = 10,
                    FamilyId = 10,
                    UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    LinkedUserId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd")
                },
                new FamilyMember
                {
                    Id = 15,
                    Name = "Elise",
                    Role = FamilyRole.Grandmother,
                    Age = 56,
                    FamilyId = 10,
                    UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    LinkedUserId = null
                },
        };

        public void Configure(EntityTypeBuilder<FamilyMember> entity)
        {
            entity.HasData(Members);

            entity
              .HasOne(fm => fm.Family)
              .WithMany(f => f.Members)
              .HasForeignKey(fm => fm.FamilyId)
              .OnDelete(DeleteBehavior.Restrict);

            entity
               .HasOne(fm => fm.User)
               .WithMany(u => u.UserFamilyMembers)
               .HasForeignKey(fm => fm.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasOne(fm => fm.LinkedUser)
                .WithOne(u => u.LinkedFamilyMember)
                .HasForeignKey<FamilyMember>(fm => fm.LinkedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        } 
    }
}