using FamilyFlow.Services.Core;
using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.Web.ViewModels.Family;
using FamilyFlow.Web.ViewModels.FamilyMember;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFlow.Web.Controllers
{
    [Authorize]
    public class FamilyController : Controller
    {
        private readonly IFamilyService familyService;
        private readonly UserManager<IdentityUser> userManager;

        public FamilyController(IFamilyService familyService, UserManager<IdentityUser> userManager)
        {
            this.familyService = familyService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> MyFamily()
        {
            string? userId = userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var family = await familyService.GetFamilyForUserAsync(userId);

            if (family == null)
            {
                return RedirectToAction("Create");
            }

            return View(family);
        }

        [HttpGet]
        public IActionResult Create()
        {
            FamilyViewModel model = new FamilyViewModel()
            {
                UserId = userManager.GetUserId(User)
            };

            return View (model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FamilyViewModel model)
        {
            string? userId = userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await familyService.CreateFamilyAsync(model);
            return RedirectToAction("MyFamily");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var family = await familyService.GetFamilyAsync(id);

            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FamilyViewModel inputModel)
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
                await familyService.EditFamilyAsync(id, inputModel);
                return RedirectToAction("MyFamily");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while editing. Please try again later.");
                return View(inputModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var family = await familyService.GetFamilyAsync(id);

            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id, FamilyViewModel? viewModel)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            try
            {
                await familyService.DeleteFamilyAsync(id, viewModel);
                return RedirectToAction("index", "Home");
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
