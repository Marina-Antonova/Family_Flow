using FamilyFlow.Data;
using FamilyFlow.Data.Models;
using FamilyFlow.ViewModels.FamilyMember;
using FamilyFlow.ViewModels.HouseTasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;


namespace FamilyFlow.Controllers
{
    public class HouseTasksController : Controller
    {
        private readonly FamilyFlowDbContext dbContext;
        public HouseTasksController(FamilyFlowDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            FamilyMember? selectedMember = dbContext
                 .FamilyMembers
                 .SingleOrDefault(fm => fm.Id == id);

            if (selectedMember == null)
            {
                return NotFound();
            }

            CreateEditTaskViewModel inputModel = new CreateEditTaskViewModel()
            {
                FamilyMemberId = id
            };

            return View(inputModel);
        }

        [HttpPost]
        public IActionResult Create(int id, CreateEditTaskViewModel inputModel)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            FamilyMember? selectedMember = dbContext
              .FamilyMembers
              .SingleOrDefault(fm => fm.Id == id);

            if (selectedMember == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Model Validation failed.");
                return View(inputModel);
            }

            try
            {
                HouseTask task = new HouseTask()
                {
                    Title = inputModel.Title,
                    Description = inputModel.Description,
                    DueDate = inputModel.DueDate,
                    IsCompleted = false,
                    FamilyMemberId = selectedMember.Id,
                };
                dbContext.HouseTasks.Add(task);
                dbContext.SaveChanges();
                return RedirectToAction("Details", "FamilyMembers", new { id = selectedMember.Id });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while creating. Please try again later.");
                return View(inputModel);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            HouseTask? selectedTask = dbContext
                .HouseTasks
                .SingleOrDefault(t => t.Id == id);

            if (selectedTask == null)
            {
                return NotFound();
            }

            CreateEditTaskViewModel inputModel = new CreateEditTaskViewModel()
            {
                Title = selectedTask.Title,
                Description = selectedTask.Description,
                DueDate = selectedTask.DueDate,
                FamilyMemberId = selectedTask.FamilyMemberId
            };

            return View(inputModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, CreateEditTaskViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Model Validation failed.");
                return View(inputModel);
            }

            if (id <= 0)
            {
                return BadRequest();
            }

            HouseTask? selectedTask = dbContext
                 .HouseTasks
                 .SingleOrDefault(t => t.Id == id);

            if (selectedTask == null)
            {
                return NotFound();
            }

            try
            {
                selectedTask.Title = inputModel.Title;
                selectedTask.Description = inputModel.Description;
                selectedTask.DueDate = inputModel.DueDate;
                selectedTask.FamilyMemberId = inputModel.FamilyMemberId;

                dbContext.SaveChanges();
                return RedirectToAction("Details", "FamilyMembers", new { id = selectedTask.FamilyMemberId });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while editing. Please try again later.");
                return View(inputModel);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            HouseTask? selectedTask = dbContext
                 .HouseTasks
                 .SingleOrDefault(t => t.Id == id);

            if (selectedTask == null)
            {
                return NotFound();
            }

            DeleteTaskViewModel viewModel = new DeleteTaskViewModel()
            {
                Title = selectedTask.Title,
                DueDate = selectedTask.DueDate.ToString(),
                FamilyMemberId = selectedTask.FamilyMemberId
               
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id, DeleteTaskViewModel viewModel)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            HouseTask? selectedTask = dbContext
                 .HouseTasks
                 .SingleOrDefault(t => t.Id == id);

            if (selectedTask == null)
            {
                return NotFound();
            }

            try
            {
                dbContext.HouseTasks.Remove(selectedTask);
                dbContext.SaveChanges();
                return RedirectToAction("Details", "FamilyMembers", new { id = selectedTask.FamilyMemberId });
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