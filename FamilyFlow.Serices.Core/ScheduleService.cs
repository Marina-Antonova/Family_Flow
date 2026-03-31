using FamilyFlow.Data;
using FamilyFlow.Services.Interfaces;
using FamilyFlow.Web.ViewModels.Schedule;
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
                .SelectMany(e => e.Participants, (e,p) => new ScheduleItemViewModel
                {
                    FamilyMemberName = p.FamilyMember.Name,
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