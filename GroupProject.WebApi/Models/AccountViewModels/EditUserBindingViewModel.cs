using System;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.WebApi.Models.AccountViewModels
{
    public class EditUserBindingViewModel
    {
        
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        [MinLength(4, ErrorMessage = "Username must be at least 4 characters long.")]
        [MaxLength(20, ErrorMessage = "Username must be less than 20 characters long.")]
        public string UserName { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        [MinLength(4, ErrorMessage = "First Name must be at least 4 characters long.")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "First name must contain only letters.")]
        public string FirstName { get; set; }
       
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        [MinLength(4, ErrorMessage = "Last Name must be at least 4 characters long.")]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        
        [EmailAddress(ErrorMessage = "Not a valid email Address.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public string Photo { get; set; }

        public DateTime? ExpireDate { get; set; }

        public DateTime? RegistrationDate { get; set; }

    }
}