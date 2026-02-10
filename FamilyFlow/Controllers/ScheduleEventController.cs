using FamilyFlow.Data;
using FamilyFlow.Data.Models;
using FamilyFlow.ViewModels.HouseTasks;
using FamilyFlow.ViewModels.ScheduleEvent;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace FamilyFlow.Controllers
{
    public class ScheduleEventController : Controller
    {
        private readonly FamilyFlowDbContext dbContext;
        public ScheduleEventController(FamilyFlowDbContext dbContext)
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
            CreateEditEventViewModel inputModel = new CreateEditEventViewModel()
            {
                FamilyMemberId = id
            };
            return View(inputModel);
        }

        [HttpPost]
        public IActionResult Create(int id, CreateEditEventViewModel inputModel)
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
                ScheduleEvent newEvent = new ScheduleEvent()
                {
                    Title = inputModel.Title,
                    StartTime = inputModel.StartTime,
                    EndTime = inputModel.EndTime,
                    FamilyMemberId = selectedMember.Id,
                };

                dbContext.ScheduleEvents.Add(newEvent);
                dbContext.SaveChanges();
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
            public IActionResult Edit(int id)
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                ScheduleEvent? selectedEvent = dbContext
                    .ScheduleEvents
                    .SingleOrDefault(e => e.Id == id);

                if (selectedEvent == null)
                {
                    return NotFound();
                }

                CreateEditEventViewModel inputModel = new CreateEditEventViewModel()
                {
                    Title = selectedEvent.Title,
                    StartTime = selectedEvent.StartTime,
                    EndTime = selectedEvent.EndTime,
                    FamilyMemberId = selectedEvent.FamilyMemberId
                };

                return View(inputModel);
            }

            [HttpPost]
            public IActionResult Edit(int id, CreateEditEventViewModel inputModel)
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                ScheduleEvent? selectedEvent = dbContext
                    .ScheduleEvents
                    .SingleOrDefault(e => e.Id == id);

                if (selectedEvent == null)
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
                    selectedEvent.Title = inputModel.Title;
                    selectedEvent.StartTime = inputModel.StartTime;
                    selectedEvent.EndTime = inputModel.EndTime;

                    dbContext.SaveChanges();
                    return RedirectToAction("Details", "FamilyMembers", new { id = selectedEvent.FamilyMemberId });
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

                ScheduleEvent? selectedEvent = dbContext
                    .ScheduleEvents
                    .SingleOrDefault(e => e.Id == id);

                if (selectedEvent == null)
                {
                    return NotFound();
                }

            DeleteEventViewModel viewModel = new DeleteEventViewModel()
                {
                    Title = selectedEvent.Title,
                    StartTime = selectedEvent.StartTime.ToString(),
                    EndTime = selectedEvent.EndTime.ToString(),
                    FamilyMemberId = selectedEvent.FamilyMemberId
                };

                return View(viewModel);
            }

            [HttpPost]
            public IActionResult Delete(int id, DeleteEventViewModel viewModel)
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                ScheduleEvent? selectedEvent = dbContext
                    .ScheduleEvents
                    .SingleOrDefault(e => e.Id == id);

                if (selectedEvent == null)
                {
                    return NotFound();
                }

            try
                {
                    dbContext.ScheduleEvents.Remove(selectedEvent);
                    dbContext.SaveChanges();
                    return RedirectToAction("Details", "FamilyMembers", new { id = selectedEvent.FamilyMemberId });
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
