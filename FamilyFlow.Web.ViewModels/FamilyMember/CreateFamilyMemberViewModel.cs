using FamilyFlow.GCommon.Enums;
using System.ComponentModel.DataAnnotations;
using static FamilyFlow.GCommon.ValidationConstants.FamilyMember;

namespace FamilyFlow.Web.ViewModels.FamilyMember
{
    public class CreateFamilyMemberViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public FamilyRole? Role { get; set; } 

        [Range(MinAge, MaxAge)]
        public int Age { get; set; }

        [EmailAddress]
        [MaxLength(256)]
        public string? Email { get; set; }

        public string? UserId { get; set; }

        public int? FamilyId { get; set; }
    }
}
