using System;
using System.ComponentModel.DataAnnotations;
using GroupProject.WebApp.Models.AccountViewModels.Validations;

namespace GroupProject.WebApp.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Not a valid email Address.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        [MinLength(4, ErrorMessage = "Username must be at least 4 characters long.")]
        [MaxLength(20, ErrorMessage = "Username must be less than 20 characters long.")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        [DateMinimumAge(18, ErrorMessage = "{0} must be at least {1} years of age")]
        public DateTime DateOfBirth { get; set; }

    }
}
