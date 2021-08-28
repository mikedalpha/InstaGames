using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using GroupProject.Entities.Domain_Models;
using GroupProject.RepositoryService;

namespace GroupProject.WebApi.Controllers
{
    public class GameController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public GameController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: api/Games
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetGames()
        {
            var games = await unitOfWork.Games.GetAllAsync();

            return Ok(games.Select(g => new
            {
                GameId = g.GameId,
                Title = g.Title,
                Photo = g.Photo,
                Trailer = g.Trailer,
                GameUrl = g.GameUrl,
                Description = g.Description,
                Pegi = g.Pegi.PegiPhoto,
                ReleaseDate = g.ReleaseDate,
                IsReleased = g.IsReleased,
                IsEarlyAccess = g.IsEarlyAccess,
                Rating = g.Rating,
                Tag = g.Tag.ToString(),
                Categories = g.GameCategories.Select(c => new
                {
                    Type = c.Type
                }),
                Developers = g.GameDevelopers.Select(d => new
                {
                    Name = $"{d.FirstName + " " + d.LastName}"
                })
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
                Trailer = game.Trailer,
                GameUrl = game.GameUrl,
                Description = game.Description,
                Pegi = game.Pegi.PegiPhoto,
                ReleaseDate = game.ReleaseDate,
                Rating = game.Rating,
                IsReleased = game.IsReleased,
                IsEarlyAccess = game.IsEarlyAccess,
                Tag = game.Tag.ToString(),
                Categories = game.GameCategories.Select(c => new
                {
                    Type = c.Type
                }),
                Developers = game.GameDevelopers.Select(d => new
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
        [HttpPost]
        public IHttpActionResult PostGame(Game game)
        {

            unitOfWork.Games.AssignGamePegi(game, game.Pegi.PegiId);
            unitOfWork.Games.AssignGameCategories(game, game.GameCategories.ToArray());
            unitOfWork.Games.AssignGameDevelopers(game, game.GameDevelopers.ToArray());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(game.Trailer))
            {
                game.Trailer = null;
            }

            unitOfWork.Games.Create(game);
            unitOfWork.Save();

            return CreatedAtRoute("DefaultApi", new { id = game.GameId }, new { Title = game.Title });
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

            await Task.Run(() => DeleteFile(game.Photo));

            if (game.Trailer != null)
            {
                await Task.Run(() => DeleteFile(game.Trailer));
            }

            unitOfWork.Games.Remove(game);
            var result = await unitOfWork.SaveAsync();

            return result > 0 ? (IHttpActionResult)Ok() : InternalServerError();
        }


        [HttpPost]
        [Route("api/game/UploadGameFiles")]
        public IHttpActionResult UploadGameFiles()
        {
            var httpRequest = HttpContext.Current.Request;
            //Upload Image
            var postedFile = httpRequest.Files["file"];
            //Create custom filename
            if (postedFile != null)
            {
                var file = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).ToArray()).Replace(" ", "-");
                file = file + Path.GetExtension(postedFile.FileName);
                var filePath = HttpContext.Current.Server.MapPath(postedFile.FileName);
                postedFile.SaveAs(filePath);
                return Ok("https://localhost:44369/" + postedFile.FileName.Replace("~", ""));
            }

            return InternalServerError();
        }


        public void DeleteFile(string fileToDelete)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory + fileToDelete;
            var fileInfo = new FileInfo(directory);
            if (fileInfo.Exists)
            {
                try
                {
                    fileInfo.Delete();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
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