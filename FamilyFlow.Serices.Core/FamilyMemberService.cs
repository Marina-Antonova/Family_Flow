using FamilyFlow.Data;
using FamilyFlow.Data.Models;
using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.ViewModels.FamilyMember;
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

        public async Task<IEnumerable<AllFamilyMembersViewModel>> GetAllFamilyMembersAsync()
        {
            IEnumerable<AllFamilyMembersViewModel> members = await dbContext
              .FamilyMembers
              .AsNoTracking()
              .Include(fm => fm.HouseTasks)
              .Include(fm => fm.ScheduleEvents)
              .Select(static fm => new AllFamilyMembersViewModel()
              {
                  Id = fm.Id,
                  Name = fm.Name,
                  Role = fm.Role.ToString(),
                  RoleImagePath = fm.Role.GetImagePath(),
                  Age = fm.Age,
                  HouseTasksCount = fm.HouseTasks.Count,
                  ScheduleEventsCount = fm.ScheduleEvents.Count
              })
              .OrderBy(fm => fm.Name)
              .ToArrayAsync();

            return members;
        }

        public async Task<DetailsFamilyMemberViewModel?> GetDetailsForFamilyMemberAsync(int id)
        {
            FamilyMember? selectedMember = await dbContext
            .FamilyMembers
            .AsNoTracking()
            .Include(fm => fm.HouseTasks)
            .Include(fm => fm.ScheduleEvents)
            .FirstOrDefaultAsync(m => m.Id == id);

            if (selectedMember == null)
            {
                return null;
            }

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
                Events = selectedMember.ScheduleEvents
                    .Select(e => new DetailsScheduleEventsViewModel
                    {
                        Id = e.Id,
                        Title = e.Title,
                        StartTime = e.StartTime,
                        EndTime = e.EndTime
                    })
                    .ToList()
            };

        }

        public async Task CreateFamilyMemberAsync(CreateFamilyMemberViewModel inputModel)
        {
            FamilyMember newMember = new FamilyMember
            {
                Name = inputModel.Name,
                Role = (FamilyRole)inputModel.Role,
                Age = inputModel.Age
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
