﻿using System;
using System.Web.Mvc;
using System.Linq;
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
            _userManager = userManager;
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

            IndexViewModel vm = new IndexViewModel(games.ToList());

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
                Text = model.Message
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
                TempData["ShowAlert"] = true;
                return RedirectToAction("Contact", "Home");
            }

            return RedirectToAction("InternalServerError", "Error");
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
