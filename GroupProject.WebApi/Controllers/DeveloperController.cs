using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GroupProject.Entities.Domain_Models;
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
                IsInstaGamesDev = d.IsInstaGamesDev,
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
                    Tag=dg.Tag
                })

            }).ToList());
        }

        public async Task<IHttpActionResult> GetDev(int? id)
        {
            var developer = await unitOfWork.Developer.FindByIdAsync(id);

            if (developer is null) return NotFound();

            return Ok( new
            {
                DeveloperId = developer.DeveloperId,
                FirstName = developer.FirstName,
                LastName = developer.LastName,
                IsInstaGamesDev = developer.IsInstaGamesDev,
                DeveloperGames = developer.DeveloperGames.Select(dg => new
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
                    Tag = dg.Tag
                })

            });
        }

        // DELETE: api/Developer/5
        [ResponseType(typeof(Developer))]
        public async Task<IHttpActionResult> DeleteDev(int id)
        {
            var developer = await unitOfWork.Developer.FindByIdAsync(id);
            if (developer == null)
            {
                return NotFound();
            }

            unitOfWork.Developer.Remove(developer);
            var result = await unitOfWork.SaveAsync();

            return result > 0 ? (IHttpActionResult)Ok() : InternalServerError();
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
