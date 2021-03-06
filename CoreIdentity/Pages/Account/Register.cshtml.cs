using CoreIdentity.Data.Account;
using CoreIdentity.Models;
using CoreIdentity.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Security.Claims;

namespace CoreIdentity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;

        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }
        public IEmailService EmailService { get; }

        public RegisterModel(UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            RegisterViewModel = new RegisterViewModel();
            this.userManager = userManager;
            EmailService = emailService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = new ApplicationUser
            {
                UserName = RegisterViewModel.Email,
                Email = RegisterViewModel.Email,
                Address = RegisterViewModel.Address,
                IdentityCard = RegisterViewModel.IdentityCard,
            };

            var userClaims = new List<Claim>
            {
                new Claim("Address", RegisterViewModel.Address),
                new Claim("IdentityCard", RegisterViewModel.IdentityCard)
            };
            var result = await userManager.CreateAsync(user, RegisterViewModel.Password);
            if (result.Succeeded)
            {
                await userManager.AddClaimsAsync(user, userClaims);
                var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                if (confirmationToken != null)
                {
                    var confirmationLink = Url.PageLink("/Account/ConfirmEmail", values: new
                    {
                        userId = user.Id,
                        token = confirmationToken
                    }) ?? string.Empty;
                    await EmailService.SendEmailAsync("tanluutrong2206@gmail.com", user.Email,
                        "Please confirm your email",
                        $"Please click on this link to confirm your email address: {confirmationLink}");
                    return RedirectToPage("/Account/Login");
                }
                return Page();
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("Register", item.Description);
                }
                return Page();
            }
        }
    }

}
