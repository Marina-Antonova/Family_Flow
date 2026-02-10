using System.ComponentModel.DataAnnotations;
using static FamilyFlow.Common.ValidationConstants.ScheduleEvent;

namespace FamilyFlow.ViewModels.ScheduleEvent
{
    public class CreateEditEventViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public int FamilyMemberId { get; set; }
    }

}
