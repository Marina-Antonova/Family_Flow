using FamilyFlow.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}
