using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreIdentity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        [BindProperty]
        public string Message { get; set; }
        public ConfirmEmailModel(UserManager<IdentityUser> userManager)
        {
            Message = string.Empty;
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                Message = "Failed to validate email";
            }
            else
            {
                var res = await userManager.ConfirmEmailAsync(user, token);
                if (res.Succeeded)
                {
                    Message = "Email address is confirm successfully.";
                }
            }
            return Page();
        }
    }
}
