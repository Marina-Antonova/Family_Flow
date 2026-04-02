using Microsoft.AspNetCore.Identity;

namespace FamilyFlow.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Family? Family { get; set; }

        public virtual ICollection<FamilyMember> UserFamilyMembers { get; set; } 
            = new List<FamilyMember>();
    }
}
