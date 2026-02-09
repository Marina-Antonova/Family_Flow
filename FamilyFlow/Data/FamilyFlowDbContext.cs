using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FamilyMemberConfiguration());
            modelBuilder.ApplyConfiguration(new HouseTaskConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleEventConfiguration());

        }
    }
}
