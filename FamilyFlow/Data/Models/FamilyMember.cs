using System.ComponentModel.DataAnnotations;
using FamilyFlow.Data.Models.Enums;
using static FamilyFlow.Common.ValidationConstants.FamilyMember;

namespace FamilyFlow.Data.Models
{
    public class FamilyMember
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public FamilyRole Role { get; set; }

        public int Age { get; set; }

        public virtual ICollection<HouseTask> HouseTasks { get; set; }
                = new List<HouseTask>();
        public virtual ICollection<ScheduleEvent> ScheduleEvents { get; set; }
                = new List<ScheduleEvent>();
        public virtual ICollection<ScheduleEvent> AccompanyEvents { get; set; } 
                = new List<ScheduleEvent>();
    }
}
