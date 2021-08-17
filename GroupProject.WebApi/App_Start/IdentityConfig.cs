using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using GroupProject.Entities;
using GroupProject.Database;

namespace GroupProject.WebApi
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        //Attaching UserGameList to user 
        //This method is used for add to MyList functionality
        public async Task AttachUserList(string userid , int id)
        {
            using (ApplicationDbContext db = ApplicationDbContext.Create())
            {
                var game = await db.Games.FindAsync(id);
                if (game == null) return;
                var user = await db.Users.FirstAsync(u => u.Id == userid);
                if (userid != user.Id) return;

                db.Users.Attach(user);
                db.Entry(user).Collection("UserList").Load();

                if (user.UserList.Contains(game))
                {
                    user.UserList.Remove(game);
                    db.Entry(user).State = EntityState.Modified;
                }
                else
                {
                    user.UserList.Add(game);
                    db.Entry(user).State = EntityState.Modified;
                }
                
                await db.SaveChangesAsync();
            }
        }

        public async Task RemoveFromUserList(string userid, int id)
        {
            using (ApplicationDbContext db = ApplicationDbContext.Create())
            {

                var game = await db.Games.FindAsync(id);
                if (game == null) return;
                var user = await db.Users.FirstAsync(u => u.Id == userid);
                if (userid != user.Id) return;

                db.Users.Attach(user);
                db.Entry(user).Collection("UserList").Load();

                user.UserList.Remove(game);
                db.Entry(user).State = EntityState.Deleted;

                await db.SaveChangesAsync();
            }
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
}
