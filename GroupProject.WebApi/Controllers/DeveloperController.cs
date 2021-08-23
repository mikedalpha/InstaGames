using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GroupProject.RepositoryService;

namespace GroupProject.WebApi.Controllers
{
    public class DeveloperController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        public DeveloperController()
        {
            unitOfWork = new UnitOfWork();
        }

        public async Task<IHttpActionResult> GetDevs()
        {
            var developers = await unitOfWork.Developer.GetAllAsync();

            return Ok(developers.Select(d => new
            {
                DeveloperId= d.DeveloperId,
                FirstName = d.FirstName,
                LastName = d.LastName,
                PegiAge = d.IsInstaGamesDev,
                DeveloperGames = d.DeveloperGames.Select(dg=>new
                {
                    GameId = dg.GameId,
                    Title = dg.Title,
                    Photo = dg.Photo,
                    Trailer = dg.Trailer,
                    Description = dg.Description,
                    Pegi = dg.Pegi.PegiPhoto,
                    ReleaseDate = dg.ReleaseDate,
                    IsReleased = dg.IsReleased,
                    IsEarlyAccess = dg.IsEarlyAccess,
                    Rating = dg.Rating,
                })

            }).ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
