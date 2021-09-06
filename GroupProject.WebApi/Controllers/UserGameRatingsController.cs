using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GroupProject.Database;
using GroupProject.Entities.Domain_Models;
using GroupProject.RepositoryService;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace GroupProject.WebApi.Controllers
{
    [Authorize]
    public class UserGameRatingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext context;

        public UserGameRatingsController()
        {
            _unitOfWork = new UnitOfWork();
            context = new ApplicationDbContext();
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
                g.GameId,
                GameTitle = g.Title,
                GamePhoto = g.Photo,
                TotalRating = g.Rating.ToString("0.0"),
                TotalRatingFloat = g.Rating,
                g.ReleaseDate,
                g.IsReleased,
                Subscribers = g.Subscribers.Select(s => new
                {
                    s.FirstName,
                    s.LastName,
                    s.UserName
                }),
                UserGameRatings = g.UserGameRatings.Select(ugr => new
                {
                    ugr.UserGameRatingsId,
                    UserId = ugr.ApplicationUser.Id,
                    ugr.ApplicationUser.UserName,
                    ugr.Rating
                }),
                GameCategories = g.GameCategories.Select(c => new
                {
                    c.Type

                })
            }).ToList().OrderBy(x => x.TotalRatingFloat));
        }

        //GET: api/UserGameRatings/5
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetRatingDetails(int id)
        {
            var game = await _unitOfWork.Games.FindByIdAsync(id);

            if (game == null) return NotFound();

            return Ok(new
            {
                game.GameId,
                GameTitle = game.Title,
                GamePhoto = game.Photo,
                GameDescription = game.Description,
                TotalRating = game.Rating.ToString("0.0"),
                TotalRatingFloat = game.Rating,
                UserGameRatings = game.UserGameRatings.Select(ugr => new
                {
                    ugr.UserGameRatingsId,
                    UserId = ugr.ApplicationUser.Id,
                    ugr.ApplicationUser.UserName,
                    ugr.Rating
                }).ToList()
            });
        }

        //POST: api/UserGameRatings/5&5&5
        [HttpPost]
        public async Task<IHttpActionResult> AddRating(UserGameRatings ratedGame)
        {
            var game = await context.Games.FindAsync(ratedGame.Game.GameId);

            if (game == null) return NotFound();

            var user = context.Users.Find(ratedGame.ApplicationUser.Id);
            if (user == null) return NotFound();

            var userGameRatings = new UserGameRatings
            {
                ApplicationUser = user,
                Game = game,
                Rating = ratedGame.Rating
            };

            context.Entry(userGameRatings).State = EntityState.Added;

            var result = await context.SaveChangesAsync();

            return result > 0
                ? (IHttpActionResult) Ok(new
                {
                    ratedGame.Rating
                })
                : InternalServerError();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                context.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }
    }
}
