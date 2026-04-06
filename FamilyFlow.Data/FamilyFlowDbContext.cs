using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FamilyFlow.Data.Models;
using FamilyFlow.Data.Configurations;
using Microsoft.AspNetCore.Identity;

namespace FamilyFlow.Data
{
    public class FamilyFlowDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
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
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FamilyMemberConfiguration());
            modelBuilder.ApplyConfiguration(new HouseTaskConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleEventConfiguration());

            modelBuilder.Entity<FamilyMember>()
                .HasOne(fm => fm.User)
                .WithMany(u => u.UserFamilyMembers)
                .HasForeignKey(fm => fm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FamilyMember>()
                .HasOne(fm => fm.LinkedUser)
                .WithOne(u => u.LinkedFamilyMember)
                .HasForeignKey<FamilyMember>(fm => fm.LinkedUserId)
                .OnDelete(DeleteBehavior.Restrict);

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
