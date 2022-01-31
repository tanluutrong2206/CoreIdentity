using System.ComponentModel.DataAnnotations;

namespace CoreIdentity.Models
{
    public class TwoFAViewModel
    {
        public TwoFAViewModel()
        {
            SecurityCode = string.Empty;
        }
        [Required]
        [Display(Name = "Security Code")]
        public string SecurityCode { get; set; }
        [Display(Name = "Do not ask again in this browser")]
        public bool RememberClient { get; set; }
        public bool RememberMe { get; set; }
    }
}
