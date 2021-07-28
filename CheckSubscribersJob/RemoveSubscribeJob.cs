using System;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Database;
using GroupProject.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Quartz;


namespace CheckSubscribersJob
{
    [DisallowConcurrentExecution]
    public class RemoveSubscribeJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            using (var db = new ApplicationDbContext())
            {
                var users = db.Users.ToList();

                foreach (var user in users.Where(user => user.ExpireDate != null && DateTime.Now > user.ExpireDate.Value))
                {
                    user.IsSubscribed = false;
                    user.SubscriptionDay = null;
                    var store = new UserStore<ApplicationUser>(db);
                    var manager = new UserManager<ApplicationUser>(store);
                    manager.RemoveFromRole(user.Id, "Subscriber");
                    manager.Update(user);
                    db.SaveChanges();
                }
            }

            return Task.CompletedTask;
        }
    }
}
