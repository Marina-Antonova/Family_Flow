using FamilyFlow.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyFlow.Data.Configurations
{
    public class ScheduleEventParticipantConfiguration : IEntityTypeConfiguration<ScheduleEventParticipant>
    {
        private readonly IEnumerable<ScheduleEventParticipant> Participants = new List<ScheduleEventParticipant>()
        {
            new ScheduleEventParticipant
            {
                ScheduleEventId = 8,
                FamilyMemberId = 12
            },
            new ScheduleEventParticipant
            {
                ScheduleEventId = 8,
                FamilyMemberId = 13
            },
            new ScheduleEventParticipant
            {
                ScheduleEventId = 8,
                FamilyMemberId = 14
            }
        };

        public void Configure(EntityTypeBuilder<ScheduleEventParticipant> entity)
        {
            entity.HasData(Participants);

            entity
                .HasKey(sep => new { sep.ScheduleEventId, sep.FamilyMemberId });

            entity
                .HasOne(sep => sep.ScheduleEvent)
                .WithMany(e => e.Participants)
                .HasForeignKey(sep => sep.ScheduleEventId);

            entity
                .HasOne(sep => sep.FamilyMember)
                .WithMany(fm => fm.EventParticipations)
                .HasForeignKey(sep => sep.FamilyMemberId);
        }
    }
}
