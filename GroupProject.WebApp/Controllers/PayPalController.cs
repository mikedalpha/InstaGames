using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ExceptionLogger;
using GroupProject.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PayPal.Api;
using Plan = GroupProject.Entities.Plan;

namespace GroupProject.WebApp.Controllers
{
    public class PayPalController : Controller
    {
        private Payment payment;
        private ILog iLog;
        private ApplicationUserManager _userManager;

        public PayPalController() { }

        public PayPalController(ApplicationUserManager userManager)
        {
            iLog = Log.GetInstance;
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            iLog.LogException(filterContext.Exception.ToString());
            filterContext.ExceptionHandled = true;
            this.Redirect("~/Error/InternalServerError").ExecuteResult(this.ControllerContext);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl, ApplicationUser user)
        {
            Item item = new Item()
            {
                name = user.SubscribePlan.ToString(),
                currency = "EUR",
                price = user.SubscribePlan == Plan.Basic ? "19" : "39",
                quantity = "1",
                sku = "sku"

            };

            var payer = new Payer() { payment_method = "paypal" };

            var redirectUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            var details = new Details()
            {
                tax = "2",
                subtotal = item.price
            };

            var amount = new Amount()
            {
                currency = "EUR",
                total = (Convert.ToInt32(details.tax) + Convert.ToInt32(item.price)).ToString(),
                details = details
            };

            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = "InstaGames Testing Transaction System",
                invoice_number = Convert.ToString(new Random().Next(1000000)),
                amount = amount
            });

            var payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirectUrls
            };

            return payment.Create(apiContext);
        }

        //Execute Payment method
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
        }


        //Create Payment With PayPal
        public async Task<ActionResult> PaymentWithPaypal(ApplicationUser user)
        {
            APIContext apiContext = PayPalConfiguration.GetApiContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/PayPal/PaymentWithPaypal?";
                    var guid = Convert.ToString(new Random().Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid, user);

                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;

                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executePayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executePayment.state.ToLower() != "approved")
                    {
                        return Redirect("~/Error/InternalServerError");
                    }
                }
            }
            catch (Exception e)
            {
                iLog.LogException(e.Message);
                return Redirect("~/Error/InternalServerError");
            }

            //IF Payment Successful

            user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            user.IsSubscribed = true;

            if (user.SubscriptionDay is null)
            {
                user.SubscriptionDay = DateTime.Now;
            }

            if (user.ExpireDate is null || user.ExpireDate < DateTime.Now)
            {
                user.ExpireDate = DateTime.Now.AddDays(user.SubscribePlan == Plan.Basic ? 30 : 90);
            }
            else
            {
                user.ExpireDate = user.ExpireDate.Value.AddDays(user.SubscribePlan == Plan.Basic ? 30 : 90);
            }

            var addRole = await UserManager.AddToRoleAsync(user.Id, "Subscriber");
            var removeRole = await UserManager.RemoveFromRoleAsync(user.Id, "Unsubscribed");
            if (addRole.Succeeded && removeRole.Succeeded)
            {
                await UserManager.UpdateAsync(user);
            }

            return View("PaymentSuccess");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager !=null)
            {
                _userManager.Dispose();
                _userManager = null;
            }
            base.Dispose(disposing);
        }
    }
}