using FamilyFlow.Data;
using FamilyFlow.Data.Models;
using FamilyFlow.GCommon.Enums;
using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.Web.ViewModels.Family;
using FamilyFlow.Web.ViewModels.FamilyMember;
using Microsoft.EntityFrameworkCore;

namespace FamilyFlow.Services.Core
{
    public class FamilyService : IFamilyService
    {
        private readonly FamilyFlowDbContext dbContext;

        public FamilyService(FamilyFlowDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<FamilyViewModel?> GetFamilyForUserAsync(string userId)
        {
            if (!Guid.TryParse(userId, out Guid parsedUserId))
            {
                return null;
            }

            int? linkedFamilyId = await dbContext
                .FamilyMembers
                .Where(fm => fm.LinkedUserId == parsedUserId)
                .Select(fm => (int?)fm.FamilyId)
                .FirstOrDefaultAsync();

            if (linkedFamilyId.HasValue)
            {
                return await dbContext
                    .Families
                    .Where(f => f.Id == linkedFamilyId.Value)
                    .Select(f => new FamilyViewModel
                    {
                        Id = f.Id,
                        Name = f.Name
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }

            return await dbContext
                .Families
                .Where(f => f.UserId == parsedUserId)
                .Select(f => new FamilyViewModel
                {
                    Id = f.Id,
                    Name = f.Name
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task CreateFamilyAsync(FamilyViewModel model, string userId)
        {
            Family newFamily = new Family
            {
                Name = model.Name,
                UserId = Guid.Parse(userId)
            };

            await dbContext.Families.AddAsync(newFamily);
            await dbContext.SaveChangesAsync();
        }

        public async Task<FamilyViewModel?> GetFamilyAsync(int id)
        {
            FamilyViewModel? family = await dbContext
            .Families
            .AsNoTracking()
            .Where(f => f.Id == id)
            .Select(f => new FamilyViewModel
            {
                Id = f.Id,
                Name = f.Name
            })
            .FirstOrDefaultAsync();

            if (family == null)
            {
                throw new Exception("Family not found.");
            }

            return family;
        }

        public async Task EditFamilyAsync(int id, FamilyViewModel inputModel)
        {
            Family? family = await dbContext
               .Families
               .FirstOrDefaultAsync(f => f.Id == id);

            if (family == null)
            {
                throw new Exception("Family not found.");
            }

            family.Name = inputModel.Name;

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteFamilyAsync(int id, FamilyViewModel? viewModel)
        {
            Family? family = await dbContext
                 .Families
                 .FirstOrDefaultAsync(f => f.Id == id);

            if (family == null)
            {
                throw new Exception("Family not found.");
            }

            dbContext.Families.Remove(family);
            await dbContext.SaveChangesAsync();
        }

        public async Task<int> GetFamilyIdForUserAsync(string? userId)
        {
            if (!Guid.TryParse(userId, out Guid parsedUserId))
            {
                return 0;
            }

            int linkedFamilyId = await dbContext
                .FamilyMembers
                .Where(fm => fm.LinkedUserId == parsedUserId)
                .Select(fm => fm.FamilyId)
                .FirstOrDefaultAsync();

            if (linkedFamilyId > 0)
            {
                return linkedFamilyId;
            }

            return await dbContext
                .Families
                .Where(f => f.UserId == parsedUserId)
                .Select(f => f.Id)
                .FirstOrDefaultAsync();
        }
    }
}
