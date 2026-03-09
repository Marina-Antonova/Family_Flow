using System.ComponentModel.DataAnnotations;

namespace FamilyFlow.ViewModels.FamilyMember
{
    public class DetailsScheduleEventsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
