using FamilyFlow.Data;
using FamilyFlow.Data.Models;
using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.ViewModels.ScheduleEvent;
using Microsoft.EntityFrameworkCore;

namespace FamilyFlow.Services.Core
{
    public class ScheduleEventService : IScheduleEventService
    {
        private readonly FamilyFlowDbContext dbContext;

        public ScheduleEventService(FamilyFlowDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CreateEditEventViewModel?> GetForCreateScheduleEventViewModelAsync(int familyMemberId)
        {
            FamilyMember? selectedMember = await dbContext
               .FamilyMembers
               .AsNoTracking()
               .FirstOrDefaultAsync(f => f.Id == familyMemberId);

            if (selectedMember == null)
            {
                return null;
            }

            return new CreateEditEventViewModel
            {
                FamilyMemberId = familyMemberId
            };
        }

        public async Task CreateScheduleEventAsync(CreateEditEventViewModel inputModel)
        {
            ScheduleEvent newEvent = new ScheduleEvent()
            {
                Title = inputModel.Title,
                StartTime = inputModel.StartTime,
                EndTime = inputModel.EndTime,
                FamilyMemberId = inputModel.FamilyMemberId,
                AccompanyingAdultId = inputModel.AccompanyingAdultId
            };

            await dbContext.ScheduleEvents.AddAsync(newEvent);
            await dbContext.SaveChangesAsync();
        }

        public async Task<CreateEditEventViewModel?> GetForEditScheduleEventViewModelAsync(int id)
        {
            CreateEditEventViewModel? selectedEvent = await dbContext
                .ScheduleEvents
                .AsNoTracking()
                .Where(se => se.Id == id)
                .Select(se => new CreateEditEventViewModel
                {
                    Id = se.Id,
                    Title = se.Title,
                    StartTime = se.StartTime,
                    EndTime = se.EndTime,
                    FamilyMemberId = se.FamilyMemberId,
                    AccompanyingAdultId = se.AccompanyingAdultId
                })
                .FirstOrDefaultAsync();

            if (selectedEvent == null)
            {
                throw new Exception("Event not found.");
            }

            return selectedEvent;
        }

        public async Task EditScheduleEventAsync(int id, CreateEditEventViewModel inputModel)
        {
            ScheduleEvent? selectedEvent = await dbContext
                .ScheduleEvents
                .FirstOrDefaultAsync(se => se.Id == id);

            if (selectedEvent == null)
            {
                throw new Exception("Event not found.");
            }

            selectedEvent.Title = inputModel.Title;
            selectedEvent.StartTime = inputModel.StartTime;
            selectedEvent.EndTime = inputModel.EndTime;
            selectedEvent.AccompanyingAdultId = inputModel.AccompanyingAdultId;

            await dbContext.SaveChangesAsync();
        }

        public async Task<DeleteEventViewModel?> GetForDeleteScheduleEventViewModelAsync(int id)
        {
            DeleteEventViewModel? selectedEvent = await dbContext
            .ScheduleEvents
            .AsNoTracking()
            .Where(se => se.Id == id)
            .Select(se => new DeleteEventViewModel
            {
                Id = se.Id,
                Title = se.Title,
                StartTime = se.StartTime.ToString(),
                EndTime = se.EndTime.ToString(),
                FamilyMemberId = se.FamilyMemberId
            })
            .FirstOrDefaultAsync();

            if (selectedEvent == null)
            {
                throw new Exception("Event not found.");
            }

            return selectedEvent;
        }


        public async Task DeleteScheduleEventAsync(int id, DeleteEventViewModel viewModel)
        {
            ScheduleEvent? selectedEvent = await dbContext
                .ScheduleEvents
                .FirstOrDefaultAsync(se => se.Id == id);

            if (selectedEvent == null)
            {
                throw new Exception("Event not found.");
            }

            dbContext.ScheduleEvents.Remove(selectedEvent);
            await dbContext.SaveChangesAsync();
        }

        public IEnumerable<CreateEditAdultViewModel> GetAllAdults()
        {
            return dbContext
             .FamilyMembers
             .AsNoTracking()
             .Where(a => a.Age >= 18)
             .OrderBy(a => a.Name)
             .Select(a => new CreateEditAdultViewModel()
             {
                 Id = a.Id,
                 Name = a.Name
             })
             .ToArray();
        }

        public async Task<FamilyMember?> FindSelectedFamilyMemberAsync(int familyMemberId)
        {
            return await dbContext
                .FamilyMembers
                .FirstOrDefaultAsync(f => f.Id == familyMemberId);
        }
    }
}
