using FamilyFlow.Common;
using FamilyFlow.Data;
using FamilyFlow.Data.Models;
using FamilyFlow.Data.Models.Enums;
using FamilyFlow.ViewModels.FamilyMember;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyFlow.Controllers
{
    public class FamilyMembersController : Controller
    {
        private readonly FamilyFlowDbContext dbContext;

        public FamilyMembersController(FamilyFlowDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public IActionResult All()
        {
            IEnumerable<AllFamilyMembersViewModel> members = dbContext
                .FamilyMembers
                .Include(fm => fm.HouseTasks)
                .Include(fm => fm.ScheduleEvents)
                .Select(fm => new AllFamilyMembersViewModel()
                {
                    Id = fm.Id,
                    Name = fm.Name,
                    Role = fm.Role.ToString(),
                    RoleImagePath = fm.Role.GetImagePath(),
                    Age = fm.Age,
                    HouseTasksCount = fm.HouseTasks.Count,
                    ScheduleEventsCount = fm.ScheduleEvents.Count
                })
                .OrderBy(fm => fm.Name)
                .ToList();
            return View(members);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            FamilyMember? selectedMember = dbContext
                 .FamilyMembers
                 .Include(fm => fm.HouseTasks)
                 .Include(fm => fm.ScheduleEvents)
                 .SingleOrDefault(fm => fm.Id == id);

            if (selectedMember == null)
            {
                return NotFound();
            }
            DetailsFamilyMemberViewModel viewModel = new DetailsFamilyMemberViewModel()
            {
                Id = selectedMember.Id,
                Name = selectedMember.Name,
                Age = selectedMember.Age,
                Role = selectedMember.Role.ToString(),
                Tasks = selectedMember.HouseTasks
                    .Select(t => new DetailsHouseTaskViewModel
                    {
                        Id = t.Id,
                        Title = t.Title,
                        DueDate = t.DueDate
                    })
                    .ToList(),
                Events = selectedMember.ScheduleEvents
                    .Select(e => new DetailsScheduleEventsViewModel
                    {
                        Id = e.Id,
                        Title = e.Title,
                        StartTime = e.StartTime,
                        EndTime = e.EndTime
                    })
                    .ToList()
            };
            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            CreateFamilyMemberViewModel inputModel = new CreateFamilyMemberViewModel()
            {
            };
            return View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(FamilyMember inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Model Validation failed.");
                return View(inputModel);
            }
            try
            {
                FamilyMember member = new FamilyMember
                {
                    Name = inputModel.Name,
                    Role = inputModel.Role,
                    Age = inputModel.Age
                };
                dbContext.FamilyMembers.Add(member);
                dbContext.SaveChanges();
                return RedirectToAction("All");
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

            FamilyMember? selectedMember = dbContext
                 .FamilyMembers
                 .SingleOrDefault(fm => fm.Id == id);

            if (selectedMember == null)
            {
                return NotFound();
            }
            CreateFamilyMemberViewModel inputModel = new CreateFamilyMemberViewModel()
            {
                Id = selectedMember.Id,
                Name = selectedMember.Name,
                Role = selectedMember.Role,
                Age = selectedMember.Age
            };
            return View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, CreateFamilyMemberViewModel inputModel)
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

            FamilyMember? selectedMember = dbContext
                 .FamilyMembers
                 .SingleOrDefault(fm => fm.Id == id);

            if (selectedMember == null)
            {
                return NotFound();
            }

            try
            {
                selectedMember.Name = inputModel.Name;
                selectedMember.Role = inputModel.Role.Value;
                selectedMember.Age = inputModel.Age;

                dbContext.SaveChanges();
                return RedirectToAction("All");
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

            FamilyMember? selectedMember = dbContext
                 .FamilyMembers
                 .SingleOrDefault(fm => fm.Id == id);

            if (selectedMember == null)
            {
                return NotFound();
            }
            DeleteFamilyMemberViewModel viewModel = new DeleteFamilyMemberViewModel()
            {
                Id = selectedMember.Id,
                Name = selectedMember.Name,
                Role = selectedMember.Role.ToString()
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int id, DeleteFamilyMemberViewModel? viewModel)
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
            try
            {
                dbContext.FamilyMembers.Remove(selectedMember);
                dbContext.SaveChanges();
                return RedirectToAction("All");
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
