using FamilyFlow.Data;
using FamilyFlow.Data.Models;
using FamilyFlow.GCommon.Enums;
using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.ViewModels.FamilyMember;
using FamilyFlow.ViewModels.HouseTasks;
using FamilyFlow.ViewModels.ScheduleEvent;
using Microsoft.EntityFrameworkCore;


namespace FamilyFlow.Services.Core
{
    public class HouseTaskService : IHouseTaskService
    {
        private readonly FamilyFlowDbContext dbContext;

        public HouseTaskService(FamilyFlowDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CreateEditTaskViewModel?> GetForCreateHouseTaskViewModelAsync(int familyMemberId)
        {
            FamilyMember? selectedMember = await dbContext
                .FamilyMembers
                .FirstOrDefaultAsync(f => f.Id == familyMemberId);

            if (selectedMember == null)
            {
                return null;
            }

            return new CreateEditTaskViewModel
            {
                FamilyMemberId = familyMemberId
            };
        }

        public async Task CreateHouseTaskAsync(int familyMemberId, CreateEditTaskViewModel inputModel)
        {
            HouseTask task = new HouseTask()
            {
                Title = inputModel.Title,
                Description = inputModel.Description,
                DueDate = inputModel.DueDate,
                IsCompleted = false,
                FamilyMemberId = familyMemberId
            };

            await dbContext.HouseTasks.AddAsync(task);
            await dbContext.SaveChangesAsync();
        }

        public async Task<CreateEditTaskViewModel?> GetForEditHouseTaskViewModelAsync(int id)
        {
            CreateEditTaskViewModel? selectedTask = await dbContext
                .HouseTasks
                .AsNoTracking()
                .Where(ht => ht.Id == id)
                .Select(ht => new CreateEditTaskViewModel
                {
                    Id = ht.Id,
                    Title = ht.Title,
                    Description = ht.Description,
                    DueDate = ht.DueDate,
                    FamilyMemberId = ht.FamilyMemberId
                })
                .SingleOrDefaultAsync();
           
            if (selectedTask == null)
            {
                throw new Exception("Task not found.");
            }

            return selectedTask;
        }

        public async Task EditHouseTaskAsync(int id, CreateEditTaskViewModel inputModel)
        {
            HouseTask? selectedTask = await dbContext
                .HouseTasks
                .SingleOrDefaultAsync(ht => ht.Id == id);

            if (selectedTask == null)
            {
                throw new Exception("Task not found.");
            }

            selectedTask.Title = inputModel.Title;
            selectedTask.Description = inputModel.Description;
            selectedTask.DueDate = inputModel.DueDate;

            await dbContext.SaveChangesAsync();
        }

        public async Task<DeleteTaskViewModel?> GetForDeleteHouseTaskViewModelAsync(int id)
        {
            DeleteTaskViewModel? selectedTask = await dbContext
            .HouseTasks
            .AsNoTracking()
            .Where(ht => ht.Id == id)
            .Select(fm => new DeleteTaskViewModel
            {
                Id = fm.Id,
                Title = fm.Title,
                DueDate = fm.DueDate.ToString("yyyy-MM-dd"),
                FamilyMemberId = fm.FamilyMemberId
            })
            .SingleOrDefaultAsync();

            if (selectedTask == null)
            {
                throw new Exception("Task not found.");
            }

            return selectedTask;
        }

        public async Task DeleteHouseTaskAsync(int id, DeleteTaskViewModel viewModel)
        {
            HouseTask? selectedTask = await dbContext
                .HouseTasks
                .SingleOrDefaultAsync(ht => ht.Id == id);

            if (selectedTask == null)
            {
                throw new Exception("Task not found.");
            }

            dbContext.HouseTasks.Remove(selectedTask);
            await dbContext.SaveChangesAsync();
        }
    }
}
