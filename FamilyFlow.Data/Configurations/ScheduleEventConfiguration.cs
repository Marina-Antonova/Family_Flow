using FamilyFlow.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace FamilyFlow.Data.Configurations
{
    public class ScheduleEventConfiguration : IEntityTypeConfiguration<ScheduleEvent>
    {
        public void Configure(EntityTypeBuilder<ScheduleEvent> entity)
        {
            entity
               .HasOne(se => se.AccompanyingAdult)
               .WithMany(fm => fm.AccompanyEvents)
               .HasForeignKey(se => se.AccompanyingAdultId)
               .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasMany(se => se.Participants)
                .WithOne(p => p.ScheduleEvent)
                .HasForeignKey(p => p.ScheduleEventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
