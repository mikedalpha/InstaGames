using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupProject.WebApp.Models.HomeViewModel
{
    public class ContactViewModel
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

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Not a valid email Address.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Message { get; set; }

    }
}