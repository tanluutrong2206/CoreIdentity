using Microsoft.AspNetCore.Identity;

namespace CoreIdentity.Data.Account
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Address = string.Empty;
            IdentityCard = string.Empty;
        }
        public string Address { get; set; }
        public string IdentityCard { get; set; }
    }
}
