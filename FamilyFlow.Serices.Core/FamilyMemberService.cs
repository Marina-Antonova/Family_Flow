using FamilyFlow.Data;
using FamilyFlow.Data.Models;
using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.Web.ViewModels.FamilyMember;
using FamilyFlow.GCommon;
using FamilyFlow.GCommon.Enums;
using Microsoft.EntityFrameworkCore;


namespace FamilyFlow.Services.Core
{
    public class FamilyMemberService : IFamilyMemberService
    {
        private readonly FamilyFlowDbContext dbContext;

        public FamilyMemberService(FamilyFlowDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllFamilyMembersViewModel>> GetAllFamilyMembersAsync(string userId)
        {
            IEnumerable<AllFamilyMembersViewModel> members = await dbContext
              .FamilyMembers
              .Where(fm => fm.UserId.ToString() == userId)
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
            FamilyMember? selectedMember = await dbContext
                .FamilyMembers
                .Where(fm => fm.UserId.ToString() == userId)
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
            FamilyMember newMember = new FamilyMember
            {
                Name = inputModel.Name,
                Role = (FamilyRole)inputModel.Role,
                Age = inputModel.Age,
                UserId = Guid.Parse(userId),
                FamilyId = familyId
            };

            await dbContext.FamilyMembers.AddAsync(newMember);
            await dbContext.SaveChangesAsync();
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
                Age = fm.Age
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

            await dbContext.SaveChangesAsync();

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
    }
}
