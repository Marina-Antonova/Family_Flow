using FamilyFlow.Data.Models;

namespace FamilyFlow.ViewModels.FamilyMember
{
    public class AllFamilyMembersViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string RoleImagePath { get; set; } = null!;
        public int Age { get; set; }
        public int? HouseTasksCount { get; set; }
        public int? ScheduleEventsCount { get; set; }

    }
}
