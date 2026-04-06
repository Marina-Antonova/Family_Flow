using FamilyFlow.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyFlow.Data.Configurations
{
    public class ScheduleEventParticipantConfiguration : IEntityTypeConfiguration<ScheduleEventParticipant>
    {
        public void Configure(EntityTypeBuilder<ScheduleEventParticipant> entity)
        {
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