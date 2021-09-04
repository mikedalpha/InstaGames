using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ExceptionLogger;
using GroupProject.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using GroupProject.WebApp.Models.ManageViewModels;
using GroupProject.WebApp.Models.HomeViewModel;
using IndexViewModel = GroupProject.WebApp.Models.ManageViewModels.IndexViewModel;

namespace GroupProject.WebApp.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ILog iLog;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
            iLog = Log.GetInstance;
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            iLog = Log.GetInstance;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            iLog.LogException(filterContext.Exception.ToString());
            filterContext.ExceptionHandled = true;
            this.Redirect("~/Error/InternalServerError").ExecuteResult(this.ControllerContext);
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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


        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            var userId = User.Identity.GetUserId();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var model = new IndexViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                HasPassword = HasPassword(user),
                HasConfirmedEmail = user.EmailConfirmed,
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),
                ExpireDate = user.ExpireDate,
                SubscribePlan = user.SubscribePlan,
                RegistrationDate = user.RegistrationDate,
                Photo = user.PhotoUrl
            };
            return View(model);
        }

        // GET: /Manage/EditUser
        public async Task<ActionResult> EditUser(ManageMessageId? message)
        {
            var userId = User.Identity.GetUserId();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var model = new IndexViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                HasPassword = HasPassword(user),
                HasConfirmedEmail = user.EmailConfirmed,
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),
                ExpireDate = user.ExpireDate,
                SubscribePlan = user.SubscribePlan,
                RegistrationDate = user.RegistrationDate,
                Photo = user.PhotoUrl
            };
            return View(model);
        }

        //Edit User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                if (model.Email != user.Email)
                {
                    var isEmailAlreadyExists = UserManager.Users.Any(x => x.Email == model.Email);
                    if (isEmailAlreadyExists)
                    {
                        ModelState.AddModelError("Email", @"This Email is already in use try a different one.");
                        return View(model);
                    }
                }

                if (user.UserName != model.Username)
                {
                    var isUserNameAlreadyExists = UserManager.Users.Any(x => x.UserName == model.Username);



                    if (isUserNameAlreadyExists)
                    {
                        ModelState.AddModelError("UserName", @"This Username is already in use try a different one.");
                        return View(model);
                    }
                }

                user.UserName = model.Username;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.DateOfBirth = model.DateOfBirth;
                user.Email = model.Email;

                var result = await UserManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    AddErrors(result);
                    return View(model);
                }

                TempData["ShowEditAlert"] = true;
                return RedirectToAction("Index");
            }

            return View(model);
        }




        // POST: /Manage/SubscriptionPlan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SubscriptionPlan(PricingPlanViewModel model)
        {

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null) Redirect("~/Error/InternalServerError");

            user.SubscribePlan = model.SubscribePlan;
            await UserManager.UpdateAsync(user);

            if (user.PasswordHash == null || user.FirstName == null || user.LastName == null)
                return RedirectToAction("GetExternalUserData", model);
            else
                return RedirectToAction("PaymentWithPaypal", "PayPal", user);
        }

        //GET
        public async Task<ActionResult> GetExternalUserData()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null) Redirect("~/Error/InternalServerError");

            var vm = new ExternalUserDataViewModel()
            {
                UserName = user.UserName,
                HasPassword = HasPassword(user),
                Password = user.PasswordHash,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> GetExternalUserData(ExternalUserDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user == null) Redirect("~/Error/InternalServerError");

                var isUserNameAlreadyExists = UserManager.Users.Any(x => x.UserName == model.UserName);

                if (isUserNameAlreadyExists)
                {
                    ModelState.AddModelError("UserName", @"This Username is already in use try a different one.");
                    return View(model);
                }

                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                await UserManager.AddPasswordAsync(user.Id, model.Password);

                var result = await UserManager.UpdateAsync(user);

                if (!result.Succeeded) Redirect("~/Error/InternalServerError");

                return RedirectToAction("PaymentWithPaypal", "PayPal", user);
            }

            return RedirectToAction("PricingPlan", "Home", model);
        }

        // POST: /Manage/UploadPhoto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadPhoto(UploadPhotoViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                return Redirect("~/Error/InternalServerError");
            }

            if (model.PhotoCreate != null)
            {
                model.PhotoCreate.SaveAs(Server.MapPath("~/Content/Images/user/" + user.Id + ".jpg"));
                model.Photo = "~/Content/Images/user/" + user.Id + ".jpg";
            }
            else
            {
                Redirect("~/Error/InternalServerError");
            }

            user.PhotoUrl = "/Content/Images/user/" + user.Id + ".jpg";

            var result = await UserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                AddErrors(result);
                Redirect("~/Error/InternalServerError");
            }

            return RedirectToAction("Index");
        }

        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }


        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? (ActionResult)Redirect("~/Error/InternalServerError") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //  POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", @"Failed to verify phone");
            return View(model);
        }

        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }

                    TempData["ShowAlert"] = true;
                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
                }

                AddErrors(result);
            }


            return View(model);
        }

        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Redirect("~/Error/InternalServerError");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword(ApplicationUser user)
        {
            return user.PasswordHash != null;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}