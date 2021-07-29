using System.Threading.Tasks;
using System.Web.Mvc;
using GroupProject.RepositoryService;

namespace GroupProject.WebApp.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private IUnitOfWork unitOfWork;

        public GameController()
        {
            unitOfWork = new UnitOfWork();
        }

        public ActionResult MultiPlayerGames()
        {
            return View();
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) return Redirect("~/Error/PageNotFound");

            var game = await unitOfWork.Games.FindByIdAsync(id);

            if (game == null) return Redirect("~/Error/InternalServerError");

            return View(game);
        }

        public async Task<ActionResult> Play(int? id)
        {
            if (id == null) return Redirect("~/Error/PageNotFound");

            var game = await unitOfWork.Games.FindByIdAsync(id);

            if (game == null) return Redirect("~/Error/InternalServerError");

            if(game.GameUrl == null) return Redirect("~/Error/PageNotFound");

            if (game.IsEarlyAccess != null && game.IsEarlyAccess.Value) TempData["ShowAlert"] = true;

            return User.IsInRole("Subscriber") ? (ActionResult)View(game) : Redirect("~/Home/PricingPlan");
        }
    }
}