using System;
using System.Linq;
using System.Threading.Tasks;
using CheckSubscribersJob;
using GroupProject.Database;
using GroupProject.Entities;
using NUnit.Framework;
using Quartz;
using Quartz.Impl;

namespace GroupProject.Tests
{
    [TestFixtureAttribute]
    public class RemoveSubscribeJobTests
    {
        private static ApplicationUser AddUser()
        {
            var user = new ApplicationUser()
            {
                UserName = "Test",
                Email = "Test@test.com",
                FirstName = "Test",
                LastName = "Testing",
                DateOfBirth = DateTime.Now.AddYears(-18),
                RegistrationDate = DateTime.Now,
                IsSubscribed = true,
                SubscribePlan = Plan.Basic,
                SubscriptionDay = DateTime.Now.AddDays(-40),
                ExpireDate = DateTime.Now
            };

            using (var db = new ApplicationDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }

            return user;
        }

        private static void RemoveUser()
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.First(u => u.FirstName == "Test");
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public class JobSchedulerTest
        {
            public static async void Start()
            {

                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();

                await scheduler.Start();

                //Calling the RemoveSubscribeJob Class to execute.
                IJobDetail job = JobBuilder.Create<RemoveSubscribeJob>().Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule
                    (s =>
                        s.WithIntervalInHours(24)
                            .OnEveryDay()
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(DateTime.Now.Hour, DateTime.Now.Minute))
                    )
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
        }


        [Test]
        public void IsRemoveSubscriberJobWorking()
        {
            var user = AddUser();
            var task = Task.Run(JobSchedulerTest.Start);

            if (task.IsCompleted)
            {
                Assert.True(user.IsSubscribed == false && user.SubscriptionDay.Value == null);
            }
            RemoveUser();
        }

    }
}


