using System.ComponentModel.DataAnnotations;

namespace CoreIdentity.Models
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
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
    }
}
