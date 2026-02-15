using FamilyFlow.Data;
using FamilyFlow.Data.Models;
using FamilyFlow.ViewModels.HouseTasks;
using FamilyFlow.ViewModels.ScheduleEvent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [Authorize]
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
                Adults = GetAllAdults().ToList(),
                FamilyMemberId = id
            };
            return View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(int id, CreateEditEventViewModel inputModel)
        {
            inputModel.Adults = GetAllAdults().ToList();

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
        
            if (selectedMember.Age <= 12 && !inputModel.AccompanyingAdultId.HasValue)
            {
                ModelState.AddModelError(nameof(inputModel.AccompanyingAdultId), "Family member under 12 must have an accompanying adult.");
                return View(inputModel);
            }

            if (inputModel.StartTime >= inputModel.EndTime)
            {
                ModelState.AddModelError(nameof(inputModel.StartTime), "Start Time must be earlier than the End Time");
                return View(inputModel);
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
                    AccompanyingAdultId = inputModel.Id
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
        [Authorize]
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
                FamilyMemberId = selectedEvent.FamilyMemberId,
                AccompanyingAdultId = selectedEvent.AccompanyingAdultId,
                    Adults = GetAllAdults().ToList()
            };

                return View(inputModel);
            }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, CreateEditEventViewModel inputModel)
            {
                inputModel.Adults = GetAllAdults().ToList();
                if (id <= 0)
                {
                    return BadRequest();
                }

                ScheduleEvent? selectedEvent = dbContext
                    .ScheduleEvents
                    .Include(se => se.FamilyMemberScheduleEvents)
                    .SingleOrDefault(e => e.Id == id);

                if (selectedEvent == null)
                {
                    return NotFound();
                }

                if (inputModel.StartTime >= inputModel.EndTime)
                {
                    ModelState.AddModelError(nameof(inputModel.StartTime), "Start Time must be earlier than the End Time");
                    return View(inputModel);
                }

                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, "Model Validation failed.");
                    return View(inputModel);
                }

                if (selectedEvent.FamilyMemberScheduleEvents.Age <= 12 && !inputModel.AccompanyingAdultId.HasValue)
                {
                    ModelState.AddModelError(nameof(inputModel.AccompanyingAdultId),"Family member under 12 must have an accompanying adult.");
                    return View(inputModel);
                }
                
                try
                {
                    selectedEvent.Title = inputModel.Title;
                    selectedEvent.StartTime = inputModel.StartTime;
                    selectedEvent.EndTime = inputModel.EndTime;
                    selectedEvent.AccompanyingAdultId = inputModel.AccompanyingAdultId;

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
        [Authorize]
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
        [Authorize]
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
        private IEnumerable<CreateEditAdultViewModel> GetAllAdults()
        {
            return dbContext
             .FamilyMembers
             .AsNoTracking()
             .Where(a => a.Age >= 18)
             .OrderBy(a => a.Name)
             .Select(a => new CreateEditAdultViewModel()
             {
                 Id = a.Id,
                 Name = a.Name
             })
             .ToArray();
        }
    }
}
