using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using GroupProject.Entities.Domain_Models;
using GroupProject.RepositoryService;

namespace GroupProject.WebApi.Controllers
{
    [EnableCors(origins: "https://localhost:44384", headers:"*",methods:"*")]
    public class GameController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public GameController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: api/Games
        [OverrideAuthentication]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetGames()
        {
            var games = await unitOfWork.Games.GetAllAsync();

            return Ok(games.Select(g => new
            {
                GameId = g.GameId,
                Title = g.Title,
                Photo = g.Photo,
                Description = g.Description,
                Pegi = g.Pegi.PegiPhoto,
                ReleaseDate = g.ReleaseDate.Year,
                IsRealeased = g.IsReleased,
                IsEarlyAccess = g.IsEarlyAccess,
                Rating = g.Rating,
                Tag = g.Tag.ToString()
            }).ToList());
        }

        // GET: api/Games/5
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> GetGame(int id)
        {
            var game = await unitOfWork.Games.FindByIdAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                GameId = game.GameId,
                Title = game.Title,
                Photo = game.Photo,
                Description = game.Description,
                Pegi = game.Pegi.PegiPhoto,
                ReleaseDate = game.ReleaseDate.Year,
                Rating = game.Rating,
                IsRealeased = game.IsReleased,
                IsEarlyAccess = game.IsEarlyAccess,
                Tag = game.Tag.ToString(),
                Categories = game.GameCategories.Select(c => new
                {
                    Type = c.Type
                }),
                Developer = game.GameDevelopers.Select(d => new
                {
                    Name = $"{d.FirstName + " " + d.LastName}"
                })
            });
        }

        // PUT: api/Games/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGame(int id, Game game)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            if (id != game.GameId) return BadRequest();

              unitOfWork.Games.Edit(game);

            try
            {
                await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!unitOfWork.Games.GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Games
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> PostGame(Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.Games.Create(game);
            await unitOfWork.SaveAsync();

            return CreatedAtRoute("DefaultApi", new { id = game.GameId }, game);
        }

        // DELETE: api/Games/5
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> DeleteGame(int id)
        {
            var game = await unitOfWork.Games.FindByIdAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            unitOfWork.Games.Remove(game);
            await unitOfWork.SaveAsync();

            return Ok(game);
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