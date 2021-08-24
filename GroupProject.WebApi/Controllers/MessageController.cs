using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GroupProject.Entities.Domain_Models;
using GroupProject.RepositoryService;

namespace GroupProject.WebApi.Controllers
{
    public class MessageController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public MessageController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: api/Games
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetMessages()
        {
            var messages = await unitOfWork.Message.GetAllAsync();

            return Ok(messages.Select(m => new
            {
                MessageId = m.MessageId,
                SubmitDate = m.SubmitDate,
                Text = m.Text,
                Creator = new
                {
                  FirstName =  m.Creator.FirstName,
                  LastName =  m.Creator.LastName,
                  UserName =  m.Creator.UserName,
                  Email = m.Creator.Email,
                }
            }).ToList());
        }

        // GET: api/Message/5
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> GetMessage(int id)
        {
            var message = await unitOfWork.Message.FindByIdAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return Ok( new
            {
                MessageId = message.MessageId,
                SubmitDate = message.SubmitDate,
                Text = message.Text,
                Creator = new
                {
                    FirstName = message.Creator.FirstName,
                    LastName = message.Creator.LastName,
                    UserName = message.Creator.UserName,
                    Email = message.Creator.Email,
                }
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
