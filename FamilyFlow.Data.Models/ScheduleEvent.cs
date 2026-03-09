using System.ComponentModel.DataAnnotations;
using static FamilyFlow.GCommon.ValidationConstants.ScheduleEvent;

namespace FamilyFlow.Data.Models
{
    public class ScheduleEvent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        
        public int FamilyMemberId { get; set; }

        public virtual FamilyMember FamilyMemberScheduleEvents { get; set; } = null!;


        public int? AccompanyingAdultId { get; set; }

        public virtual FamilyMember? AccompanyingAdult { get; set; }
    }
}

