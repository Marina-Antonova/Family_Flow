using FamilyFlow.Data.Models;
using FamilyFlow.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace FamilyFlow.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService scheduleService;
        private readonly UserManager<ApplicationUser> userManager;

        public ScheduleController(IScheduleService scheduleService, UserManager<ApplicationUser> userManager)
        {
            this.scheduleService = scheduleService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string userId = userManager.GetUserId(User);
            var fullSchedule = await scheduleService.GetFullScheduleAsync(userId);

            return View(fullSchedule);
        }
    }
}
