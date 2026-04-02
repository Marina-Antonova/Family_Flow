using FamilyFlow.Data;
using FamilyFlow.Data.Models;
using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.Web.ViewModels.ScheduleEvent;
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
              return new CreateEditEventViewModel();
        }

        public async Task CreateScheduleEventAsync(CreateEditEventViewModel inputModel, int familyMemberId)
        {
            FamilyMember? selectedMember = await FindSelectedFamilyMemberAsync(familyMemberId);

            if (selectedMember == null)
            {
                throw new Exception("Family member not found");
            }

            ScheduleEvent newEvent = new ScheduleEvent()
            {
                Title = inputModel.Title,
                StartTime = inputModel.StartTime,
                EndTime = inputModel.EndTime,
                AccompanyingAdultId = inputModel.AccompanyingAdultId,
                CreatorId = selectedMember.Id,
                Participants = inputModel.SelectedMemberIds?
                    .Select(memberId => new ScheduleEventParticipant
                    {
                        FamilyMemberId = memberId
                    })
                    .ToList() ?? new List<ScheduleEventParticipant>()
                .ToList()
                };

            await dbContext.ScheduleEvents.AddAsync(newEvent);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            
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
                    CreatorId = se.CreatorId,
                    AccompanyingAdultId = se.AccompanyingAdultId,
                    SelectedMemberIds = se.Participants
                        .Select(p => p.FamilyMemberId)
                        .ToList()
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
            Data.Models.ScheduleEvent? selectedEvent = await dbContext
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
                CreatorId = se.CreatorId

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
            Data.Models.ScheduleEvent? selectedEvent = await dbContext
                .ScheduleEvents
                .FirstOrDefaultAsync(se => se.Id == id);

            if (selectedEvent == null)
            {
                throw new Exception("Event not found.");
            }

            dbContext.ScheduleEvents.Remove(selectedEvent);
            await dbContext.SaveChangesAsync();
        }

        public IEnumerable<CreateEditAdultViewModel> GetAllAdults(string userId)
        {
            return dbContext
             .FamilyMembers
             .AsNoTracking()
             .Where(a => a.Age >= 18 && a.UserId.ToString() == userId)
             .OrderBy(a => a.Name)
             .Select(a => new CreateEditAdultViewModel()
             {
                 Id = a.Id,
                 Name = a.Name
             })
             .ToArray();
        }

        public IEnumerable<CreateEditAdultViewModel> GetAllMembers(string userId)
        {
            return dbContext
             .FamilyMembers
             .AsNoTracking()
             .Where(m => m.UserId.ToString() == userId)
             .OrderBy(a => a.Name)
             .Select(a => new CreateEditAdultViewModel()
             {
                 Id = a.Id,
                 Name = a.Name
             })
             .ToArray();
        }

        public async Task<Data.Models.FamilyMember?> FindSelectedFamilyMemberAsync(int familyMemberId)
        {
            return await dbContext
                .FamilyMembers
                .FirstOrDefaultAsync(f => f.Id == familyMemberId);
        }
    }
}
