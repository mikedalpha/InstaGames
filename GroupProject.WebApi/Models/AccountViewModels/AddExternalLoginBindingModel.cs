using System.ComponentModel.DataAnnotations;

namespace GroupProject.WebApi.Models.AccountViewModels
{
    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }
}