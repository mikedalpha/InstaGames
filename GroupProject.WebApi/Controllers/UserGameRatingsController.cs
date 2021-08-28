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
            var games = await _unitOfWork.Games.GetAllAsync();

            return Ok(games.Select(g => new
            {
                GameId = g.GameId,
                GameTitle = g.Title,
                GamePhoto = g.Photo,
                TotalRating = g.Rating.ToString("0.0"),
                TotalRatingFloat = g.Rating,
                Subscribers = g.Subscribers.Select(s=>new 
                {
                    FirstName = s.FirstName,
                    LastName=s.LastName,
                    UserName = s.UserName
                }),
                UserGameRatings = g.UserGameRatings.Select(ugr =>new {
                    UserGameRatingsId = ugr.UserGameRatingsId,
                    UserId = ugr.ApplicationUser.Id,
                    UserName = ugr.ApplicationUser.UserName,
                    Rating = ugr.Rating
                })
            }).ToList());
        }

        //GET: api/UserGameRatings/5
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetRatingDetails(int id)
        {
            var game = await _unitOfWork.Games.FindByIdAsync(id);

            if (game == null) return NotFound();

            return Ok(new
            {
                GameId = game.GameId,
                GameTitle = game.Title,
                GamePhoto = game.Photo,
                GameDescription = game.Description,
                TotalRating = game.Rating.ToString("0.0"),
                TotalRatingFloat = game.Rating,
                UserGameRatings = game.UserGameRatings.Select(ugr => new
                {
                    UserGameRatingsId = ugr.UserGameRatingsId,
                    UserId = ugr.ApplicationUser.Id,
                    UserName = ugr.ApplicationUser.UserName,
                    Rating = ugr.Rating
                }).ToList()
            });
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

            var userRating =
                (await _unitOfWork.UserGameRatings.GetAllAsync()).FirstOrDefault(g =>
                    g.GameId == gameId && g.ApplicationUserId == userId);

            return Ok(new
            {
                Rating = rating
            });
        }
    }
}
