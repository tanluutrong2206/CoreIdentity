using CoreIdentity.Data.Account;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.ComponentModel.DataAnnotations;

namespace CoreIdentity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }
        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            LoginViewModel = new LoginViewModel();
            this.signInManager = signInManager;
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

            var res = await signInManager.PasswordSignInAsync(LoginViewModel.Email, LoginViewModel.Password, LoginViewModel.RememberMe, false);
            if (res.Succeeded)
            {
                return RedirectToPage("/Index");
            } else
            {
                if (res.IsLockedOut)
                {
                    ModelState.AddModelError("Login", "You are locked out");
                } else
                {
                    ModelState.AddModelError("Login", "Failed to login");
                }

                return Page();
            }
        }
    }

    public class LoginViewModel
    {
        public LoginViewModel()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
