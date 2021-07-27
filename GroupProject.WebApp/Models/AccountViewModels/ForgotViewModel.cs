using System.ComponentModel.DataAnnotations;

namespace GroupProject.WebApp.Models.AccountViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
