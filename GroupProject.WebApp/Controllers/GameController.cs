using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ExceptionLogger;
using GroupProject.RepositoryService;
using GroupProject.WebApp.Models.GameViewModels;

namespace GroupProject.WebApp.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private IUnitOfWork unitOfWork;
        private ILog iLog;
        public GameController()
        {
            iLog = Log.GetInstance;
            unitOfWork = new UnitOfWork();
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            iLog.LogException(filterContext.Exception.ToString());
            filterContext.ExceptionHandled = true;
            this.Redirect("~/Error/InternalServerError").ExecuteResult(this.ControllerContext);
        }

        public ActionResult MultiPlayerGames()
        {
            return View();
        }
        public async Task<ActionResult> SinglePlayer()
        {
            var games = await unitOfWork.Games.GetAllAsync();
            if (games == null) return RedirectToAction("InternalServerError", "Error");

           
            var id = new Random().Next(1,6);
            var game = await unitOfWork.Games.FindByIdAsync(id);

            if (game == null) return RedirectToAction("InternalServerError", "Error");

            var categories = await unitOfWork.Category.GetAllAsync();
            if(categories == null) return RedirectToAction("InternalServerError", "Error");

            var vm = new SinglePlayerViewModel(games.ToList(),game , categories.ToList());

            return View(vm);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}