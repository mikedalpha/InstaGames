using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GroupProject.Database;
using GroupProject.Entities.Domain_Models;
using GroupProject.RepositoryService;

namespace GroupProject.WebApp.Controllers.WebApi
{
    public class GamesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly IUnitOfWork unitOfWork;

        public GamesController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: api/Games
        public async Task<IHttpActionResult> GetGames()
        {
            var  games = await unitOfWork.Games.GetAllAsync();

            return Ok( games.Select(g=>new
            {
                GameId = g.GameId,
                Title = g.Title,
                Photo=g.Photo,
                Descripition =g.Description,
                Pegi = g.Pegi,
                ReleaseDate =g.ReleaseDate.Year,
                IsRealeased = g.IsReleased,
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
                GameId=game.GameId,
                Title=game.Title,
                Photo = game.Photo,
                Descripition = game.Description,
                Pegi = game.Pegi,
                ReleaseDate = game.ReleaseDate.Year,
                Rating = game.Rating,
                IsRealeased=game.IsReleased,
                Tag=game.Tag.ToString(),
                Categories = new
                {
                    Type = game.GameCategories.Select(c=>c.Type)
                },
                Developer = new
                {
                    Name =game.GameDevelopers.Select(d => d.FirstName +" " + d.LastName)
                }
            });
        }

        // PUT: api/Games/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGame(int id, Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != game.GameId)
            {
                return BadRequest();
            }

            db.Entry(game).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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

            db.Games.Add(game);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = game.GameId }, game);
        }

        // DELETE: api/Games/5
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> DeleteGame(int id)
        {
            Game game = await db.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            db.Games.Remove(game);
            await db.SaveChangesAsync();

            return Ok(game);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameExists(int id)
        {
            return db.Games.Count(e => e.GameId == id) > 0;
        }
    }
}