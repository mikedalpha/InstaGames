﻿using System.Data.Entity;
using System.Web.Mvc;
using GroupProject.Database;
using GroupProject.WebApp.Models;
using System.Linq;
using System.Threading.Tasks;
using ExceptionLogger;
using GroupProject.RepositoryService;
using GroupProject.WebApp.Models.HomeViewModel;

namespace GroupProject.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        private ILog iLog;

        public HomeController()
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
            return View();
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
    }
}