using FamilyFlow.Data;
using FamilyFlow.Data.Models;
using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.Web.ViewModels.FamilyMember;
using FamilyFlow.GCommon;
using FamilyFlow.GCommon.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FamilyFlow.Web.ViewModels.ScheduleEvent;


namespace FamilyFlow.Services.Core
{
    public class FamilyMemberService : IFamilyMemberService
    {
        private readonly FamilyFlowDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public FamilyMemberService(FamilyFlowDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<AllFamilyMembersViewModel>> GetAllFamilyMembersAsync(string userId)
        {
            int familyId = await GetAccessibleFamilyIdAsync(userId);
            if (familyId <= 0)
            {
                return Enumerable.Empty<AllFamilyMembersViewModel>();
            }

            IEnumerable<AllFamilyMembersViewModel> members = await dbContext
              .FamilyMembers
              .Where(fm => fm.FamilyId == familyId)
              .AsNoTracking()
              .Include(fm => fm.HouseTasks)
              .Include(fm => fm.EventParticipations)
              .ThenInclude(ep => ep.ScheduleEvent)
              .Select(static f => new AllFamilyMembersViewModel()
              {
                  Id = f.Id,
                  Name = f.Name,
                  Role = f.Role.ToString(),
                  RoleImagePath = f.Role.GetImagePath(),
                  Age = f.Age,
                  HouseTasksCount = f.HouseTasks.Count,
                  ScheduleEventsCount = f.AccompanyEvents.Count + f.EventParticipations.Count
              })
              .OrderBy(f => f.Id)
              .ToArrayAsync();

            return members;
        }

        public async Task<DetailsFamilyMemberViewModel?> GetDetailsForFamilyMemberAsync(int id, string userId)
        {
            int familyId = await GetAccessibleFamilyIdAsync(userId);
            if (familyId <= 0)
            {
                return null;
            }

            FamilyMember? selectedMember = await dbContext
                .FamilyMembers
                .Where(fm => fm.FamilyId == familyId)
                .AsNoTracking()
                .Include(fm => fm.HouseTasks)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (selectedMember == null)
            {
                return null;
            }

            List<DetailsScheduleEventsViewModel> events = await dbContext
                .ScheduleEvents
                .AsNoTracking()
                .Where(se => se.CreatorId == selectedMember.Id
                    || se.Participants.Any(p => p.FamilyMemberId == selectedMember.Id))
                .Select(se => new DetailsScheduleEventsViewModel
                {
                    Id = se.Id,
                    Title = se.Title,
                    StartTime = se.StartTime,
                    EndTime = se.EndTime
                })
                .OrderBy(se => se.StartTime)
                .ToListAsync();

            return new DetailsFamilyMemberViewModel
            {
                Id = selectedMember.Id,
                Name = selectedMember.Name,
                Age = selectedMember.Age,
                Role = selectedMember.Role.ToString(),
                Tasks = selectedMember.HouseTasks
                    .Select(t => new DetailsHouseTaskViewModel
                    {
                        Id = t.Id,
                        Title = t.Title,
                        DueDate = t.DueDate
                    })
                    .ToList(),
                Events = events
                    .GroupBy(e => e.Id)
                    .Select(g => g.First())
                    .ToList()
            };

        }

        public async Task CreateFamilyMemberAsync(CreateFamilyMemberViewModel inputModel, string userId, int familyId)
        {
            inputModel.Email = inputModel.Email?.Trim();
            Guid? linkedUserId = await ResolveLinkedUserIdAsync(inputModel.Email, userId);

            FamilyMember newMember = new FamilyMember
            {
                Name = inputModel.Name,
                Role = (FamilyRole)inputModel.Role,
                Age = inputModel.Age,
                Email = inputModel.Email,
                UserId = Guid.Parse(userId),
                LinkedUserId = linkedUserId,
                FamilyId = familyId
            };

            await dbContext.FamilyMembers.AddAsync(newMember);
            await dbContext.SaveChangesAsync();

            await SyncIdentityRoleAsync(newMember.LinkedUserId, newMember.Role);
        }

        public async Task<CreateFamilyMemberViewModel?> GetForEditFamilyMemberAsync(int id)
        {
            CreateFamilyMemberViewModel? selectedMember = await dbContext
            .FamilyMembers
            .AsNoTracking()
            .Where(fm => fm.Id == id)
            .Select(fm => new CreateFamilyMemberViewModel
            {
                Id = fm.Id,
                Name = fm.Name,
                Role = fm.Role,
                Age = fm.Age,
                Email = fm.Email
            })
            .FirstOrDefaultAsync();

            if (selectedMember == null)
            {
                throw new Exception("Member not found.");
            }

            return selectedMember;
        }

        public async Task EditFamilyMemberAsync(int id, CreateFamilyMemberViewModel inputModel)
        {
            inputModel.Email = inputModel.Email?.Trim();
            FamilyMember? selectedMember = await dbContext
                .FamilyMembers
                .FirstOrDefaultAsync(fm => fm.Id == id);

            if (selectedMember == null)
            {
                throw new Exception("Member not found.");
            }

            selectedMember.Name = inputModel.Name;
            selectedMember.Role = (FamilyRole)inputModel.Role;
            selectedMember.Age = inputModel.Age;
            selectedMember.Email = inputModel.Email;
            selectedMember.LinkedUserId = await ResolveLinkedUserIdAsync(
                inputModel.Email,
                selectedMember.UserId.ToString(),
                selectedMember.Id);

            await dbContext.SaveChangesAsync();

            await SyncIdentityRoleAsync(selectedMember.LinkedUserId, selectedMember.Role);

        }

        public async Task<DeleteFamilyMemberViewModel?> GetForDeleteFamilyMemberAsync(int id)
        {
            DeleteFamilyMemberViewModel? selectedMember = await dbContext
            .FamilyMembers
            .AsNoTracking()
            .Where(fm => fm.Id == id)
            .Select(fm => new DeleteFamilyMemberViewModel
            {
                Id = fm.Id,
                Name = fm.Name
            })
            .FirstOrDefaultAsync();

            if (selectedMember == null)
            {
                throw new Exception("Member not found.");
            }

            return selectedMember;
        }

        public async Task DeleteFamilyMemberAsync(int id, DeleteFamilyMemberViewModel? viewModel)
        {
           FamilyMember? selectedMember = await dbContext
          .FamilyMembers
          .AsNoTracking()
          .Where(fm => fm.Id == id)
          .FirstOrDefaultAsync();

            if (selectedMember == null)
            {
                throw new Exception("Member not found.");
            }

            dbContext.FamilyMembers.Remove(selectedMember);
            await dbContext.SaveChangesAsync();

        }

        private async Task<int> GetAccessibleFamilyIdAsync(string userId)
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

        private async Task<Guid?> ResolveLinkedUserIdAsync(
            string? email,
            string ownerUserId,
            int? currentFamilyMemberId = null)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            string trimmedEmail = email.Trim();
            string normalizedEmail = userManager.NormalizeEmail(trimmedEmail) ?? trimmedEmail.ToUpperInvariant();
            ApplicationUser? ownerUser = await userManager.FindByIdAsync(ownerUserId);

            if (ownerUser != null &&
                !string.IsNullOrWhiteSpace(ownerUser.Email) &&
                string.Equals(ownerUser.Email.Trim(), trimmedEmail, StringComparison.OrdinalIgnoreCase))
            {
                bool ownerAlreadyLinkedToAnotherMember = await dbContext.FamilyMembers
                    .AnyAsync(fm => fm.LinkedUserId == ownerUser.Id && fm.Id != currentFamilyMemberId);

                if (ownerAlreadyLinkedToAnotherMember)
                {
                    throw new InvalidOperationException("This account is already linked to another family member.");
                }

                return ownerUser.Id;
            }

            ApplicationUser? existingUser = await userManager.Users
                .FirstOrDefaultAsync(u =>
                    u.NormalizedEmail == normalizedEmail ||
                    u.Email == trimmedEmail);

            if (existingUser == null)
            {
                return null;
            }

            bool alreadyLinkedToAnotherMember = await dbContext.FamilyMembers
                .AnyAsync(fm => fm.LinkedUserId == existingUser.Id && fm.Id != currentFamilyMemberId);

            if (alreadyLinkedToAnotherMember)
            {
                throw new InvalidOperationException("This email is already linked to another family member.");
            }

            return existingUser.Id;
        }

