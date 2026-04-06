using FamilyFlow.GCommon.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FamilyFlow.GCommon.ValidationConstants.FamilyMember;

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

        [EmailAddress]
        [MaxLength(256)]
        public string? Email { get; set; }

        public virtual ICollection<HouseTask> HouseTasks { get; set; }
                = new List<HouseTask>();
        public virtual ICollection<ScheduleEvent> AccompanyEvents { get; set; } 
                = new List<ScheduleEvent>();
        public virtual ICollection<ScheduleEventParticipant> EventParticipations { get; set; }
               = new List<ScheduleEventParticipant>();

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(LinkedUser))]
        public Guid? LinkedUserId { get; set; }

        public ApplicationUser? LinkedUser { get; set; }

        public int FamilyId { get; set; }
        public virtual Family Family { get; set; } = null!;
    }
}
