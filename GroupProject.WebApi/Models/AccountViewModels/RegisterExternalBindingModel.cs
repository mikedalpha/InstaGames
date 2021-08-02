using System.ComponentModel.DataAnnotations;

namespace GroupProject.WebApi.Models.AccountViewModels
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}