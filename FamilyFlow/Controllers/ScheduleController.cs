using FamilyFlow.Data;
using FamilyFlow.ViewModels.Schedule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFlow.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly FamilyFlowDbContext dbContext;

        public ScheduleController(FamilyFlowDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var eventItems = dbContext.ScheduleEvents
                .Select(e => new ScheduleItemViewModel
                {
                    FamilyMemberName = e.FamilyMemberScheduleEvents.Name,
                    Title = e.Title,
                    Type = "Event",
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    AccompanyingAdultName = e.AccompanyingAdult.Name
                 });

            var taskItems = dbContext.HouseTasks
                .Select(t => new ScheduleItemViewModel
                {
                    FamilyMemberName = t.FamilyMemberHouseTasks.Name,
                    Title = t.Title,
                    Type = "Task",
                    StartTime = null,
                    EndTime = t.DueDate,
                    AccompanyingAdultName = null
                });

            var fullSchedule = eventItems
                .Concat(taskItems)
                .OrderBy(x => x.EndTime)
                .ToList();

            return View(fullSchedule);
        }
    }
}
