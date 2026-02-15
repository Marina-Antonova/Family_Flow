using FamilyFlow.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static FamilyFlow.Common.ValidationConstants.FamilyMember;

namespace FamilyFlow.ViewModels.FamilyMember
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
    }
}
