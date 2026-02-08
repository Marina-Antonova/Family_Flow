using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FamilyFlow.Common.ValidationConstants.HouseTask;

namespace FamilyFlow.Data.Models
{
    public class HouseTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;
        
        [ForeignKey(nameof(FamilyMemberHouseTasks))]
        public int FamilyMemberId { get; set; }

        public virtual FamilyMember FamilyMemberHouseTasks { get; set; } = null!;
    }
}
