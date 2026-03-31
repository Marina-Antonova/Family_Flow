using System.ComponentModel.DataAnnotations;
using static FamilyFlow.GCommon.ValidationConstants.ScheduleEvent;

namespace FamilyFlow.Web.ViewModels.ScheduleEvent
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
        public DateTime StartTime { get; set; } = DateTime.Now;

        [Required]
        public DateTime EndTime { get; set; } = DateTime.Now.AddHours(1);

        public int? AccompanyingAdultId { get; set; }

        public ICollection<CreateEditAdultViewModel>? Adults { get; set; }
            = new List<CreateEditAdultViewModel>();

        public ICollection<CreateEditAdultViewModel>? Members { get; set; }
            = new List<CreateEditAdultViewModel>();

        public List<int> SelectedMemberIds { get; set; } = new();

        public int CreatorId { get; set; }
    }

}
