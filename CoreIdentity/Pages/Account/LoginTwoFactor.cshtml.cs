using CoreIdentity.Data.Account;
using CoreIdentity.Models;
using CoreIdentity.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreIdentity.Pages.Account
{
    public class LoginTwoFactorModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailService emailService;
        private readonly SignInManager<ApplicationUser> signInManager;

        [BindProperty]
        public TwoFAViewModel TwoFAViewModel { get; set; }
        public LoginTwoFactorModel(UserManager<ApplicationUser> userManager, IEmailService emailService, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.signInManager = signInManager;
            TwoFAViewModel = new TwoFAViewModel();
        }
        public async Task<IActionResult> OnGet(string email, bool rememberMe)
        {
            var user = await userManager.FindByNameAsync(email);
            var securityCode = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

            await emailService.SendEmailAsync("tanluutrong2206@gmail.com", email, "Verify Login", $"Please use this code as OTP: {securityCode}");

            TwoFAViewModel = new TwoFAViewModel
            {
                SecurityCode = string.Empty,
                RememberMe = rememberMe,
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var res = await signInManager.TwoFactorSignInAsync("Email", TwoFAViewModel.SecurityCode, TwoFAViewModel.RememberMe, TwoFAViewModel.RememberClient);
            if (res.Succeeded)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                if (res.IsLockedOut)
                {
                    ModelState.AddModelError("Login2FA", "You are locked out");
                }
                else
                {
                    ModelState.AddModelError("Login2FA", "Failed to login");
                }

                return Page();
            }
        }
    }
}
