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

        public async Task<List<ScheduleItemViewModel>> GetFullScheduleAsync(string userId)
        {

            var userFamilyMembersIds = await dbContext.FamilyMembers
                .Where(fm => fm.UserId.ToString() == userId)
                .Select(fm => fm.Id)
                .ToListAsync();

            var participantEventItems = await dbContext.ScheduleEvents
                .Where (e=> e.Participants.Any(p => userFamilyMembersIds.Contains(p.FamilyMemberId)))
                .SelectMany(e => e.Participants, (e,p) => new ScheduleItemViewModel
                {
                    FamilyMemberName = p.FamilyMember.Name,
                    Title = e.Title,
                    Type = "Event",
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    AccompanyingAdultName = e.AccompanyingAdult != null ? e.AccompanyingAdult.Name : null
                })
                .ToListAsync();

            var creatorOnlyEventItems = await dbContext.ScheduleEvents
                .Where(e => !e.Participants.Any() && userFamilyMembersIds.Contains(e.CreatorId))
                .Select(e => new ScheduleItemViewModel
                {
                    FamilyMemberName = dbContext.FamilyMembers
                        .Where(fm => fm.Id == e.CreatorId)
                        .Select(fm => fm.Name)
                        .FirstOrDefault()!,
                    Title = e.Title,
                    Type = "Event",
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    AccompanyingAdultName = e.AccompanyingAdult != null ? e.AccompanyingAdult.Name : null
                })
                .ToListAsync();

            var taskItems = await dbContext.HouseTasks
                .Where(t => userFamilyMembersIds.Contains(t.FamilyMemberId))
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

            return participantEventItems
                .Concat(creatorOnlyEventItems)
                .Concat(taskItems)
                .OrderBy(x => x.EndTime)
                .ToList();
        }
    }
}
