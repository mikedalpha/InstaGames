using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using GroupProject.WebApp.Models.ManageViewModels.Validations;

namespace GroupProject.WebApp.Models.ManageViewModels
{
    public class UploadPhotoViewModel
    {
        public string Username { get; set; }

        public string Photo { get; set; }

        [JpgValidation(ErrorMessage = "Please select a .jpg file.")]
        
        public HttpPostedFileBase PhotoCreate { get; set; }

        public DateTime? RegistrationDate { get; set; }
    }
}