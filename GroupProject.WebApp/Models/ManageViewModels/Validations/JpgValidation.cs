using System.ComponentModel.DataAnnotations;
using System.Web;

namespace GroupProject.WebApp.Models.ManageViewModels.Validations
{
    public class JpgValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;

            if (file == null) return false;

            var fileName = file.FileName;

            return fileName.Substring(fileName.Length - 3) == "jpg";
        }
    }
}