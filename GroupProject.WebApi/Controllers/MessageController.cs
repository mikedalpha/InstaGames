using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
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
                Answered = m.Answered,
                Creator = new
                {
                    FirstName = m.Creator.FirstName,
                    LastName = m.Creator.LastName,
                    UserName = m.Creator.UserName,
                    Email = m.Creator.Email,
                    Photo = m.Creator.PhotoUrl
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

            return Ok(new
            {
                MessageId = message.MessageId,
                SubmitDate = message.SubmitDate,
                Text = message.Text,
                Answered = message.Answered,
                Creator = new
                {
                    FirstName = message.Creator.FirstName,
                    LastName = message.Creator.LastName,
                    UserName = message.Creator.UserName,
                    Email = message.Creator.Email,
                    Photo = message.Creator.PhotoUrl
                }
            });
        }

        // PUT: api/Games/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Put(int id, Message message)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != message.MessageId) return BadRequest();

            unitOfWork.Message.Edit(message);

            var result = 0;

            try
            {
                result = await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!unitOfWork.Message.MessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return result > 0 ? (IHttpActionResult)StatusCode(HttpStatusCode.NoContent) : InternalServerError();
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
