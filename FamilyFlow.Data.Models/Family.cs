using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using static FamilyFlow.GCommon.ValidationConstants.Family;

namespace FamilyFlow.Data.Models
{
    public class Family
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<FamilyMember> Members { get; set; } 
            = new List<FamilyMember>();

    }
}
