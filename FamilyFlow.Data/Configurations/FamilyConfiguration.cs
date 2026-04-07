using FamilyFlow.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyFlow.Data.Configurations
{
    public class FamilyConfiguration : IEntityTypeConfiguration<Family>
    {
        private readonly IEnumerable<Family> Family = new List<Family>()
        {
            new Family
            {
                Id = 10,
                Name = "Anderson",
                UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
            }
        };
        public void Configure(EntityTypeBuilder<Family> entity)
        {
            entity.HasData(Family);
        }
    }
}
