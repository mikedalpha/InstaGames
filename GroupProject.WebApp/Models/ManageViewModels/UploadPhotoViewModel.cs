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

        //[Required(ErrorMessage = "Please select a .jpg file.")]
        [JpgValidation(ErrorMessage = "Please select a .jpg file.")]
        //[RegularExpression(@"^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpg|.JPG)$")]
        public HttpPostedFileBase PhotoCreate { get; set; }

        public DateTime? RegistrationDate { get; set; }
    }
}