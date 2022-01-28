using CoreIdentity.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreIdentity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }
        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            RegisterViewModel = new RegisterViewModel();
            this.userManager = userManager;
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
            var user = new IdentityUser
            {
                UserName = RegisterViewModel.Email,
                Email = RegisterViewModel.Email,
            };
            var result = await userManager.CreateAsync(user, RegisterViewModel.Password);
            if (result.Succeeded)
            {
                var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                if (confirmationToken != null)
                {
                    return Redirect(Url.PageLink("/Account/ConfirmEmail", values: new
                    {
                        userId = user.Id,
                        token = confirmationToken
                    }) ?? string.Empty);
                }
                return Page();
            } else
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
