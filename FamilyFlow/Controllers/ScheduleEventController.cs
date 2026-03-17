using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.Web.ViewModels.ScheduleEvent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FamilyFlow.Controllers
{
    public class ScheduleEventController : Controller
    {
        private readonly IScheduleEventService scheduleEventService;
        public ScheduleEventController(IScheduleEventService scheduleEventService)
        {
            this.scheduleEventService = scheduleEventService;
        }

        [HttpGet]
        [Authorize]
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

            return View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(int id, CreateEditEventViewModel inputModel)
        {
            inputModel.Adults = scheduleEventService.GetAllAdults().ToList();

            if (id <= 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Model Validation failed.");
                return View(inputModel);
            }

            var selectedMember = await scheduleEventService.FindSelectedFamilyMemberAsync(inputModel.FamilyMemberId);

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
                await scheduleEventService.CreateScheduleEventAsync(inputModel);
                return RedirectToAction("Details", "FamilyMembers", new { id = inputModel.FamilyMemberId });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while creating. Please try again later.");
                return View(inputModel);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

            var selectedEvent = await scheduleEventService.GetForEditScheduleEventViewModelAsync(id);
            selectedEvent.Adults = scheduleEventService.GetAllAdults().ToList();

            if (selectedEvent == null)
                {
                    return NotFound();
                }

                return View(selectedEvent);
            }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, CreateEditEventViewModel inputModel)
            {
                inputModel.Adults = scheduleEventService.GetAllAdults().ToList();

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

                var selectedMember = await scheduleEventService.FindSelectedFamilyMemberAsync(inputModel.FamilyMemberId);

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
                    return RedirectToAction("Details", "FamilyMembers", new { id = inputModel.FamilyMemberId });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    ModelState.AddModelError(string.Empty, "Unexpected error occurred while editing. Please try again later.");
                    return View(inputModel);
                }
            }

        [HttpGet]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Delete(int id, DeleteEventViewModel viewModel)
            {
                if (id <= 0)
                {
                    return BadRequest();
                }
            try
                {
                    await scheduleEventService.DeleteScheduleEventAsync(id, viewModel);
                    return RedirectToAction("Details", "FamilyMembers", new { id = viewModel.FamilyMemberId });
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
