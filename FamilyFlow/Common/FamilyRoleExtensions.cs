using FamilyFlow.Data.Models.Enums;

namespace FamilyFlow.Common
{
    public static class FamilyRoleExtensions
    {
        public static string GetImagePath(this FamilyRole role)
        {
            return role switch
            {
                FamilyRole.Mother => "/images/mother.png",
                FamilyRole.Father => "/images/father.png",
                FamilyRole.Daughter => "/images/daughter.png",
                FamilyRole.Son => "/images/son.png",
                FamilyRole.Grandmother => "/images/grandmother.png",
                FamilyRole.Grandfather => "/images/grandfather.png",
                FamilyRole.Other => "/images/default.png",
                _ => "/images/default.png"
            };
        }
    }
}
