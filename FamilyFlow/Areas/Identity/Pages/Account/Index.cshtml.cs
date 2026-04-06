using FamilyFlow.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyFlow.Areas.Identity.Pages.Account
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public string UserName { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        public bool IsEmailConfirmed { get; private set; }

        public string RoleSummary { get; private set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync()
        {
            ApplicationUser? user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge();
            }

            UserName = user.UserName ?? string.Empty;
            Email = user.Email ?? string.Empty;
            IsEmailConfirmed = user.EmailConfirmed;

            IList<string> roles = await userManager.GetRolesAsync(user);
            RoleSummary = roles.Count > 0 ? string.Join(", ", roles) : "No roles assigned";

            return Page();
        }
    }
}
