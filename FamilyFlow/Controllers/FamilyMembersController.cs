using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.Web.ViewModels.FamilyMember;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace FamilyFlow.Controllers
{
    public class FamilyMembersController : Controller
    {
        private readonly IFamilyMemberService familyMemberService;
        private readonly UserManager<IdentityUser> userManager;

        public FamilyMembersController(IFamilyMemberService familyMemberService, UserManager<IdentityUser> userManager)
        {
            this.familyMemberService = familyMemberService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> All()
        {
            string userId = userManager.GetUserId(User);

            IEnumerable<AllFamilyMembersViewModel> members = await familyMemberService
            .GetAllFamilyMembersAsync(userId);
            return View(members);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            string userId = userManager.GetUserId(User);

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
        public IActionResult Create()
        {
            return View(new CreateFamilyMemberViewModel());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateFamilyMemberViewModel inputModel)
        {
            string userId = userManager.GetUserId(User);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Model Validation failed.");
                return View(inputModel);
            }
            try
            {
                await familyMemberService.CreateFamilyMemberAsync(inputModel, userId);
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
