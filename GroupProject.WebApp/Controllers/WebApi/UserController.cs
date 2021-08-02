using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using GroupProject.Database;
using GroupProject.Entities;
using Microsoft.AspNet.Identity.Owin;

namespace GroupProject.WebApp.Controllers.WebApi
{
    public class UserController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public UserController()
        {

        }
        
        public UserController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        // GET: api/Users
        public async Task<IHttpActionResult> GetUsers()
        {
            var users = await UserManager.Users.ToListAsync();

            return Ok(users.Select(u => new
            {
                Id = u.Id,
                UserName = u.UserName,
                PasswordHash = u.PasswordHash,
                Email = u.Email,
                DateOfBirth = u.DateOfBirth,
                RegistrationDate = u.RegistrationDate,
                FirstName=u.FirstName,
                LastName=u.LastName,
                IsSubscribed = u.IsSubscribed,
               SecurityStamp = u.SecurityStamp
            }).ToList());
        }

        // PUT: api/Users/5
        public async Task<IHttpActionResult> PutUsers(string id, ApplicationUser user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            if (id != user.Id) return BadRequest();

            db.Entry(user).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { UserName = user.UserName });
        }

        // PUT: api/Users/5&5
        public async Task<IHttpActionResult> PutUsers(string id, ApplicationUser user, int gameId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            if (id != user.Id) return BadRequest();

            var game = await db.Games.FindAsync(gameId);
            if (game == null) return BadRequest();

            db.Users.Attach(user);
            db.Entry(user).Collection("UserList").Load();
            user.UserList.Add(game);

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { UserName = user.UserName });
        }


        // POST: api/Users
        [ResponseType(typeof(ApplicationUser))]
        public async Task<IHttpActionResult> PostGame(ApplicationUser user , string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await UserManager.CreateAsync(user,password);

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, new{UserName = user.UserName});
        }

        // DELETE: api/User/5
        [ResponseType(typeof(ApplicationUser))]
        public async Task<IHttpActionResult> DeleteGame(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user == null) return NotFound();


           var result = await UserManager.DeleteAsync(user);

           return result.Succeeded ? (IHttpActionResult) Ok(new {UserName = user.UserName}) : BadRequest();
        }


        private bool UserExists(string id)
        {
            return UserManager.FindByIdAsync(id).IsCompleted;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
