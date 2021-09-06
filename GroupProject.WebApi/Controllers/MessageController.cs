using System;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ExceptionLogger;
using GroupProject.Entities.Domain_Models;
using GroupProject.RepositoryService;

namespace GroupProject.WebApi.Controllers
{

    [Authorize]
    public class MessageController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private ILog iLog;

        public MessageController()
        {
            unitOfWork = new UnitOfWork();
            iLog = Log.GetInstance;
        }

        // GET: api/Message
        public async Task<IHttpActionResult> GetMessages()
        {
            var messages = await unitOfWork.Message.GetAllAsync();

            return Ok(messages.Select(m => new
            {
                MessageId = m.MessageId,
                SubmitDate = m.SubmitDate,
                Text = m.Text,
                Answered = m.Answered,
                Reply = m.Reply,
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
                Reply = message.Reply,
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
            catch (DbUpdateConcurrencyException ex)
            {
                if (!unitOfWork.Message.MessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    iLog.LogException(ex.Message);
                }
            }

            if (!string.IsNullOrEmpty(message.Reply))
            {
                await Task.Run(() => SendAdminsReply(message));
            }

            return result > 0 ? (IHttpActionResult)StatusCode(HttpStatusCode.NoContent) : InternalServerError();
        }

        // DELETE: api/Message/5
        public async Task<IHttpActionResult> DeleteMessage(int id)
        {
            var message = await unitOfWork.Message.FindByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            unitOfWork.Message.Remove(message);
            var result = await unitOfWork.SaveAsync();

            return result > 0 ? (IHttpActionResult)Ok() : InternalServerError();
        }

        private void SendAdminsReply(Message message)
        {
            var mail = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));

            mail.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString());
            mail.To.Add(message.Creator.Email);
            mail.Subject = "InstaGames Reply to " + message.Creator.UserName;
            mail.Body = message.Reply;

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["Email"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(mail);
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
