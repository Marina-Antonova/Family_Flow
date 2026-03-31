namespace FamilyFlow.Data.Models
{
    public class ScheduleEventParticipant
    {
        public int ScheduleEventId { get; set; }
        public ScheduleEvent ScheduleEvent { get; set; } = null!;

        public int FamilyMemberId { get; set; }
        public FamilyMember FamilyMember { get; set; } = null!;
    }
}
