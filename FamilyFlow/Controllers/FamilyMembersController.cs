using FamilyFlow.Data.Models;
using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.Web.ViewModels.FamilyMember;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;


namespace FamilyFlow.Controllers
{
    public class FamilyMembersController : Controller
    {
        private readonly IFamilyMemberService familyMemberService;
        private readonly IFamilyService familyService;
        private readonly UserManager<ApplicationUser> userManager;

        public FamilyMembersController(IFamilyService familyService, IFamilyMemberService familyMemberService, UserManager<ApplicationUser> userManager)
        {
            this.familyMemberService = familyMemberService;
            this.userManager = userManager;
            this.familyService = familyService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> All()
        {
            string userId = userManager.GetUserId(User);

            if(string.IsNullOrEmpty(userId))
            { 
                return RedirectToAction("Login", "Account");
            }

            var family = await familyService.GetFamilyForUserAsync(userId);

            if (family == null)
            {
                return RedirectToAction("MyFamily", "Family");
            }

            IEnumerable<AllFamilyMembersViewModel> members = await familyMemberService.GetAllFamilyMembersAsync(userId);
            return View(members);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            string userId = userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            if (id <= 0)
            {
                return BadRequest();
            }

            DetailsFamilyMemberViewModel? selectedMember = await familyMemberService.GetDetailsForFamilyMemberAsync(id, userId);

            if (selectedMember == null)
            {
                return NotFound();
            }

            return View(selectedMember);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            string userId = userManager.GetUserId(User);
            int familyId = await familyService.GetFamilyIdForUserAsync(userId);


            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            if (familyId == null || familyId <= 0)
            {
                return RedirectToAction("MyFamily", "Family");
            }

            var model = new CreateFamilyMemberViewModel()
            {
                UserId = userId,
                FamilyId = familyId
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateFamilyMemberViewModel inputModel)
        {
            string userId = userManager.GetUserId(User);
            int familyId = await familyService.GetFamilyIdForUserAsync(userId);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Model Validation failed.");
                return View(inputModel);
            }
            try
            {
                await familyMemberService.CreateFamilyMemberAsync(inputModel, userId, familyId);
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
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var selectedMember = await familyMemberService.GetForEditFamilyMemberAsync(id);

            if (selectedMember == null)
            {
                return NotFound();
            }

            return View(selectedMember);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, CreateFamilyMemberViewModel inputModel)
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

            try
            {
                await familyMemberService.EditFamilyMemberAsync(id, inputModel);
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
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var selectedMember = await familyMemberService.GetForDeleteFamilyMemberAsync(id);

            if (selectedMember == null)
            {
                return NotFound();
            }

            return View(selectedMember);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id, DeleteFamilyMemberViewModel? inputModel)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            try
            {
                await familyMemberService.DeleteFamilyMemberAsync(id, inputModel);
                return RedirectToAction("All");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while deleting. Please try again later.");
                return View(inputModel);
            }
        }
    }
}
