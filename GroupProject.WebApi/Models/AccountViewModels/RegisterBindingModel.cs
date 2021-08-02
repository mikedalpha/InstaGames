using System;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.WebApi.Models.AccountViewModels
{
    public class RegisterBindingModel
    {

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        [MinLength(4, ErrorMessage = "First Name must be at least 4 characters long.")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "First name must contain only letters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        [MinLength(4, ErrorMessage = "Last Name must be at least 4 characters long.")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        [MinLength(4, ErrorMessage = "Username must be at least 4 characters long.")]
        [MaxLength(20, ErrorMessage = "Username must be less than 20 characters long.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Not a valid email Address.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}