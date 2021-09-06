using System.Data;
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
    [Authorize]
    public class CategoryController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private ILog iLog;
        public CategoryController()
        {
            unitOfWork = new UnitOfWork();
            iLog = Log.GetInstance;
        }

        // GET: api/Category
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
                    IsReleased = g.IsReleased,
                    IsEarlyAccess = g.IsEarlyAccess,
                    Rating = g.Rating,
                    Tag = g.Tag.ToString()
                })
            }).ToList());
        }

        public async Task<IHttpActionResult> GetCategory(int id)
        {
            var category = await unitOfWork.Category.FindByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(new
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
                    IsReleased = g.IsReleased,
                    IsEarlyAccess = g.IsEarlyAccess,
                    Rating = g.Rating,
                    Tag = g.Tag.ToString()
                })
            });
        }

        // POST: api/Category
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> PostCategory(Category category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            unitOfWork.Category.Create(category);
            await unitOfWork.SaveAsync();

            return CreatedAtRoute("DefaultApi", new { id = category.CategoryId }, new { Type = category.Type });
        }

        // PUT: api/Category/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            if (id != category.CategoryId) return BadRequest();

            unitOfWork.Category.Edit(category);

            var result = 0;
            try
            {
                result = await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!unitOfWork.Category.CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    iLog.LogException(ex.Message);
                }
            }

            return result > 0 ? (IHttpActionResult)Ok(new { Type = category.Type }) : InternalServerError();
        }

        //DELETE: api/Category/4
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> DeleteCategory(int id)
        {
            var category = await unitOfWork.Category.FindByIdAsync(id);

            if (category is null) return NotFound();

            unitOfWork.Category.Remove(category);
            
            var result = 0;
            try
            {
                result = await unitOfWork.SaveAsync();
            }
            catch (DBConcurrencyException ex)
            {
                if (!unitOfWork.Category.CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    iLog.LogException(ex.Message);
                }
            }

            return result > 0 ? (IHttpActionResult)Ok(new { Type = category.Type }) : InternalServerError();
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