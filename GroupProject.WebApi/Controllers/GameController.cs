using System;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using ExceptionLogger;
using GroupProject.Entities.Domain_Models;
using GroupProject.RepositoryService;

namespace GroupProject.WebApi.Controllers
{

    [AllowAnonymous]
    public class GameController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private ILog iLog;

        public GameController()
        {
            unitOfWork = new UnitOfWork();
            iLog = Log.GetInstance;
        }

        // GET: api/Games
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
                }).ToList(),
                Developers = g.GameDevelopers.Select(d => new
                {
                    Name = $"{d.FirstName + " " + d.LastName}"
                }).ToList()
            }).ToList());
        }

        // GET: api/Games/5
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> GetGame(int id)
        {
            var game = await unitOfWork.Games.FindByIdAsync(id);

            if (game is null) return NotFound();

            return Ok(new
            {
                GameId = game.GameId,
                Title = game.Title,
                Photo = game.Photo,
                Trailer = game.Trailer,
                GameUrl = game.GameUrl,
                Description = game.Description,
                Pegi = game.Pegi.PegiPhoto,
                PegiId = game.Pegi.PegiId,
                ReleaseDate = game.ReleaseDate.ToString("yyyy-MM-dd"),
                Rating = game.Rating,
                IsReleased = game.IsReleased,
                IsEarlyAccess = game.IsEarlyAccess,
                Tag = game.Tag.ToString(),
                SelectedCategories = game.GameCategories.Select(c => c.CategoryId).ToList(),
                Categories = game.GameCategories.Select(c => new
                {
                    Type = c.Type
                }).ToList(),
                SelectedDevelopers = game.GameDevelopers.Select(c => c.DeveloperId).ToList(),
                Developers = game.GameDevelopers.Select(d => new
                {
                    Name = $"{d.FirstName + " " + d.LastName}"
                }).ToList()
            });
        }

        // PUT: api/Games/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGame(int id, Game postedGame)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != postedGame.GameId) return BadRequest();

            var catIds = postedGame.GameCategories.Select(c => c.CategoryId).ToArray();
            var devIds = postedGame.GameDevelopers.Select(d => d.DeveloperId).ToArray();
            var pegiId = postedGame.Pegi.PegiId;


             var game =   await unitOfWork.Games.FindByIdAsync(id);
             game.Title = postedGame.Title;
             game.Description = postedGame.Description;
             game.ReleaseDate = postedGame.ReleaseDate;
             game.Photo = postedGame.Photo;
             game.Trailer = postedGame.Trailer;
             game.IsEarlyAccess = postedGame.IsEarlyAccess;
             game.IsReleased = postedGame.IsReleased;
             game.Tag = postedGame.Tag;
             game.GameUrl = postedGame.GameUrl;
             
            unitOfWork.Games.LoadCategories(game);
            unitOfWork.Games.AssignGameCategories(game, catIds);

            unitOfWork.Games.LoadDevelopers(game);
            unitOfWork.Games.AssignGameDevelopers(game, devIds);

            unitOfWork.Games.Attach(game);
            unitOfWork.Games.AssignGamePegi(game, pegiId);
           

            unitOfWork.Games.Edit(game);

            try
            {
                await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!unitOfWork.Games.GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    iLog.LogException(ex.Message);
                }
            }

            return Ok(new { Title = game.Title });
        }

        // POST: api/Games
        [HttpPost]
        public IHttpActionResult PostGame(Game game)
        {
            var catIds = game.GameCategories.Select(c => c.CategoryId).ToArray();
            var devIds = game.GameDevelopers.Select(d => d.DeveloperId).ToArray();

            unitOfWork.Games.AssignGamePegi(game, game.Pegi.PegiId);
            unitOfWork.Games.AssignGameCategories(game, catIds);
            unitOfWork.Games.AssignGameDevelopers(game, devIds);

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

            var result = 0;
            try
            {
               result = await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!unitOfWork.Games.GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    iLog.LogException(ex.Message);
                }
            }

            return result > 0 ? (IHttpActionResult)Ok(new { Title = game.Title }) : InternalServerError();
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
                catch (Exception ex)
                {
                    iLog.LogException(ex.Message);
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