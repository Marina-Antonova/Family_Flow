using FamilyFlow.Data.Models;
using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.Web.ViewModels.Family;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFlow.Web.Controllers
{
    [Authorize]
    public class FamilyController : Controller
    {
        private readonly IFamilyService familyService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public FamilyController(
            IFamilyService familyService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.familyService = familyService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> MyFamily()
        {
            string? userId = userManager.GetUserId(User);
            ApplicationUser? currentUser = await userManager.GetUserAsync(User);

            if (string.IsNullOrEmpty(userId) || currentUser == null)
            {
                await signInManager.SignOutAsync();
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
            return View(new FamilyViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(FamilyViewModel model)
        {
            string? userId = userManager.GetUserId(User);
            ApplicationUser? currentUser = await userManager.GetUserAsync(User);

            if (string.IsNullOrEmpty(userId) || currentUser == null)
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await familyService.CreateFamilyAsync(model, userId);
            return RedirectToAction("MyFamily");
        }

        //[HttpGet]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    if (id <= 0)
        //    {
        //        return BadRequest();
        //    }
        //    var family = await familyService.GetFamilyAsync(id);

        //    if (family == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(family);
        //}

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Edit(int id, FamilyViewModel inputModel)
        //{
        //    if (id <= 0)
        //    {
        //        return BadRequest();
        //    }


        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError(string.Empty, "Model Validation failed.");
        //        return View(inputModel);
        //    }

        //    try
        //    {
        //        await familyService.EditFamilyAsync(id, inputModel);
        //        return RedirectToAction("MyFamily");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        ModelState.AddModelError(string.Empty, "Unexpected error occurred while editing. Please try again later.");
        //        return View(inputModel);
        //    }
        //}

        //[HttpGet]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (id <= 0)
        //    {
        //        return BadRequest();
        //    }
        //    var family = await familyService.GetFamilyAsync(id);

        //    if (family == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(family);
        //}

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Delete(int id, FamilyViewModel? viewModel)
        //{
        //    if (id <= 0)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        await familyService.DeleteFamilyAsync(id, viewModel);
        //        return RedirectToAction("index", "Home");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        ModelState.AddModelError(string.Empty, "Unexpected error occurred while deleting. Please try again later.");
        //        return View(viewModel);
        //    }
        //}
    }
}
