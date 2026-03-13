using FamilyFlow.Data;
using FamilyFlow.Services.Interfaces;
using FamilyFlow.ViewModels.Schedule;
using Microsoft.EntityFrameworkCore;

namespace FamilyFlow.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly FamilyFlowDbContext dbContext;

        public ScheduleService(FamilyFlowDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ScheduleItemViewModel>> GetFullScheduleAsync()
        {
            var eventItems = await dbContext.ScheduleEvents
                .Select(e => new ScheduleItemViewModel
                {
                    FamilyMemberName = e.FamilyMemberScheduleEvents.Name,
                    Title = e.Title,
                    Type = "Event",
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    AccompanyingAdultName = e.AccompanyingAdult.Name
                })
                .ToListAsync();

            var taskItems = await dbContext.HouseTasks
                .Select(t => new ScheduleItemViewModel
                {
                    FamilyMemberName = t.FamilyMemberHouseTasks.Name,
                    Title = t.Title,
                    Type = "Task",
                    StartTime = null,
                    EndTime = t.DueDate,
                    AccompanyingAdultName = null
                })
                .ToListAsync();

            return eventItems
                .Concat(taskItems)
                .OrderBy(x => x.EndTime)
                .ToList();
        }
    }
}