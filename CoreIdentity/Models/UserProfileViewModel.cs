namespace CoreIdentity.Models
{
    public class UserProfileViewModel
    {
        public UserProfileViewModel()
        {
            Email = string.Empty;
            Address = string.Empty;
            IdentityCard = string.Empty;
        }
        public string Email { get; set; }
        public string Address { get; set; }
        public string IdentityCard { get; set; }
    }
}
