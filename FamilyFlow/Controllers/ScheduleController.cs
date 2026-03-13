using FamilyFlow.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFlow.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var fullSchedule = await scheduleService.GetFullScheduleAsync();

            return View(fullSchedule);
        }
    }
}
