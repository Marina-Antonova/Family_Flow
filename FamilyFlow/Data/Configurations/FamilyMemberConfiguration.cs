using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FamilyFlow.Data.Models;
using FamilyFlow.Data.Models.Enums;

namespace FamilyFlow.Data.Configurations
{
    public class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMember>
    {
        private readonly IEnumerable<FamilyMember> Members = new List<FamilyMember>()
        {
             new FamilyMember { Id = 1, Name = "Alice", Role = FamilyRole.Mother, Age = 40 },
             new FamilyMember { Id = 2, Name = "Bob", Role = FamilyRole.Father, Age = 42 },
             new FamilyMember { Id = 3, Name = "Charlie", Role = FamilyRole.Son, Age = 12 },
             new FamilyMember { Id = 4, Name = "Daisy", Role = FamilyRole.Daughter, Age = 10 }
        };
        public void Configure(EntityTypeBuilder<FamilyMember> entity)
        {
            entity.HasData(Members);
        } 
    }
}
