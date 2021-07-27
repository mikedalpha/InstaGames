using System.Web.Mvc;
using GroupProject.Database;

namespace GroupProject.WebApp.Controllers
{
    [Authorize]
    public class GameController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult MultiPlayerGames()
        {
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null) return Redirect("~/Error/PageNotFound");

            var game = db.Games.Find(id);

            if (game == null) return Redirect("~/Error/InternalServerError");

            return View(game);
        }

        public ActionResult Play(int? id)
        {
            if (id == null) return Redirect("~/Error/PageNotFound");

            var game = db.Games.Find(id);

            if (game == null) return Redirect("~/Error/InternalServerError");

            if(game.GameUrl == null) return Redirect("~/Error/PageNotFound");

            if (game.IsEarlyAccess != null && game.IsEarlyAccess.Value) TempData["ShowAlert"] = true;

            return User.IsInRole("Subscriber") ? (ActionResult)View(game) : Redirect("~/Home/PricingPlan");
        }
    }
}