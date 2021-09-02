using System;
using System.Configuration;
using System.Web.Mvc;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using ExceptionLogger;
using GroupProject.Entities.Domain_Models;
using GroupProject.RepositoryService;
using GroupProject.WebApp.Models.HomeViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GroupProject.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private ApplicationUserManager _userManager;

        private ILog iLog;

        public HomeController()
        {
            iLog = Log.GetInstance;
            unitOfWork = new UnitOfWork();
        }

        public HomeController(ApplicationUserManager userManager)
        {
            iLog = Log.GetInstance;
            _userManager = userManager;
            unitOfWork = new UnitOfWork();
        }

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

        protected override void OnException(ExceptionContext filterContext)
        {
            iLog.LogException(filterContext.Exception.ToString());
            filterContext.ExceptionHandled = true;
            this.Redirect("~/Error/InternalServerError").ExecuteResult(this.ControllerContext);
        }

        public async Task<ActionResult> Index()
        {
            var games = await unitOfWork.Games.GetAllAsync();
            if (games == null) return RedirectToAction("InternalServerError", "Error");

           

            var appuser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (appuser == null) return RedirectToAction("InternalServerError", "Error");

            var ratedGame = await unitOfWork.UserGameRatings.GetAllAsync();

            IndexViewModel vm = new IndexViewModel(games.ToList(), appuser, ratedGame.ToList());
            
            var index = new Random().Next(vm.LatestGames.ToArray().Length);
            var randomGame = await unitOfWork.Games.FindByIdAsync(vm.LatestGames[index].GameId);
            if (randomGame == null) return RedirectToAction("InternalServerError", "Error");
            vm.RandomGame = randomGame;

            return View(vm);
        }
        public async Task<ActionResult> MyList()
        {
            
            var appuser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (appuser == null) return RedirectToAction("InternalServerError", "Error");

            IndexViewModel vm = new IndexViewModel(appuser);
            return View(vm);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            var vm = new ContactViewModel();
            return View(vm);
        }

        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult PricingPlan()
        {
            var vm = new PricingPlanViewModel();
            return View(vm);
        }

        public ActionResult TermsOfUse()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> SaveUserMessage(ContactViewModel model)
        {
            if (!ModelState.IsValid) return View("Contact", model);

            var message = new Message()
            {
                SubmitDate = DateTime.Now,
                Text = model.Message,
                Answered = false
            };

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null) RedirectToAction("InternalServerError", "Error");

            if (model.PhoneNumber != null)
            {
                user.PhoneNumber = model.PhoneNumber;
            }

            user.Messages.Add(message);

            var result = await UserManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                SendMailWeReceivedYourMessage(message);
                TempData["ShowAlert"] = true;
                return RedirectToAction("Contact", "Home");
            }

            return RedirectToAction("InternalServerError", "Error");
        }

        private void SendMailWeReceivedYourMessage(Message message)
        {
            var mail = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));

            mail.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString());
            mail.To.Add(message.Creator.Email);
            mail.Subject = "InstaGames received your Message";
            mail.Body = message.Creator.UserName + " InstaGames team received your Message.\n\nWe will get back to you as soon as possible. \n\n\n Best Wishes,\nInstaGames Team.";

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["Email"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(mail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                unitOfWork.Dispose();
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }
    }
}
