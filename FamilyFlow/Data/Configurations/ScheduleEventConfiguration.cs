using FamilyFlow.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace FamilyFlow.Data.Configurations
{
    public class ScheduleEventConfiguration : IEntityTypeConfiguration<ScheduleEvent>
    {
        private static readonly IEnumerable<ScheduleEvent> Events = new List<ScheduleEvent>()
        {
           new ScheduleEvent
                {
                    Id = 1,
                    Title = "Family Meeting",
                    StartTime = new DateTime(2026, 2, 15, 18, 0, 0),
                    EndTime = new DateTime(2026, 2, 15, 19, 0, 0),
                    FamilyMemberId = 1
                },
                new ScheduleEvent
                {
                    Id = 2,
                    Title = "Grocery Shopping",
                    StartTime = new DateTime(2026, 2, 14, 10, 0, 0),
                    EndTime = new DateTime(2026, 2, 14, 11, 0, 0),
                    FamilyMemberId = 2
                },
                new ScheduleEvent
                {
                    Id = 3,
                    Title = "Doctor Appointment",
                    StartTime = new DateTime(2026, 7, 3, 14, 0, 0),
                    EndTime = new DateTime(2026, 7, 3, 15, 0, 0),
                    FamilyMemberId = 3,
                    AccompanyingAdultId = 1
                },
                new ScheduleEvent
                {
                    Id = 4,
                    Title = "Birthday Party",
                    StartTime = new DateTime(2026, 7, 4, 16, 0, 0),
                    EndTime = new DateTime(2026, 7, 4, 18, 0, 0),
                    FamilyMemberId = 4,
                    AccompanyingAdultId = 2
                }
        };
        public void Configure(EntityTypeBuilder<ScheduleEvent> entity)
        {
            entity
               .HasOne(se => se.FamilyMemberScheduleEvents)
               .WithMany(fm => fm.ScheduleEvents)
               .HasForeignKey(se => se.FamilyMemberId)
               .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasOne(se => se.AccompanyingAdult)
                .WithMany(fm => fm.AccompanyEvents)
                .HasForeignKey(se => se.AccompanyingAdultId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(Events);
        }
    }
}
