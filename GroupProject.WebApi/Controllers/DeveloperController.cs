using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ExceptionLogger;
using GroupProject.Entities.Domain_Models;
using GroupProject.RepositoryService;

namespace GroupProject.WebApi.Controllers
{
    public class DeveloperController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private ILog iLog;
        public DeveloperController()
        {
            unitOfWork = new UnitOfWork();
            iLog = Log.GetInstance;
        }

        //Get api/Developer
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

        //Get api/Developer/5
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

        // POST: api/Developer
        [ResponseType(typeof(Developer))]
        public async Task<IHttpActionResult> PostDev(Developer developer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.Developer.Create(developer);
            await unitOfWork.SaveAsync();

            return CreatedAtRoute("DefaultApi", new { id = developer.DeveloperId }, new { FirstName = developer.FirstName, LastName = developer.LastName });
        }

        // PUT: api/Developer/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDev(int id, Developer developer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            if (id != developer.DeveloperId) return BadRequest();

            unitOfWork.Developer.Edit(developer);

            var result = 0;

            try
            {
               result = await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!unitOfWork.Developer.DeveloperExists(id))
                {
                    return NotFound();
                }
                else
                {
                    iLog.LogException(ex.Message);
                }
            }

            return result > 0 ? (IHttpActionResult)Ok(new { FirstName = developer.FirstName, LastName = developer.LastName }) : InternalServerError();
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

            var result = 0;
            try
            {
               result = await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!unitOfWork.Developer.DeveloperExists(id))
                {
                    return NotFound();
                }
                else
                {
                    iLog.LogException(ex.Message);
                }
            }

            return result > 0 ? (IHttpActionResult)Ok(new{FirstName = developer.FirstName , LastName = developer.LastName}) : InternalServerError();
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