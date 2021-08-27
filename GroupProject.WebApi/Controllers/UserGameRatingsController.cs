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

        //GET: api/UserGameRatings
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetRatings()
        {
            var userGameRatings = await _unitOfWork.UserGameRatings.GetAllAsync();

            return Ok(userGameRatings.Select(ugr => new
            {
                UserGameRatingsId = ugr.UserGameRatingsId,
                UserId = ugr.ApplicationUser.Id,
                UserName = ugr.ApplicationUser.UserName,
                GameId = ugr.Game.GameId,
                GameTitle = ugr.Game.Title,
                GamePhoto = ugr.Game.Photo,
                TotalRating = ugr.Game.Rating.ToString("0.00"),
                Rating = ugr.Rating
            }).ToList());
        }

        //POST: api/UserGameRatings/5&5&5
        [HttpPost]
        public async Task<IHttpActionResult> AddRating(string userId, int gameId, int rating)
        {
            var game = await _unitOfWork.Games.FindByIdAsync(gameId);
            if (game == null) return NotFound();

            var user = await UserManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var userGameRatings = new UserGameRatings();

            userGameRatings.ApplicationUserId = userId;
            userGameRatings.GameId = gameId;
            userGameRatings.Rating = rating;

            _unitOfWork.UserGameRatings.Create(userGameRatings);

            await _unitOfWork.SaveAsync();

            //var userRating =
            //    (await _unitOfWork.UserGameRatings.GetAllAsync()).FirstOrDefault(g =>
            //        g.GameId == gameId && g.ApplicationUserId == userId);

            return Ok(new
            {
                Rating = rating
            });
        }
    }
}
