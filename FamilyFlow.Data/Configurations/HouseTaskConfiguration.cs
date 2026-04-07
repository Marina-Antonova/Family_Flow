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
                 Id = 3,
                 Title = "Clean the kitchen",
                 Description = "Wipe down counters and mop the floor",
                 DueDate = new DateTime(2026, 4, 12, 18, 0, 0),
                 IsCompleted = false,
                 FamilyMemberId = 11
             },
             new HouseTask
             {
                 Id = 4,
                 Title = "Mow the lawn",
                 Description = "Mow the front and back yard",
                 DueDate = new DateTime(2026, 4, 13, 17, 0, 0),
                 IsCompleted = false,
                 FamilyMemberId = 12
             },
             new HouseTask
             {
                 Id = 5,
                 Title = "Take out the trash",
                 Description = "Collect all trash and recycling and take to curb",
                 DueDate = new DateTime(2026, 4, 11, 19, 0, 0),
                 IsCompleted = false,
                 FamilyMemberId = 13
             },
             new HouseTask
             {
                 Id = 6,
                 Title = "Wash the car",
                 Description = "Clean the exterior and interior of the car",
                 DueDate = new DateTime(2026, 4, 15, 16, 0, 0),
                 IsCompleted = false,
                 FamilyMemberId = 12
             },
             new HouseTask
             {
                 Id = 7,
                 Title = "Organize the garage",
                 Description = "Sort and declutter items in the garage",
                 DueDate = new DateTime(2026, 4, 17, 15, 0, 0),
                 IsCompleted = false,
                 FamilyMemberId = 12
             },
             new HouseTask
             {
                 Id = 8,
                 Title = "Grocery shopping",
                 Description = "Buy groceries for the week",
                 DueDate = new DateTime(2026, 4, 11, 12, 0, 0),
                 IsCompleted = false,
                 FamilyMemberId = 11
             },
             new HouseTask
             {
                 Id = 9,
                 Title = "Laundry",
                 Description = "Wash and fold clothes",
                 DueDate = new DateTime(2026, 4, 12, 14, 0, 0),
                 IsCompleted = false,
                 FamilyMemberId = 14
             }
        };
        public void Configure(EntityTypeBuilder<HouseTask> entity)
        {

            entity.HasData(Tasks);
 
        }
    }
}
