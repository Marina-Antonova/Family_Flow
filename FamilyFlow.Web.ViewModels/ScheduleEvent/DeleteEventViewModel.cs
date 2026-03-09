namespace FamilyFlow.ViewModels.ScheduleEvent
{
    public class DeleteEventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string StartTime { get; set; } = null!;
        public string EndTime { get; set; } = null!;
        public int FamilyMemberId { get; set; }
    }
}
