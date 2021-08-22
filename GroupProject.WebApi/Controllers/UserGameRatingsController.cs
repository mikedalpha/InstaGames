using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GroupProject.Entities.Domain_Models;
using GroupProject.RepositoryService;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace GroupProject.WebApi.Controllers
{
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
            _unitOfWork = new UnitOfWork();
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

        //POST: api/UserGameRatings/5&5&5
        [HttpPost]
        public async Task<IHttpActionResult> AddRating(string userId, int gameId, int rating)
        {
            var game = await _unitOfWork.Games.FindByIdAsync(gameId);
            if (game == null) return NotFound();

            var user = await UserManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var userRating =
                (await _unitOfWork.UserGameRatings.GetAllAsync()).FirstOrDefault(g =>
                    g.GameId == gameId && g.ApplicationUserId == userId);

            if (userRating == null)
            {
                var userGameRatings = new UserGameRatings();

                userGameRatings.ApplicationUserId = userId;
                userGameRatings.GameId = gameId;
                userGameRatings.Rating = rating;

                _unitOfWork.UserGameRatings.Create(userGameRatings);
            }
            else
            {
                userRating.Rating = rating;
                _unitOfWork.UserGameRatings.Edit(userRating);
            }

            await _unitOfWork.SaveAsync();

            return Ok();
        }
    }
}
