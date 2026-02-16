using FamilyFlow.Data.Models.Enums;

namespace FamilyFlow.Common
{
    public static class FamilyRoleExtensions
    {
        public static string GetImagePath(this FamilyRole role)
        {
            return role switch
            {
                FamilyRole.Mother => "/images/role/mother.png",
                FamilyRole.Father => "/images/role/father.png",
                FamilyRole.Daughter => "/images/role/daughter.png",
                FamilyRole.Son => "/images/role/son.png",
                FamilyRole.Grandmother => "/images/role/grandmother.png",
                FamilyRole.Grandfather => "/images/role/grandfather.png",
                FamilyRole.Other => "/images/role/default.png",
                _ => "/images/role/default.png"
            };
        }
    }
}
