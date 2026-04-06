using FamilyFlow.Data.Models;
using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.Web.ViewModels.ScheduleEvent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFlow.Controllers
{
    public class ScheduleEventController : Controller
    {
        private readonly IScheduleEventService scheduleEventService;
        private readonly UserManager<ApplicationUser> userManager;
        public ScheduleEventController(IScheduleEventService scheduleEventService, UserManager<ApplicationUser> userManager)
        {
            this.scheduleEventService = scheduleEventService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task <IActionResult> Create(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var inputModel = await scheduleEventService.GetForCreateScheduleEventViewModelAsync(id);

            if (inputModel == null)
            {
                return NotFound();
            }

            string userId = userManager.GetUserId(User);
            inputModel.Adults = scheduleEventService.GetAllAdults(userId).ToList();

            inputModel.Members = scheduleEventService
                .GetAllMembers(userId)
                .Where(m => m.Id != inputModel.AccompanyingAdultId)
                .ToList();

            return View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(int id, CreateEditEventViewModel inputModel)
        {
            string userId = userManager.GetUserId(User);

            inputModel.Adults = scheduleEventService.GetAllAdults(userId).ToList();
            inputModel.Members = scheduleEventService.GetAllMembers(userId).ToList();

            if (id <= 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Model Validation failed.");
                return View(inputModel);
            }

            var selectedMember = await scheduleEventService.FindSelectedFamilyMemberAsync(id);

            if (selectedMember == null)
            {
                return NotFound();
            }

            if (selectedMember.Age <= 12 && !inputModel.AccompanyingAdultId.HasValue)
            {
                ModelState.AddModelError(nameof(inputModel.AccompanyingAdultId),
                    "Family member under 12 must have an accompanying adult.");
                return View(inputModel);
            }
        
            if (inputModel.StartTime >= inputModel.EndTime)
            {
                ModelState.AddModelError(nameof(inputModel.StartTime), "Start Time must be earlier than the End Time");
                return View(inputModel);
            }

            try
            {
                await scheduleEventService.CreateScheduleEventAsync(inputModel, id);
                return RedirectToAction("Details", "FamilyMembers", new { id });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while creating. Please try again later.");
                return View(inputModel);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

            var selectedEvent = await scheduleEventService.GetForEditScheduleEventViewModelAsync(id);
            
            string userId = userManager.GetUserId(User);
            selectedEvent.Adults = scheduleEventService.GetAllAdults(userId).ToList();

            if (selectedEvent == null)
                {
                    return NotFound();
                }

                return View(selectedEvent);
            }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, CreateEditEventViewModel inputModel)
            {
                string userId = userManager.GetUserId(User);

                inputModel.Adults = scheduleEventService.GetAllAdults(userId).ToList();

                if (id <= 0)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, "Model Validation failed.");
                    return View(inputModel);
                }

                if (inputModel.StartTime >= inputModel.EndTime)
                {
                    ModelState.AddModelError(nameof(inputModel.StartTime), "Start Time must be earlier than the End Time");
                    return View(inputModel);
                }
           
                var selectedMember = await scheduleEventService.FindSelectedFamilyMemberAsync(inputModel.CreatorId);

                if (selectedMember == null)
                {
                    return NotFound();
                }

                if (selectedMember.Age <= 12 && !inputModel.AccompanyingAdultId.HasValue)
                {
                    ModelState.AddModelError(nameof(inputModel.AccompanyingAdultId),"Family member under 12 must have an accompanying adult.");
                    return View(inputModel);
                }
                
                try
                {
                    await scheduleEventService.EditScheduleEventAsync(id, inputModel);
                    return RedirectToAction("Details", "FamilyMembers", new { id = inputModel.CreatorId });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    ModelState.AddModelError(string.Empty, "Unexpected error occurred while editing. Please try again later.");
                    return View(inputModel);
                }
            }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                var selectedEvent = await scheduleEventService.GetForDeleteScheduleEventViewModelAsync(id);

                if (selectedEvent == null)
                        {
                            return NotFound();
                        }

                return View(selectedEvent);
            }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id, DeleteEventViewModel viewModel)
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

            try
                {
                    await scheduleEventService.DeleteScheduleEventAsync(id, viewModel);
                    return RedirectToAction("Details", "FamilyMembers", new { id = viewModel.CreatorId });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    ModelState.AddModelError(string.Empty, "Unexpected error occurred while deleting. Please try again later.");
                    return View(viewModel);
                }
            }
    }
}
