using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace GroupProject.WebApp.Models.ManageViewModels
{
    public class UploadPhotoViewModel
    {
        public string Username { get; set; }

        public string Photo { get; set; }

        [Required(ErrorMessage = "Please select file.")]
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.jpg)$", ErrorMessage = "Only Image files allowed.")]
        public HttpPostedFileBase PhotoCreate { get; set; }

        public DateTime? RegistrationDate { get; set; }
    }
}