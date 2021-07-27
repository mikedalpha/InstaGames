using System.Web.Mvc;

namespace GroupProject.WebApp.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        // GET: Error
       
        public ActionResult PageNotFound()
        {
            return View();
        }
       
        public ActionResult InternalServerError()
        {
            return View();
        }
    }
}