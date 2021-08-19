using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using GroupProject.Database;
using GroupProject.Entities.Domain_Models;
using GroupProject.RepositoryService;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace GroupProject.WebApi.Controllers
{
    [EnableCors(origins: "https://localhost:44384", headers: "*", methods: "*")]
    public class UserGameRatingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationUserManager _userManager;

        public UserGameRatingsController()
        {
            _unitOfWork = new UnitOfWork();
        }

        public UserGameRatingsController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // POST: api/UserGameRatings/5&5&5
        public async Task<ActionResult> AddRating(string userId, int gameId, int rating)
        {
            using (ApplicationDbContext db = ApplicationDbContext.Create())
            {
                var userRating =
                    (await _unitOfWork.UserGameRatings.GetAllAsync()).FirstOrDefault(g =>
                        g.GameId == gameId && g.ApplicationUserId == userId);

                if (userRating == null) //add, else if rating != null tote edit, kai telos save changes
                {
                    //_unitOfWork.UserGameRatings.Create();
                }
                else
                {
                    //_unitOfWork.UserGameRatings.Edit();
                }

                await _unitOfWork.SaveAsync();

                return Ok(UserGameRatings);
            }
        }
    }
}
