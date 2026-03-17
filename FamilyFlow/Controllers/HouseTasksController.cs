using FamilyFlow.Data;
using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.Web.ViewModels.HouseTasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFlow.Controllers
{
    public class HouseTasksController : Controller
    {
        private readonly IHouseTaskService houseTaskService;
        public HouseTasksController(FamilyFlowDbContext dbContext, IHouseTaskService houseTaskService)
        {
            this.houseTaskService = houseTaskService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var inputModel = await houseTaskService.GetForCreateHouseTaskViewModelAsync(id);

            if (inputModel == null)
            {
                return NotFound();
            }

            return View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(int id, CreateEditTaskViewModel inputModel)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            if(inputModel.DueDate < DateTime.Today)
            {
                ModelState.AddModelError(nameof(inputModel.DueDate), "Due Date cannot be in the past.");
                return View(inputModel);
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Model Validation failed.");
                return View(inputModel);
            }

            try
            {
                await houseTaskService.CreateHouseTaskAsync(id, inputModel);

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
        [Authorize]
        public async Task <IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var selectedTask = await houseTaskService.GetForEditHouseTaskViewModelAsync(id);

            if (selectedTask == null)
            {
                return NotFound();
            }

            return View(selectedTask);
        }

        [HttpPost]
        [Authorize]
        public async Task <IActionResult> Edit(int id, CreateEditTaskViewModel inputModel)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Model Validation failed.");
                return View(inputModel);
            }

            if (inputModel.DueDate < DateTime.Today)
            {
                ModelState.AddModelError(nameof(inputModel.DueDate), "Due Date cannot be in the past.");
                return View(inputModel);
            }

            try
            {
                await houseTaskService.EditHouseTaskAsync(id, inputModel);
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

         var selectedTask = await houseTaskService.GetForDeleteHouseTaskViewModelAsync(id);

            if (selectedTask == null)
            {
                return NotFound();
            }

            return View(selectedTask);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id, DeleteTaskViewModel viewModel)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await houseTaskService.DeleteHouseTaskAsync(id, viewModel);
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