using FamilyFlow.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace FamilyFlow.Data.Configurations
{
    public class ScheduleEventConfiguration : IEntityTypeConfiguration<ScheduleEvent>
    {
        private readonly IEnumerable<ScheduleEvent> Events = new List<ScheduleEvent>()
        {
            new ScheduleEvent
            {
                Id = 7,
                Title = "Morning Walk",
                StartTime = new DateTime(2026, 4, 12, 7, 0, 0),
                EndTime = new DateTime(2026, 4, 12, 8, 0, 0),
                AccompanyingAdultId = 11,
                CreatorId = 11
            },
            new ScheduleEvent
            {
                Id = 8,
                Title = "Family Meeting",
                StartTime = new DateTime(2026, 4, 13, 18, 30, 0),
                EndTime = new DateTime(2026, 4, 13, 19, 0, 0),
                AccompanyingAdultId = null,
                CreatorId = 12
            },
            new ScheduleEvent
            {
                Id = 9,
                Title = "Birthday Party",
                StartTime = new DateTime(2026, 4, 14, 10, 0, 0),
                EndTime = new DateTime(2026, 4, 14, 12, 0, 0),
                AccompanyingAdultId = 12,
                CreatorId = 13
            },
            new ScheduleEvent
            {
                Id = 10,
                Title = "Doctor Apointment",
                StartTime = new DateTime(2026, 4, 15, 11, 30, 0),
                EndTime = new DateTime(2026, 4, 15, 12, 30, 0),
                AccompanyingAdultId = 11,
                CreatorId = 14
            }
        };

        public void Configure(EntityTypeBuilder<ScheduleEvent> entity)
        {
            entity.HasData(Events);

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
