using System.ComponentModel.DataAnnotations;
using static FamilyFlow.GCommon.ValidationConstants.Family;

namespace FamilyFlow.Web.ViewModels.Family
{
    public class FamilyViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        public string Name { get; set; } = null!;
        //[Required]
        public string? UserId { get; set; } = null!;
    }
}