        private async Task SyncIdentityRoleAsync(Guid? linkedUserId, FamilyRole familyRole)
        {
            if (!linkedUserId.HasValue)
            {
                return;
            }

            ApplicationUser? user = await userManager.FindByIdAsync(linkedUserId.Value.ToString());

            if (user == null)
            {
                return;
            }

            string targetRole = familyRole == FamilyRole.Mother || familyRole == FamilyRole.Father
                ? "Admin"
                : "User";

            string otherRole = targetRole == "Admin" ? "User" : "Admin";

            if (await userManager.IsInRoleAsync(user, otherRole))
            {
                IdentityResult removeResult = await userManager.RemoveFromRoleAsync(user, otherRole);
                if (!removeResult.Succeeded)
                {
                    throw new InvalidOperationException($"Failed to remove role '{otherRole}' from user.");
                }
            }

            if (!await userManager.IsInRoleAsync(user, targetRole))
            {
                IdentityResult addResult = await userManager.AddToRoleAsync(user, targetRole);
                if (!addResult.Succeeded)
                {
                    throw new InvalidOperationException($"Failed to add role '{targetRole}' to user.");
                }
            }
        }
        public async Task<IEnumerable<AllFamilyMembersViewModel>> SearchFamilyMemberAsync(string userId, string searchText)
        {
            int familyId = await GetAccessibleFamilyIdAsync(userId);

            if (familyId <= 0 || string.IsNullOrWhiteSpace(searchText))
            {
                return Enumerable.Empty<AllFamilyMembersViewModel>();
            }

            searchText = searchText.Trim();

            return await dbContext.FamilyMembers
                .AsNoTracking()
                .Where(fm => fm.FamilyId == familyId &&
                             EF.Functions.Like(fm.Name, $"%{searchText}%"))
                .Select(fm => new AllFamilyMembersViewModel
                {
                    Id = fm.Id,
                    Name = fm.Name,
                    Role = fm.Role.ToString(),
                    RoleImagePath = fm.Role.GetImagePath(),
                    Age = fm.Age,
                    HouseTasksCount = fm.HouseTasks.Count,
                    ScheduleEventsCount = fm.AccompanyEvents.Count + fm.EventParticipations.Count
                })
                .OrderBy(fm => fm.Name)
                .ToListAsync();
        }
    }
}
