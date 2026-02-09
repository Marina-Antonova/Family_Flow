using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FamilyFlow.Data.Models;


namespace FamilyFlow.Data.Configurations
{
    public class HouseTaskConfiguration : IEntityTypeConfiguration<HouseTask>
    {
        private readonly IEnumerable<HouseTask> Tasks = new List<HouseTask>()
        {
            new HouseTask
             {
                 Id = 1,
                 Title = "Clean the kitchen",
                 Description = "Wipe down counters and mop the floor",
                 DueDate = DateTime.Now.AddDays(2),
                 IsCompleted = false,
                 FamilyMemberId = 1
             },
             new HouseTask
             {
                 Id = 2,
                 Title = "Mow the lawn",
                 Description = "Mow the front and back yard",
                 DueDate = DateTime.Now.AddDays(3),
                 IsCompleted = false,
                 FamilyMemberId = 2
             },
             new HouseTask
             {
                 Id = 3,
                 Title = "Take out the trash",
                 Description = "Collect all trash and recycling and take to curb",
                 DueDate = DateTime.Now.AddDays(1),
                 IsCompleted = false,
                 FamilyMemberId = 3
             },
             new HouseTask
             {
                 Id = 4,
                 Title = "Wash the car",
                 Description = "Clean the exterior and interior of the car",
                 DueDate = DateTime.Now.AddDays(5),
                 IsCompleted = false,
                 FamilyMemberId = 1
             },
             new HouseTask
             {
                 Id = 5,
                 Title = "Organize the garage",
                 Description = "Sort and declutter items in the garage",
                 DueDate = DateTime.Now.AddDays(7),
                 IsCompleted = false,
                 FamilyMemberId = 2
             },
             new HouseTask
             {
                 Id = 6,
                 Title = "Grocery shopping",
                 Description = "Buy groceries for the week",
                 DueDate = DateTime.Now.AddDays(1),
                 IsCompleted = false,
                 FamilyMemberId = 3
             }
        };
        public void Configure(EntityTypeBuilder<HouseTask> entity)
        {

            entity.HasData(Tasks);
 
        }
    }
}
