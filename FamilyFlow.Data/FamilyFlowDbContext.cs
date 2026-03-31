using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FamilyFlow.Data.Models;
using FamilyFlow.Data.Configurations;

namespace FamilyFlow.Data
{
    public class FamilyFlowDbContext : IdentityDbContext
    {
        public FamilyFlowDbContext(DbContextOptions<FamilyFlowDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<FamilyMember> FamilyMembers { get; set; } = null!;
        public virtual DbSet<HouseTask> HouseTasks { get; set; } = null!;
        public virtual DbSet<ScheduleEvent> ScheduleEvents { get; set; } = null!;
        public virtual DbSet<Family> Families { get; set; } = null!;
        public virtual DbSet<ScheduleEventParticipant> ScheduleEventParticipants { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FamilyMemberConfiguration());
            modelBuilder.ApplyConfiguration(new HouseTaskConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleEventConfiguration());

            modelBuilder.Entity<ScheduleEventParticipant>()
                .HasKey(sep => new { sep.ScheduleEventId, sep.FamilyMemberId });

            modelBuilder.Entity<ScheduleEventParticipant>()
                .HasOne(sep => sep.ScheduleEvent)
                .WithMany(e => e.Participants)
                .HasForeignKey(sep => sep.ScheduleEventId);

            modelBuilder.Entity<ScheduleEventParticipant>()
                .HasOne(sep => sep.FamilyMember)
                .WithMany(fm => fm.EventParticipations)
                .HasForeignKey(sep => sep.FamilyMemberId);
        }
    }
}
