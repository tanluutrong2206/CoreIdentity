using CoreIdentity.Data.Account;
using CoreIdentity.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Security.Claims;

namespace CoreIdentity.Pages.Account
{
    [Authorize]
    public class UserProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        [BindProperty]
        public UserProfileViewModel UserProfileViewModel { get; set; }
        [BindProperty]
        public string? SuccessMessage { get; set; }
        public UserProfileModel(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            UserProfileViewModel = new UserProfileViewModel();
        }
        public async Task<IActionResult> OnGetAsync()
        {
            SuccessMessage = string.Empty;
            var (currentUser, addressClaim, identityCardClaim) = await GetCurrenUserInformationAsync();
            UserProfileViewModel = new UserProfileViewModel
            {
                Email = currentUser.Email,
                Address = addressClaim?.Value ?? string.Empty,
                IdentityCard = identityCardClaim?.Value ?? string.Empty,
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var (currentUser, addressClaim, identityCardClaim) = await GetCurrenUserInformationAsync();

            try
            {
                await userManager.ReplaceClaimAsync(currentUser, addressClaim, new Claim("Address", UserProfileViewModel.Address));
                await userManager.ReplaceClaimAsync(currentUser, identityCardClaim, new Claim("IdentityCard", UserProfileViewModel.IdentityCard));
                SuccessMessage = "User Profile save successfully.";
            }
            catch
            {
                ModelState.AddModelError("UserProfile", "Error occured when updating user profile");
            }
            return Page();
        }

        private async Task<(ApplicationUser user, Claim? addressClaim, Claim? identityCardClaim)> GetCurrenUserInformationAsync()
        {
            var currentUser = await userManager.FindByNameAsync(User.Identity?.Name);
            var claims = await userManager.GetClaimsAsync(currentUser);
            var addressClaim = claims.FirstOrDefault(x => x.Type == "Address");
            var identityCardClaim = claims.FirstOrDefault(x => x.Type == "IdentityCard");

            return (currentUser, addressClaim, identityCardClaim);
        }
    }
}
