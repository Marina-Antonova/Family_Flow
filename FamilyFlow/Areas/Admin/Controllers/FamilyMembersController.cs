using FamilyFlow.Data.Models;
using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.Web.ViewModels.FamilyMember;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFlow.Web.Areas.Admin.Controllers
{
    public class FamilyMembersController : Controller
    {
        private readonly IFamilyMemberService familyMemberService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public FamilyMembersController(
            IFamilyMemberService familyMemberService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.familyMemberService = familyMemberService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
                ApplicationUser? currentUser = await userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    await userManager.UpdateSecurityStampAsync(currentUser);
                    await signInManager.SignInAsync(currentUser, isPersistent: false);
                }

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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
