using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using GroupProject.RepositoryService;

namespace GroupProject.WebApi.Controllers
{
    [EnableCors(origins: "https://localhost:44384", headers: "*", methods: "*")]
    public class CategoryController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: api/Games
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetCategories()
        {
            var categories = await unitOfWork.Category.GetAllAsync();

            return Ok(categories.Select(c => new
            {
                CategoryId = c.CategoryId,
                Type = c.Type,
                Description = c.Description,
                Games = c.CategoryGames.Select(g => new
                {
                    GameId = g.GameId,
                    Title = g.Title,
                    Photo = g.Photo,
                    Trailer = g.Trailer,
                    Description = g.Description,
                    Pegi = g.Pegi.PegiPhoto,
                    ReleaseDate = g.ReleaseDate.Year,
                    IsRealeased = g.IsReleased,
                    IsEarlyAccess = g.IsEarlyAccess,
                    Rating = g.Rating,
                    Tag = g.Tag.ToString()
                }),
            }).ToList());
        }

        public async Task<IHttpActionResult> GetCategories(int id)
        {
            var category = await unitOfWork.Category.FindByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(  new
            {
                CategoryId = category.CategoryId,
                Type = category.Type,
                Description = category.Description,
                Games = category.CategoryGames.Select(g => new
                {
                    GameId = g.GameId,
                    Title = g.Title,
                    Photo = g.Photo,
                    Trailer = g.Trailer,
                    Description = g.Description,
                    Pegi = g.Pegi.PegiPhoto,
                    ReleaseDate = g.ReleaseDate.Year,
                    IsRealeased = g.IsReleased,
                    IsEarlyAccess = g.IsEarlyAccess,
                    Rating = g.Rating,
                    Tag = g.Tag.ToString()
                })
            });
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
