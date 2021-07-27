using System;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Database;
using GroupProject.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Quartz;
using Quartz.Impl;

namespace GroupProject.WebApp
{
    public class CheckSubscribersJob : IJob
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

    public class JobSheduler
    {
        public static async void Start()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<CheckSubscribersJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                (s =>
                    s.WithIntervalInHours(24)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(23, 04))
                )
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }


}