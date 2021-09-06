using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GroupProject.RepositoryService;

namespace GroupProject.WebApi.Controllers
{
    [AllowAnonymous]
    public class PegiController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        public PegiController()
        {
            unitOfWork = new UnitOfWork();
        }
        public async Task<IHttpActionResult> GetPegi()
        {
            var pegi = await unitOfWork.Pegi.GetAllAsync();

            return Ok(pegi.Select(p => new
            {
                PegiId = p.PegiId,
                PegiPhoto = p.PegiPhoto,
                PegiAge = p.PegiAge
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
