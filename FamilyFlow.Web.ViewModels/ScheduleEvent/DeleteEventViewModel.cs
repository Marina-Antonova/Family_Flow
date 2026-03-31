namespace FamilyFlow.Web.ViewModels.ScheduleEvent
{
    public class DeleteEventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string StartTime { get; set; } = null!;
        public string EndTime { get; set; } = null!;
        public int CreatorId { get; set; }
    }
}
