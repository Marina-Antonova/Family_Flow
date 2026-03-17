namespace FamilyFlow.Web.ViewModels.Schedule
{
    public class ScheduleItemViewModel
    {
        public string FamilyMemberName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Type { get; set; } = null!;
        public DateTime? StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? AccompanyingAdultName { get; set; }

    }
}
