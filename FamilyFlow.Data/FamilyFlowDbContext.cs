using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FamilyFlow.Data.Models;
using FamilyFlow.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using FamilyFlow.Data.Seeding;

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

            modelBuilder.ApplyConfiguration(new FamilyConfiguration());
            modelBuilder.ApplyConfiguration(new FamilyMemberConfiguration());
            modelBuilder.ApplyConfiguration(new HouseTaskConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleEventConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleEventParticipantConfiguration());

            UserSeeder.Seed(modelBuilder);
        }
    }
}
