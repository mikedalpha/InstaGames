using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ExceptionLogger;
using GroupProject.RepositoryService;
using GroupProject.WebApp.Models.GameViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GroupProject.WebApp.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private IUnitOfWork unitOfWork;
        private ApplicationUserManager _userManager;
        private ILog iLog;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public GameController()
        {
            iLog = Log.GetInstance;
            unitOfWork = new UnitOfWork();
        }
        public GameController(ApplicationUserManager userManager)
        {
            iLog = Log.GetInstance;
            _userManager = userManager;
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

            var appuser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (appuser == null) return RedirectToAction("InternalServerError", "Error");

            var ratedGame = await unitOfWork.UserGameRatings.GetAllAsync();

            var vm = new SinglePlayerViewModel(games.ToList(),game , categories.ToList() , appuser, ratedGame.ToList());

            return View(vm);
        }

        public async Task<ActionResult> Play(int? id)
        {
            if (id == null) return Redirect("~/Error/PageNotFound");

            var game = await unitOfWork.Games.FindByIdAsync(id);

            if (game == null) return Redirect("~/Error/InternalServerError");

            if(game.GameUrl == null) return Redirect("~/Error/PageNotFound");

            if (game.IsEarlyAccess != null && game.IsEarlyAccess.Value) TempData["ShowAlert"] = true;

            return User.IsInRole("Subscriber") || User.IsInRole("Admin") ? (ActionResult)View(game) : Redirect("~/Home/PricingPlan");
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