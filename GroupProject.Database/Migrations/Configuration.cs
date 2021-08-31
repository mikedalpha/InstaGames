using System.Collections.Generic;
using GroupProject.Entities;
using GroupProject.Entities.Domain_Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GroupProject.Database.Migrations
{
    using System;
    using System.Collections.ObjectModel;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            #region Adding Roles

            if (!context.Roles.Any(x => x.Name == "Admin"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }

            if (!context.Roles.Any(x => x.Name == "Subscriber"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = "Subscriber" };
                manager.Create(role);
            }

            if (!context.Roles.Any(x => x.Name == "Unsubscribed"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = "Unsubscribed" };
                manager.Create(role);
            }

            #endregion

            #region Adding Users 

            var passwordHash = new PasswordHasher();
            var store = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(store);

            var user1 = new ApplicationUser()
            {
                UserName = "GameMaster",
                Email = "admin@instagames.com",
                PasswordHash = passwordHash.HashPassword("Admin1"),
                RegistrationDate = DateTime.Now,
                FirstName = "Super",
                LastName = "Gamer",
                PhotoUrl = "/Content/images/user/Admin.png",
                DateOfBirth = new DateTime(1990, 10, 28),
                IsSubscribed = true,
                EmailConfirmed = true,
                Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now, Text = "Hello fellow admins I am testing the Message section!" } }
            };

            if (!context.Users.Any(x => x.UserName == user1.UserName))
            {
                userManager.Create(user1);
                userManager.AddToRole(user1.Id, "Admin");
            }

            var user2 = new ApplicationUser()
            {
                UserName = "mikedalpha",
                Email = "mikedalpha@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = DateTime.Now,
                FirstName = "Michael",
                LastName = "Athanasoglou",
                PhotoUrl = "/Content/images/user/Michael.jpg",
                DateOfBirth = new DateTime(1991, 2, 22),
                IsSubscribed = true,
                EmailConfirmed = true,
                Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now, Text = "Hello fellow admins I am Mike and I am testing the Message section!" } }
            };

            if (!context.Users.Any(x => x.UserName == user2.UserName))
            {
                userManager.Create(user2);
                userManager.AddToRole(user2.Id, "Admin");
            }

            var user3 = new ApplicationUser()
            {
                UserName = "kwstaskor",
                Email = "kwstaskor@hotmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = DateTime.Now,
                FirstName = "Kostas",
                LastName = "Korliaftis",
                PhotoUrl = "/Content/images/user/Kostas.jpg",
                DateOfBirth = new DateTime(1991, 2, 28),
                IsSubscribed = true,
                EmailConfirmed = true,
                Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now, Text = "Hello fellow admins I am Kostas and I am testing the Message section!" } }
            };

            if (!context.Users.Any(x => x.UserName == user3.UserName))
            {
                userManager.Create(user3);
                userManager.AddToRole(user3.Id, "Admin");
            }

            var user4 = new ApplicationUser()
            {
                UserName = "konstantinala",
                Email = "klakoumenta@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = DateTime.Now,
                FirstName = "Konstantina",
                LastName = "Lakoumenta",
                PhotoUrl = "/Content/images/user/Konstantina.jpg",
                DateOfBirth = new DateTime(1998, 9, 8),
                IsSubscribed = true,
                EmailConfirmed = true,
                Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now, Text = "Hello fellow admins I am Kostantina and I am testing the Message section!" } }
            };

            if (!context.Users.Any(x => x.UserName == user4.UserName))
            {
                userManager.Create(user4);
                userManager.AddToRole(user4.Id, "Admin");
            }

            var user5 = new ApplicationUser()
            {
                UserName = "LittlePlump",
                Email = "littlePlump@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 7, 24),
                FirstName = "Nikos",
                LastName = "Korobos",
                DateOfBirth = new DateTime(1998, 9, 8),
                IsSubscribed = true,
                EmailConfirmed = true,
                SubscribePlan = Plan.Premium,
                SubscriptionDay = new DateTime(2021, 7, 24),
                ExpireDate = DateTime.Now.AddDays(90),
                Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now, Text = "Hello I really enjoy play games here , is there any chance for a free month please?It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user5.UserName))
            {
                userManager.Create(user5);
                userManager.AddToRole(user5.Id, "Subscriber");
            }

            var user6 = new ApplicationUser()
            {
                UserName = "Papaki",
                Email = "Papaki@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 6, 5),
                FirstName = "Petros",
                LastName = "Ioulios",
                DateOfBirth = new DateTime(1998, 9, 8),
                IsSubscribed = false,
                EmailConfirmed = true,
                SubscriptionDay = new DateTime(2021, 7, 24),
                ExpireDate = new DateTime(2021, 7, 24).AddDays(30),
                Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now, Text = "Hello I really enjoy play games here , is there any chance for a free month please?It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user6.UserName))
            {
                userManager.Create(user6);
                userManager.AddToRole(user6.Id, "Unsubscribed");
            }

            var user7 = new ApplicationUser()
            {
                UserName = "Trixotos",
                Email = "Trixotos@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 6, 5),
                FirstName = "Giannis",
                LastName = "Ioannou",
                PhotoUrl = "/Content/images/user/Trixotos.jpg",
                DateOfBirth = new DateTime(1995, 9, 8),
                IsSubscribed = false,
                EmailConfirmed = true,
                SubscriptionDay = new DateTime(2021, 6, 5),
                ExpireDate = DateTime.Now.AddDays(30),
                Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now, Text = "Hello I really enjoy play games here , is there any chance for a free month please?It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user7.UserName))
            {
                userManager.Create(user7);
                userManager.AddToRole(user7.Id, "Unsubscribed");
            }

            var user8 = new ApplicationUser()
            {
                UserName = "DarthVader",
                Email = "DarthVader123@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 9, 3),
                FirstName = "George",
                LastName = "Ioannou",
                PhotoUrl = "/Content/images/user/DarthVader.jpg",
                DateOfBirth = new DateTime(1995, 10, 28),
                IsSubscribed = true,
                EmailConfirmed = true,
                SubscribePlan = Plan.Premium,
                SubscriptionDay = new DateTime(2021, 9, 3),
                ExpireDate = new DateTime(2021, 9, 3).AddDays(90),
                Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now, Text = "Hello I really enjoy play games here , is there any chance for a free month please?It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user8.UserName))
            {
                userManager.Create(user8);
                userManager.AddToRole(user8.Id, "Subscriber");
            }

            var user9 = new ApplicationUser()
            {
                UserName = "StarDestroyer",
                Email = "StarDestroyer123@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 8, 20),
                FirstName = "Nick",
                LastName = "TheGreek",
                PhotoUrl = "/Content/images/user/StarDestroyer.jpg",
                DateOfBirth = new DateTime(1985, 10, 28),
                IsSubscribed = true,
                EmailConfirmed = true,
                SubscribePlan = Plan.Premium,
                SubscriptionDay = new DateTime(2021, 8, 20),
                ExpireDate = new DateTime(2021, 8, 20).AddDays(90),
                Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now, Text = "Hello I really enjoy play games here , is there any chance for a free month please?It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user9.UserName))
            {
                userManager.Create(user9);
                userManager.AddToRole(user9.Id, "Subscriber");

            }

            var user10 = new ApplicationUser()
            {
                UserName = "Asos",
                Email = "Asos123@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 6, 28),
                FirstName = "Kostas",
                LastName = "Papakostas",
                PhotoUrl = "/Content/images/user/Asos.jpg",
                DateOfBirth = new DateTime(1985, 10, 28),
                IsSubscribed = true,
                EmailConfirmed = true,
                SubscribePlan = Plan.Premium,
                SubscriptionDay = new DateTime(2021, 6, 28),
                ExpireDate = new DateTime(2021, 6, 28).AddDays(90),
                Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now, Text = "Hello I really enjoy play games here , is there any chance for a free month please?It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user10.UserName))
            {
                userManager.Create(user10);
                userManager.AddToRole(user10.Id, "Subscriber");

            }

            var user11 = new ApplicationUser()
            {
                UserName = "Asterix",
                Email = "Asterix@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 8, 18),
                FirstName = "Nikos",
                LastName = "Kotsidas",
                DateOfBirth = new DateTime(1985, 10, 28),
                IsSubscribed = true,
                EmailConfirmed = true,
                SubscribePlan = Plan.Basic,
                SubscriptionDay = new DateTime(2021, 8, 28),
                ExpireDate = new DateTime(2021, 8, 28).AddDays(30),
                Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now, Text = "Hello I really enjoy play games here , is there any chance for a free month please?It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user11.UserName))
            {
                userManager.Create(user11);
                userManager.AddToRole(user11.Id, "Subscriber");
            }

            var user12 = new ApplicationUser()
            {
                UserName = "Obelix",
                Email = "Obelix@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 6, 28),
                FirstName = "Miltos",
                LastName = "Gatsoulis",
                PhotoUrl = "/Content/images/user/Obelix.jpg",
                DateOfBirth = new DateTime(1985, 10, 28),
                IsSubscribed = false,
                EmailConfirmed = true,
                SubscriptionDay = new DateTime(2021, 6, 28),
                ExpireDate = new DateTime(2021, 6, 28).AddDays(30),
                Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now, Text = "Hello I really enjoy play games here , is there any chance for a free month please?It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user12.UserName))
            {
                userManager.Create(user12);
                userManager.AddToRole(user12.Id, "Unsubscribed");
            }

            var user13 = new ApplicationUser()
            {
                UserName = "Indefix",
                Email = "Indefix@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 7, 28),
                FirstName = "Giannis",
                LastName = "Astrogiannis",
                PhotoUrl = "/Content/images/user/Idefix.jpg",
                DateOfBirth = new DateTime(1995, 10, 28),
                IsSubscribed = false,
                EmailConfirmed = true,
                SubscribePlan = Plan.Basic,
                SubscriptionDay = new DateTime(2021, 7, 28),
                ExpireDate = new DateTime(2021, 7, 28).AddDays(30),
                Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now, Text = "Hello I really enjoy play games here , is there any chance for a free month please?It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user13.UserName))
            {
                userManager.Create(user13);
                userManager.AddToRole(user13.Id, "Subscriber");
            }

            var user14 = new ApplicationUser()
            {
                UserName = "Papastroumf",
                Email = "Papastroumf@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 7, 12),
                FirstName = "Aggelos",
                LastName = "Aggelou",
                DateOfBirth = new DateTime(1991, 10, 28),
                IsSubscribed = true,
                EmailConfirmed = true,
                SubscribePlan = Plan.Premium,
                SubscriptionDay = new DateTime(2021, 7, 12),
                ExpireDate = new DateTime(2021, 7, 12).AddDays(90),
                Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now, Text = "Hello I really enjoy play games here , is there any chance for a free month please?It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user14.UserName))
            {
                userManager.Create(user14);
                userManager.AddToRole(user14.Id, "Subscriber");
            }

            var user15 = new ApplicationUser()
            {
                UserName = "Stroumfita",
                Email = "Stroumfita@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 7, 12),
                FirstName = "Niki",
                LastName = "Nikou",
                DateOfBirth = new DateTime(1990, 1, 28),
                EmailConfirmed = true,
                Messages = new List<Message> { new Message() { SubmitDate = new DateTime(2021, 9, 1), Text = "Hello I really enjoy play games here , is there any chance for a free month please?It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user15.UserName))
            {
                userManager.Create(user15);
                userManager.AddToRole(user15.Id, "Unsubscribed");
            }

            var user16 = new ApplicationUser()
            {
                UserName = "Princess",
                Email = "Princess@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 6, 12),
                FirstName = "Xristina",
                LastName = "Xristou",
                PhotoUrl = "/Content/images/user/Princess.jpg",
                DateOfBirth = new DateTime(1990, 1, 28),
                EmailConfirmed = true,
                Messages = new List<Message> { new Message() { SubmitDate = new DateTime(2021, 6, 12), Text = "Hello I really enjoy play games here , is there any chance for a free month please?It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user16.UserName))
            {
                userManager.Create(user16);
                userManager.AddToRole(user16.Id, "Unsubscribed");
            }

            var user17 = new ApplicationUser()
            {
                UserName = "Prince",
                Email = "Prince@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 6, 12),
                FirstName = "Xristos",
                LastName = "Papaxristou",
                PhotoUrl = "/Content/images/user/Prince.jpg",
                DateOfBirth = new DateTime(1992, 1, 28),
                EmailConfirmed = true,
                Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now, Text = "Hello I really enjoy play games here , is there any chance for a free month please?It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user17.UserName))
            {
                userManager.Create(user17);
                userManager.AddToRole(user17.Id, "Unsubscribed");
            }

            var user18 = new ApplicationUser()
            {
                UserName = "Trololo",
                Email = "Trololo@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 8, 24),
                FirstName = "Vasilis",
                LastName = "Kolias",
                PhotoUrl = "/Content/images/user/Trololo.jpg",
                IsSubscribed = true,
                SubscribePlan = Plan.Basic,
                SubscriptionDay = new DateTime(2021, 8, 25),
                ExpireDate = new DateTime(2021, 8, 25).AddDays(30),
                DateOfBirth = new DateTime(1990, 7, 1),
                EmailConfirmed = true,
                Messages = new List<Message> { new Message() { SubmitDate = new DateTime(2021, 8, 25), Text = "Hello I really enjoy play games here , is there any chance for a free month please?It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user18.UserName))
            {
                userManager.Create(user18);
                userManager.AddToRole(user18.Id, "Subscriber");
            }

            var user19 = new ApplicationUser()
            {
                UserName = "Aetomatis",
                Email = "Aetomatis@gmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 6, 12),
                FirstName = "Angelos",
                LastName = "Kaskanis",
                PhotoUrl = "/Content/images/user/Aetomatis.jpg",
                DateOfBirth = new DateTime(1990, 2, 8),
                EmailConfirmed = false,
                Messages = new List<Message> { new Message() { SubmitDate = new DateTime(2021, 6, 12), Text = "Hello I really enjoy play games here , is there any chance for a free month please?It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user19.UserName))
            {
                userManager.Create(user19);
                userManager.AddToRole(user19.Id, "Unsubscribed");
            }

            var user20 = new ApplicationUser()
            {
                UserName = "Provider",
                Email = "Provider1234567@hotmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 9, 4),
                FirstName = "Spyridon",
                LastName = "Providas",
                IsSubscribed = true,
                SubscribePlan = Plan.Premium,
                SubscriptionDay = new DateTime(2021, 9, 4),
                ExpireDate = new DateTime(2021, 9, 4).AddDays(90),
                DateOfBirth = new DateTime(1987, 7, 19),
                EmailConfirmed = true,
                Messages = new List<Message> { new Message() { SubmitDate = new DateTime(2021, 9, 4), Text = "Hello I really enjoy play games here , is there any chance for a free month please? It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user20.UserName))
            {
                userManager.Create(user20);
                userManager.AddToRole(user20.Id, "Subscriber");
            }

            var user21 = new ApplicationUser()
            {
                UserName = "Megaz0rd",
                Email = "Megaz0rdz0r@hotmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 6, 1),
                FirstName = "Vasilis",
                LastName = "Notas",
                PhotoUrl = "/Content/images/user/Megaz0rd.jpg",
                IsSubscribed = false,
                SubscriptionDay = new DateTime(2021, 6, 1),
                ExpireDate = new DateTime(2021, 6, 1).AddDays(30),
                DateOfBirth = new DateTime(1989, 3, 11),
                EmailConfirmed = true,
                Messages = new List<Message> { new Message() { SubmitDate = new DateTime(2021, 6, 1), Text = "Hello I really enjoy play games here , is there any chance for a free month please? It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user21.UserName))
            {
                userManager.Create(user21);
                userManager.AddToRole(user21.Id, "Unsubscribed");
            }

            var user22 = new ApplicationUser()
            {
                UserName = "Gandalf",
                Email = "GandalfTheGrayz0r@hotmail.com",
                PasswordHash = passwordHash.HashPassword("GroupProject21"),
                RegistrationDate = new DateTime(2021, 6, 25),
                FirstName = "Vasilis",
                LastName = "Notas",
                PhotoUrl = "/Content/images/user/Gandalf.jpg",
                IsSubscribed = false,
                DateOfBirth = new DateTime(1993, 11, 8),
                EmailConfirmed = false,
                Messages = new List<Message> { new Message() { SubmitDate = new DateTime(2021, 6, 25), Text = "Hello I really enjoy play games here , is there any chance for a free month please? It's my birthday and i want it!" } }
            };

            if (!context.Users.Any(x => x.UserName == user22.UserName))
            {
                userManager.Create(user22);
                userManager.AddToRole(user22.Id, "Unsubscribed");
            }

            #endregion

            #region Seed Devs
            var d1 = new Developer()
            {
                FirstName = "Kostas",
                LastName = "Korliaftis",
                IsInstaGamesDev = true

            };

            var d2 = new Developer()
            {
                FirstName = "Michael",
                LastName = "Athanasoglou",
                IsInstaGamesDev = true
            };

            var d3 = new Developer()
            {
                FirstName = "Konstantina",
                LastName = "Lakoumenta",
                IsInstaGamesDev = true
            };

            var d4 = new Developer()
            {
                FirstName = "George",
                LastName = "Papadatos",
                IsInstaGamesDev = false
            };

            var d5 = new Developer()
            {
                FirstName = "Todd",
                LastName = "Howard",
                IsInstaGamesDev = false
            };

            var d6 = new Developer()
            {
                FirstName = "Johan",
                LastName = "Andersson",
                IsInstaGamesDev = false
            };

            var developers = new List<Developer>() { d1, d2, d3, d4, d5, d6 };
            foreach (var developer in developers)
            {
                context.Developers.AddOrUpdate(d => new
                {
                    d.FirstName,
                    d.LastName
                }, developer);
            }
            #endregion

            #region Seed Categories
            var c1 = new Category
            {
                Type = "Arcade",
                Description = "While the category still exists it has morphed quite a bit since the early 70s to the mid 80s when arcades could be found in large shopping malls, " +
                              "small strip centers, pizzerias, pool halls etc. The games took money and in turn gave the player a set number of lives" +
                              " to complete as much as they could without losing a life. Some games allowed for cooperative play (multiple players pay at the same time)" +
                              " others were single player but allowed multiple players to play in turn, competing for high scores."
            };
            var c2 = new Category
            {
                Type = "Math",
                Description = "A mathematical game is a game whose rules, strategies, and outcomes are defined by clear mathematical parameters.Often," +
                              " such games have simple rules and match procedures, such as Tic-tac-toe and Dots and Boxes. Generally, mathematical games need not be conceptually" +
                              " intricate to involve deeper computational underpinnings. For example, even though the rules of Mancala are relatively basic," +
                              " the game can be rigorously analyzed through the lens of combinatorial game theory."
            };
            var c3 = new Category
            {
                Type = "Fun",
                Description = "Fun games, also known as 'casual' games are video games targeted at a mass market audience, as opposed to a hardcore game, which is " +
                              "targeted at hobbyist gamers. Casual games may exhibit any type of gameplay and genre. They generally involve simpler rules, shorter sessions, and require less learned skill."

            };
            var c4 = new Category
            {
                Type = "Classic",
                Description = "Classic games, also known as retro games and old school games, are considered the older versions of video games (generally arcade) " +
                              "in contemporary times. Usually, classic games are based upon systems that are obsolete or discontinued. They are typically put into practice " +
                              "for the purpose of nostalgia, preservation or the need to achieve authenticity."
            };
            var c5 = new Category
            {
                Type = "Action",
                Description = "An action game is a video game genre that emphasizes physical challenges, including hand–eye coordination and reaction-time." +
                " The genre includes a large variety of sub-genres, such as fighting games, beat 'em ups, shooter games and platform games." +
                " Multiplayer online battle arena and some real-time strategy games are also considered action games. "
            };
            var c6 = new Category
            {
                Type = "Text Adventure",
                Description = "Text adventures (sometimes synonymously referred to as interactive fiction) are text-based games wherein worlds are described in the narrative" +
                              " and the player submits typically simple commands to interact with the worlds. Colossal Cave Adventure is considered to be the first adventure game," +
                              " and indeed the name of the genre adventure game is derived from the title."
            };
            var c7 = new Category
            {
                Type = "Fantasy",
                Description = "In Fantasy games, players usually control the actions of a single character while undertaking a quest in or otherwise exploring an elaborate virtual world."
            };
            var c8 = new Category
            {
                Type = "Horror",
                Description = "Horror games are centered on horror fiction and typically designed to scare the player. Unlike most other video game genres, which are classified by their gameplay," +
                              " horror games are nearly always based on narrative or visual presentation, and use a variety of gameplay types. " +
                              "One of the best-defined and most common types of horror games are survival horror games."
            };

            var categories = new List<Category>() { c1, c2, c3, c4, c5, c6, c7, c8 };
            foreach (var category in categories)
            {
                context.Categories.AddOrUpdate(c => c.Type, category);
            }
            #endregion

            #region Seed Pegi

            var p3 = new Pegi { PegiAge = 3, PegiPhoto = "/Content/images/Pegi/3.jpg" };
            var p7 = new Pegi { PegiAge = 7, PegiPhoto = "/Content/images/Pegi/7.jpg", };
            var p12 = new Pegi { PegiAge = 12, PegiPhoto = "/Content/images/Pegi/12.jpg", };
            var p16 = new Pegi { PegiAge = 16, PegiPhoto = "/Content/images/Pegi/16.jpg", };
            var p18 = new Pegi { PegiAge = 18, PegiPhoto = "/Content/images/Pegi/18.jpg", };

            var pegiList = new List<Pegi> { p3, p7, p12, p16, p18 };

            foreach (var pegi in pegiList)
            {
                context.Pegi.AddOrUpdate(p => p.PegiAge, pegi);
            }

            #endregion

            #region Seed Games
            var g1 = new Game()
            {
                Title = "Brick Breaker",
                ReleaseDate = new DateTime(2021, 3, 20),
                Description = "Brick Breaker Classic is inspired by some of the best classic arcade games." +
                   " The goal of the game is to break all the bricks in each level without dropping the ball into the abyss. Every brick you destroy gives you points." +
                   " Destroying multiple bricks at a time grants you bonus points. The more points you get, the more stars you will receive at the end of the level.",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p3,
                Photo = "/Content/images/Games/BrickBreaker.png",
                GameUrl = "https://i.simmer.io/@InstaGames/brick-breaker",
                GameCategories = new Collection<Category>() { c1, c3, c4 },
                GameDevelopers = new Collection<Developer>() { d1 },
                Subscribers = new Collection<ApplicationUser>() { user1, user11, user13, user14 }
            };

            var g2 = new Game()
            {
                Title = "Space Wars",
                ReleaseDate = new DateTime(2021, 8, 5),
                Description = "Explore different planets! Space Wars is a spaceship battle action game , shoot enemy spaceships to increase your score , try to survive and advance to the next level.",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p3,
                Trailer = "/Content/video/starships-game.mp4",
                Photo = "/Content/images/Games/Space-Wars.png",
                GameUrl = "https://i.simmer.io/@InstaGames/space-wars",
                GameCategories = new Collection<Category>() { c5, c7 },
                GameDevelopers = new Collection<Developer>() { d1 },
                Subscribers = new Collection<ApplicationUser>() { user1, user2, user12, user16, user14, user17, user7 }
            };

            var g3 = new Game()
            {
                Title = "Obstacle Avoiding",
                ReleaseDate = new DateTime(2021, 7, 15),
                Description = "Avoiding Obstacles Game - first unfinished version for alpha testing",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p3,
                GameCategories = new Collection<Category>() { c3, c4, c5 },
                Photo = "/Content/images/Games/ObstacleAvoiding.png",
                GameUrl = "https://i.simmer.io/@InstaGames/obstacle-avoiding-aplha",
                IsEarlyAccess = true,
                GameDevelopers = new Collection<Developer>() { d1 },
                Subscribers = new Collection<ApplicationUser>() { user1, user2, user9, user17 }
            };

            var g4 = new Game()
            {
                Title = "The Prophet",
                ReleaseDate = new DateTime(2021, 5, 19),
                Description = "Simple Text Adventure within a fantasy setting",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p7,
                GameCategories = new Collection<Category>() { c4, c6 },
                Photo = "/Content/images/Games/ProphetImage.png",
                GameUrl = @"https://i.simmer.io/@InstaGames/the-prophet",
                GameDevelopers = new Collection<Developer>() { d2 },
                Subscribers = new Collection<ApplicationUser>() { user1, user2, user3, user4, user5 }
            };

            var g5 = new Game()
            {
                Title = "Tic Tac Toe",
                ReleaseDate = new DateTime(2021, 4, 19),
                Description = "The classic game of Tic Tac Toe played against the computer",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p3,
                GameCategories = new Collection<Category>() { c2, c3, c4 },
                Photo = "/Content/images/Games/tictactoe.png",
                GameUrl = @"https://i.simmer.io/@InstaGames/tictactoe",
                GameDevelopers = new Collection<Developer>() { d1, d3 },
                Subscribers = new Collection<ApplicationUser>() { user1, user2, user3, user4, user5, user13 }
            };

            var g6 = new Game()
            {
                Title = "Number Wizard",
                ReleaseDate = new DateTime(2021, 3, 15),
                Description = "It is a simple number guessing game created in Unity",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p3,
                Photo = "/Content/images/Games/NumberWizzard.png",
                GameUrl = "https://i.simmer.io/@InstaGames/guessing-game",
                GameCategories = new Collection<Category>() { c3, c4, c2 },
                GameDevelopers = new Collection<Developer>() { d1 },
                Subscribers = new Collection<ApplicationUser>() { user1, user2 }
            };

            var g7 = new Game()
            {
                Title = "Overcooked",
                ReleaseDate = new DateTime(2021, 10, 7),
                Description = "Overcooked is a chaotic couch co-op cooking game for one to four players. Working as a team, you and your fellow chefs must prepare, cook and serve up a variety of tasty orders before the baying customers storm out in a huff.",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p3,
                GameCategories = new Collection<Category>() { c5, c3 },
                Photo = "/Content/images/Games/overcooked.jpg",
                GameDevelopers = new Collection<Developer>() { d1, d2 },
                Subscribers = new Collection<ApplicationUser>() { user10, user12, user13, user1 }
            };

            var g8 = new Game()
            {
                Title = "Until Dawn",
                ReleaseDate = new DateTime(2021, 12, 8),
                Description = "When eight friends are trapped on a remote mountain retreat and things quickly turn sinister, they start to suspect they aren’t alone.Gripped by fear and with tensions in the group running high,you’ll be forced to make snap decisions that could mean life,or death, for everyone involved.",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p18,
                GameCategories = new Collection<Category>() { c8, c7 },
                Photo = "/Content/images/Games/UntilDawn.png",
                GameDevelopers = new Collection<Developer>() { d4, d1 },
                Subscribers = new Collection<ApplicationUser>() { user10, user12, user13, user1 }
            };

            var g9 = new Game()
            {
                Title = "Outlast",
                ReleaseDate = new DateTime(2021, 10, 7),
                Description = "Hell is an experiment you can't survive in Outlast, a first-person survival horror game developed by veterans of some of the biggest game franchises in history. As investigative journalist Miles Upshur, explore Mount Massive Asylum and try to survive long enough to discover its terrible secret... ",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p18,
                GameCategories = new Collection<Category>() { c8, c7 },
                Photo = "/Content/images/Games/Outlast.jpg",
                GameDevelopers = new Collection<Developer>() { d3, d4 },
                Subscribers = new Collection<ApplicationUser>() { user9, user7, user17, user13 }
            };

            var g10 = new Game()
            {
                Title = "Half-Life",
                ReleaseDate = new DateTime(2021, 10, 11),
                Description = "Blends action and adventure with award-winning technology to create a frighteningly realistic world where players must think to survive. Also includes an exciting multiplayer mode that allows you to play against friends and enemies...",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p16,
                GameCategories = new Collection<Category>() { c5 },
                Photo = "/Content/images/Games/half-life.jpg",
                GameDevelopers = new Collection<Developer>() { d1, d2 }
            };


            var g11 = new Game()
            {
                Title = "Portal",
                ReleaseDate = new DateTime(2021, 9, 11),
                Description = " Set in the mysterious Aperture Science Laboratories, Portal has been called one of the most innovative new games on the horizon and will offer gamers hours of unique gameplay",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p16,
                GameCategories = new Collection<Category>() { c5 },
                Photo = "/Content/images/Games/portal.jpg",
                GameDevelopers = new Collection<Developer>() { d4 }
            };

            var g12 = new Game()
            {
                Title = "Left 4 Dead",
                ReleaseDate = new DateTime(2021, 9, 3),
                Description = "A co-op action horror game that casts up to four players in an epic struggle for survival against swarming zombie hordes and terrifying mutant monsters.",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p18,
                GameCategories = new Collection<Category>() { c8, c5 },
                Photo = "/Content/images/Games/left4dead.jpg",
                GameDevelopers = new Collection<Developer>() { d1 }
            };
            var g13 = new Game()
            {
                Title = "Civilization",
                ReleaseDate = new DateTime(2021, 9, 3),
                Description = "New ways to interact with your world, expand your empire across the map, advance your culture, and compete against history’s greatest leaders to build a civilization that will stand the test of time. Play as one of 20 historical leaders including Roosevelt (America) and Victoria (England)",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p12,
                GameCategories = new Collection<Category>() { c7 },
                Photo = "/Content/images/Games/civilization.jpg",
                GameDevelopers = new Collection<Developer>() { d1, d4 }
            };
            var g14 = new Game()
            {
                Title = "Assassin’s Creed",
                ReleaseDate = new DateTime(2014, 9, 12),
                Description = "The French Revolution was led by the people. They stood together and fought the oppression. This year, and for the 1st time in the Assassin's Creed® franchise, team up with friends to fight and destroy the symbols of oppression.",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p18,
                Photo = "/Content/images/Games/Assassins.jpg",
                Trailer = "/Content/video/Assassins-Creed.mp4",
                GameCategories = new Collection<Category>() { c5, c7 },
                GameDevelopers = new Collection<Developer>() { d2 },
                Subscribers = new Collection<ApplicationUser>() { user9, user7, user17, user13, user16 }
            };
            var g15 = new Game()
            {
                Title = "Fable Anniversary",
                ReleaseDate = new DateTime(2021, 9, 12),
                Description = "Fully re-mastered with HD visuals and audio, Fable Anniversary is a stunning rendition of the original game that will delight faithful fans and new players alike! The all new Heroic difficulty setting will test the mettle of even the most hardcore Fable fan.",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p16,
                GameCategories = new Collection<Category>() { c6 },
                Photo = "/Content/images/Games/fable.jpg",
                GameDevelopers = new Collection<Developer>() { d4 }
            };
            var g16 = new Game()
            {
                Title = "Kbyte",
                ReleaseDate = new DateTime(2021, 9, 12),
                Description = "Kbyte is a platform/puzzle game made in various styles of pixel art, ranging from 8bit to 16bit; it is a graphic journey through all the ages of the video game industry, so it uses both modern and classic game dynamics",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p7,
                GameCategories = new Collection<Category>() { c6 },
                Photo = "/Content/images/Games/Kbyte.jpg",
                GameDevelopers = new Collection<Developer>() { d4 }
            };

            var g17 = new Game()
            {
                Title = "Toast Time",
                ReleaseDate = new DateTime(2021, 9, 12),
                Description = "Toast Time is a throwback to the golden age of video games where old-school homebrew titles fused arcade action with a distinctly British sense of humour.",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p7,
                GameCategories = new Collection<Category>() { c4 },
                Photo = "/Content/images/Games/ToastTime.jpg",
                GameDevelopers = new Collection<Developer>() { d2 }
            };
            var g18 = new Game()
            {
                Title = "WolfQuest",
                ReleaseDate = new DateTime(2021, 9, 12),
                Description = "LIVE THE LIFE OF A WILD WOLF! As a two-year-old gray wolf in Yellowstone National Park, hunt elk and moose, find a mate, and then establish a territory and raise your pups.",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p12,
                GameCategories = new Collection<Category>() { c4 },
                Photo = "/Content/images/Games/WolfQuest.jpg",
                GameDevelopers = new Collection<Developer>() { d2 }
            };
            var g19 = new Game()
            {
                Title = "Math Fun",
                ReleaseDate = new DateTime(2021, 9, 12),
                Description = "Math Fun is amazing fun loving app for everyone. This app will let you learn math in an fun and easy manner while playing.",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p7,
                GameCategories = new Collection<Category>() { c4 },
                Photo = "/Content/images/Games/MathFun.jpg",
                GameDevelopers = new Collection<Developer>() { d2 }
            };

            var g20 = new Game()
            {
                Title = "Air Forte",
                ReleaseDate = new DateTime(2021, 9, 12),
                Description = "Air Forte is a high-altitude game of math, vocabulary, and geography. Compete with friends or fly solo in the various arenas. Good luck, pilot!Key features:Play with friends: Up to four people can play Air Forte together. Find out who's the best pilot!",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p12,
                GameCategories = new Collection<Category>() { c4 },
                Photo = "/Content/images/Games/AirForte.jpg",
                GameDevelopers = new Collection<Developer>() { d2 }
            };

            var g21 = new Game()
            {
                Title = "Canyon Capers",
                ReleaseDate = new DateTime(2021, 9, 12),
                Description = "Remember fondly playing old style platformers, where you didn't have to learn a hundred button combos to play it successfully? canyon capers is just for you if that's the case.canyon capers is a retro style arcade platformer for all ages, created by the authors of the 1992 original.",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p16,
                GameCategories = new Collection<Category>() { c4 },
                Photo = "/Content/images/Games/CanyonCapers.jpg",
                GameDevelopers = new Collection<Developer>() { d2 }
            };

            var g22 = new Game()
            {
                Title = "Monster Hunter Stories",
                ReleaseDate = new DateTime(2021, 9, 12),
                Description = "Wings of ruin, players are able to play the opening portion of the game for free. Save data from the demo can be carried over to the retail version. mount up and get ready to experience an all new rpg adventure set in the world of monster hunter.",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p12,
                GameCategories = new Collection<Category>() { c4 },
                Photo = "/Content/images/Games/MonsterHunterStories.jpg",
                GameDevelopers = new Collection<Developer>() { d2 }
            };

            var g23 = new Game()
            {
                Title = "Penarium",
                ReleaseDate = new DateTime(2022, 6, 7),
                Description = "Penarium is a frantic 2D arena arcade game where you take on the role of Willy, trapped in a sinister circus show. Run, jump and avoid an array of killer death-traps while being cheered on by a sadistic crowd that’s out for blood.",
                Tag = Tag.Singleplayer,
                IsReleased = false,
                Pegi = p3,
                GameCategories = new Collection<Category>() { c3, c5 },
                Photo = "/Content/images/Games/Penarium.jpg",
                GameDevelopers = new Collection<Developer>() { d2, d3 }
            };

            var g24 = new Game()
            {
                Title = "Catastrophe",
                ReleaseDate = new DateTime(2021, 8, 19),
                Description = "Action packed game, when you level up you can pick which defenders you want. One of the top played games.",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p16,
                Photo = "/Content/images/Games/catastrophe.jpg",
                GameUrl = @"https://i.simmer.io/@Erus/catastrophe",
                GameCategories = new Collection<Category>() { c5, c7 },
                GameDevelopers = new Collection<Developer>() { d1, d2 },
                Subscribers = new Collection<ApplicationUser>() { user17, user21, user22 }
            };

            var g25 = new Game()
            {
                Title = "Slope",
                ReleaseDate = new DateTime(2021, 8, 15),
                Description = "Arcade game based on the old arcade classics.",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p7,
                Photo = "/Content/images/Games/slope.jpg",
                GameUrl = "https://i.simmer.io/@ClumsyPanda/slope",
                GameCategories = new Collection<Category>() { c1, c4 },
                GameDevelopers = new Collection<Developer>() { d4 },
                Subscribers = new Collection<ApplicationUser>() { user18, user19, user2 }
            };


            var g26 = new Game()
            {
                Title = "The Moon 2050",
                ReleaseDate = new DateTime(2020, 11, 18),
                Description = "Navigate an unfriendly extraterrestrial wasteland, avoiding enemies and mines as you go!",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p16,
                Photo = "/Content/images/Games/themoon2050.jpg",
                GameUrl = "https://i.simmer.io/@amatory22220000/the-moon-2050",
                GameCategories = new Collection<Category>() { c5 },
                GameDevelopers = new Collection<Developer>() { d2, d5 },
                Subscribers = new Collection<ApplicationUser>() { user1, user3, user22, user18, user19, user21 }
            };

            var g27 = new Game()
            {
                Title = "Noob Paradise",
                ReleaseDate = new DateTime(2021, 7, 30),
                Description = "First person shooter featuring several levels, a selection of weapons and a plethora of enemies!",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p18,
                Trailer = "/Content/video/NoobParadiseTrailer.mp4",
                Photo = "/Content/images/Games/NoobParadise.jpg",
                GameUrl = "https://i.simmer.io/@Badal/noob-paradise",
                GameCategories = new Collection<Category>() { c4, c5 },
                GameDevelopers = new Collection<Developer>() { d3, d5 },
                Subscribers = new Collection<ApplicationUser>() { user1, user19, user20, user21, user22 }
            };

            var g28 = new Game()
            {
                Title = "Galaxy Blaster",
                ReleaseDate = new DateTime(2021, 8, 5),
                Description = "Blast your way through twelve increasingly difficult waves of enemies to face the ultimate power in the universe: the dreaded Galaxy Blaster!",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p12,
                Photo = "/Content/images/Games/GalaxyBlaster.jpg",
                GameUrl = "https://i.simmer.io/@eMeLDi/galaxy-blaster",
                GameCategories = new Collection<Category>() { c1, c5 },
                GameDevelopers = new Collection<Developer>() { d5 },
                Subscribers = new Collection<ApplicationUser>() { user5, user8, user13, user16, user18 }
            };

            var g29 = new Game()
            {
                Title = "Powered By Peanut Butter",
                ReleaseDate = new DateTime(2021, 6, 29),
                Description = "800 years in the future Peanut Butter has been declared illegal! On a hostile planet you must defend the last Real Peanut Butter factory" +
                              " which sells its product on the galactic black market to fund the Colonial Resistance. The fate of the galaxy rests in your hands. " +
                              "This is a demo game, with 4 levels so far.More to come.",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p7,
                Trailer = "/Content/video/PoweredByPeanutTrailer.mp4",
                Photo = "/Content/images/Games/PoweredByPeanutButter.jpg",
                GameUrl = "https://i.simmer.io/@DPulcifer/powered-by-peanut-butter",
                GameCategories = new Collection<Category>() { c3 },
                GameDevelopers = new Collection<Developer>() { d6 },
                Subscribers = new Collection<ApplicationUser>() { user1, user3, user11, user13, user17, user15 }
            };

            var g30 = new Game()
            {
                Title = "Kingdom Runner",
                ReleaseDate = new DateTime(2021, 5, 19),
                Description = "A 2D and Platformer, in which the player has the power to change gravity and thus apart from going through the ground, he can also" +
                              " go through the ceiling. Beware of enemies and try not to fall into the holes!",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p18,
                Photo = "/Content/images/Games/KingdomRunner.jpg",
                GameUrl = "https://i.simmer.io/@MiniRoxy/kingdom-runner",
                GameCategories = new Collection<Category>() { c1 },
                GameDevelopers = new Collection<Developer>() { d5, d6 },
                Subscribers = new Collection<ApplicationUser>() { user2, user7, user10 }
            };

            var g31 = new Game()
            {
                Title = "GridXDash",
                ReleaseDate = new DateTime(2021, 6, 7),
                Description = "Main goal: Collect as many green points as possible! Tips: Avoid hitting red triangles, pick up green squares for points, and don't let squares " +
                              "get by you! (This will damage your health bar)",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p7,
                Photo = "/Content/images/Games/gridxdash.jpg",
                GameUrl = "https://i.simmer.io/@mkmerino/grid-x-dash",
                GameCategories = new Collection<Category>() { c7 },
                GameDevelopers = new Collection<Developer>() { d3 },
                Subscribers = new Collection<ApplicationUser>() { user1, user3, user9, user15 }
            };

            var g32 = new Game()
            {
                Title = "Leave Her Johnny",
                ReleaseDate = new DateTime(2021, 6, 7),
                Description = "Leave Her Johnny is a game about a captain named Johnny who looks for his love, he finds out she was staying at a lighthouse and she's been kidnapped by pirates.",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p3,
                Trailer = "/Content/video/LeaveHerJohnny.mp4",
                Photo = "/Content/images/Games/LeaveHerJohnny.jpg",
                GameUrl = "https://i.simmer.io/@EmirRedz/leave-her-johnny",
                GameCategories = new Collection<Category>() { c5 },
                GameDevelopers = new Collection<Developer>() { d3 },
                Subscribers = new Collection<ApplicationUser>() { user1, user3, user9, user15 }
            };

            var g33 = new Game()
            {
                Title = "Push The Box",
                ReleaseDate = new DateTime(2021, 4, 18),
                Description = "In this isometric game, you push around crates to reach the end diamond. Crates can be used to cross water or to trigger buttons or to reach higher places. " +
                              "In some instances, you have to think smart to utilize a crate. The player and crates can make use of a lift-block that takes you to a higher place in the level.",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p7,
                Photo = "/Content/images/Games/PushTheBox.jpg",
                GameUrl = "https://i.simmer.io/@Nannings/push-the-box",
                GameCategories = new Collection<Category>() { c3 },
                GameDevelopers = new Collection<Developer>() { d1 },
                Subscribers = new Collection<ApplicationUser>() { user1, user2, user3, user4, user10, user11, user13, user15 }
            };

            var g34 = new Game()
            {
                Title = "Connected Towers",
                ReleaseDate = new DateTime(2020, 12, 22),
                Description = "Connected towers is a puzzle game where you connect or disconnect towers from their power supply. You play as a little robot, who pushes towers trying " +
                              "to make his way through each puzzle. He needs to overcome obstacles such as gates, mazes, moving platforms and lava to make it to the end goal.",
                Tag = Tag.Singleplayer,
                IsReleased = true,
                Pegi = p7,
                Photo = "/Content/images/Games/ConnectedTowers.jpg",
                GameUrl = "https://i.simmer.io/@Nannings/connected-towers",
                GameCategories = new Collection<Category>() { c3 },
                GameDevelopers = new Collection<Developer>() { d2 },
                Subscribers = new Collection<ApplicationUser>() { user3, user6, user9, user12, user15, user18, user21 }
            };

            var games = new List<Game>() { g1, g2, g3, g4, g5, g6, g7, g8, g9, g10, g11, g12, g13, g14, g15, g16, g17, g18, g19, g20, g21, g22, g23, g24, g25, g26, g27, g28, g29, g30, g31, g32, g33, g34 };
            foreach (var game in games)
            {
                context.Games.AddOrUpdate(c => c.Title, game);
            }
            #endregion

            #region Seed UserGameRatings

            var ugr1 = new UserGameRatings
            {
                UserGameRatingsId = 1,
                ApplicationUser = user1,
                Game = g1,
                Rating = 4
            };

            var ugr2 = new UserGameRatings
            {
                UserGameRatingsId = 2,
                ApplicationUser = user1,
                Game = g14,
                Rating = 5
            };

            var ugr3 = new UserGameRatings
            {
                UserGameRatingsId = 3,
                ApplicationUser = user1,
                Game = g2,
                Rating = 5
            };

            var ugr4 = new UserGameRatings
            {
                UserGameRatingsId = 4,
                ApplicationUser = user1,
                Game = g6,
                Rating = 1
            };

            var ugr5 = new UserGameRatings
            {
                UserGameRatingsId = 5,
                ApplicationUser = user2,
                Game = g1,
                Rating = 3
            };

            var ugr6 = new UserGameRatings
            {
                UserGameRatingsId = 6,
                ApplicationUser = user2,
                Game = g2,
                Rating = 5
            };

            var ugr7 = new UserGameRatings
            {
                UserGameRatingsId = 7,
                ApplicationUser = user2,
                Game = g5,
                Rating = 5
            };

            var ugr8 = new UserGameRatings
            {
                UserGameRatingsId = 8,
                ApplicationUser = user2,
                Game = g6,
                Rating = 5
            };

            var ugr9 = new UserGameRatings
            {
                UserGameRatingsId = 9,
                ApplicationUser = user2,
                Game = g14,
                Rating = 4
            };

            var ugr10 = new UserGameRatings
            {
                UserGameRatingsId = 10,
                ApplicationUser = user3,
                Game = g1,
                Rating = 5
            };

            var ugr11 = new UserGameRatings
            {
                UserGameRatingsId = 11,
                ApplicationUser = user3,
                Game = g4,
                Rating = 4
            };

            var ugr12 = new UserGameRatings
            {
                UserGameRatingsId = 12,
                ApplicationUser = user3,
                Game = g5,
                Rating = 4
            };

            var ugr13 = new UserGameRatings
            {
                UserGameRatingsId = 13,
                ApplicationUser = user4,
                Game = g2,
                Rating = 5
            };

            var ugr14 = new UserGameRatings
            {
                UserGameRatingsId = 14,
                ApplicationUser = user4,
                Game = g4,
                Rating = 5
            };

            var ugr15 = new UserGameRatings
            {
                UserGameRatingsId = 15,
                ApplicationUser = user4,
                Game = g5,
                Rating = 5
            };

            var ugr16 = new UserGameRatings
            {
                UserGameRatingsId = 16,
                ApplicationUser = user4,
                Game = g6,
                Rating = 4
            };

            var ugr17 = new UserGameRatings
            {
                UserGameRatingsId = 17,
                ApplicationUser = user6,
                Game = g12,
                Rating = 3
            };

            var ugr18 = new UserGameRatings
            {
                UserGameRatingsId = 18,
                ApplicationUser = user6,
                Game = g2,
                Rating = 5
            };

            var ugr19 = new UserGameRatings
            {
                UserGameRatingsId = 19,
                ApplicationUser = user7,
                Game = g8,
                Rating = 4
            };

            var ugr20 = new UserGameRatings
            {
                UserGameRatingsId = 20,
                ApplicationUser = user7,
                Game = g14,
                Rating = 5
            };

            var ugr21 = new UserGameRatings
            {
                UserGameRatingsId = 21,
                ApplicationUser = user8,
                Game = g8,
                Rating = 3
            };

            var ugr22 = new UserGameRatings
            {
                UserGameRatingsId = 22,
                ApplicationUser = user9,
                Game = g12,
                Rating = 2
            };

            var ugr23 = new UserGameRatings
            {
                UserGameRatingsId = 23,
                ApplicationUser = user10,
                Game = g6,
                Rating = 2
            };

            var ugr24 = new UserGameRatings
            {
                UserGameRatingsId = 24,
                ApplicationUser = user10,
                Game = g12,
                Rating = 4
            };

            var ugr25 = new UserGameRatings
            {
                UserGameRatingsId = 25,
                ApplicationUser = user11,
                Game = g2,
                Rating = 5
            };

            var ugr26 = new UserGameRatings
            {
                UserGameRatingsId = 26,
                ApplicationUser = user12,
                Game = g2,
                Rating = 5
            };


            var ugr27 = new UserGameRatings
            {
                UserGameRatingsId = 27,
                ApplicationUser = user12,
                Game = g6,
                Rating = 4
            };


            var ugr28 = new UserGameRatings
            {
                UserGameRatingsId = 28,
                ApplicationUser = user16,
                Game = g2,
                Rating = 5
            };

            var ugr29 = new UserGameRatings
            {
                UserGameRatingsId = 29,
                ApplicationUser = user16,
                Game = g1,
                Rating = 4
            };

            var ugr30 = new UserGameRatings
            {
                UserGameRatingsId = 30,
                ApplicationUser = user17,
                Game = g2,
                Rating = 5
            };

            var ugr31 = new UserGameRatings
            {
                UserGameRatingsId = 31,
                ApplicationUser = user18,
                Game = g24,
                Rating = 4
            };

            var ugr32 = new UserGameRatings
            {
                UserGameRatingsId = 32,
                ApplicationUser = user19,
                Game = g24,
                Rating = 2
            };

            var ugr33 = new UserGameRatings
            {
                UserGameRatingsId = 33,
                ApplicationUser = user20,
                Game = g24,
                Rating = 4
            };

            var ugr34 = new UserGameRatings
            {
                UserGameRatingsId = 34,
                ApplicationUser = user20,
                Game = g25,
                Rating = 4
            };

            var ugr35 = new UserGameRatings
            {
                UserGameRatingsId = 35,
                ApplicationUser = user21,
                Game = g25,
                Rating = 1
            };

            var ugr36 = new UserGameRatings
            {
                UserGameRatingsId = 36,
                ApplicationUser = user22,
                Game = g25,
                Rating = 5
            };

            var ugr37 = new UserGameRatings
            {
                UserGameRatingsId = 37,
                ApplicationUser = user22,
                Game = g26,
                Rating = 2
            };

            var ugr38 = new UserGameRatings
            {
                UserGameRatingsId = 38,
                ApplicationUser = user14,
                Game = g26,
                Rating = 4
            };

            var ugr39 = new UserGameRatings
            {
                UserGameRatingsId = 39,
                ApplicationUser = user18,
                Game = g27,
                Rating = 5
            };

            var ugr40 = new UserGameRatings
            {
                UserGameRatingsId = 40,
                ApplicationUser = user20,
                Game = g27,
                Rating = 3
            };

            var ugr41 = new UserGameRatings
            {
                UserGameRatingsId = 41,
                ApplicationUser = user21,
                Game = g28,
                Rating = 5
            };

            var ugr42 = new UserGameRatings
            {
                UserGameRatingsId = 42,
                ApplicationUser = user22,
                Game = g28,
                Rating = 2
            };

            var ugr43 = new UserGameRatings
            {
                UserGameRatingsId = 43,
                ApplicationUser = user11,
                Game = g28,
                Rating = 4
            };

            var ugr44 = new UserGameRatings
            {
                UserGameRatingsId = 44,
                ApplicationUser = user11,
                Game = g29,
                Rating = 3
            };

            var ugr45 = new UserGameRatings
            {
                UserGameRatingsId = 45,
                ApplicationUser = user14,
                Game = g29,
                Rating = 4
            };

            var ugr46 = new UserGameRatings
            {
                UserGameRatingsId = 46,
                ApplicationUser = user17,
                Game = g30,
                Rating = 2
            };

            var ugr47 = new UserGameRatings
            {
                UserGameRatingsId = 47,
                ApplicationUser = user19,
                Game = g31,
                Rating = 3
            };

            var ugr48 = new UserGameRatings
            {
                UserGameRatingsId = 48,
                ApplicationUser = user20,
                Game = g31,
                Rating = 1
            };

            var ugr49 = new UserGameRatings
            {
                UserGameRatingsId = 49,
                ApplicationUser = user2,
                Game = g32,
                Rating = 3
            };

            var ugr50 = new UserGameRatings
            {
                UserGameRatingsId = 50,
                ApplicationUser = user7,
                Game = g33,
                Rating = 5
            };

            var ugr51 = new UserGameRatings
            {
                UserGameRatingsId = 51,
                ApplicationUser = user14,
                Game = g33,
                Rating = 4
            };

            var ugr52 = new UserGameRatings
            {
                UserGameRatingsId = 52,
                ApplicationUser = user20,
                Game = g33,
                Rating = 3
            };

            var ugr53 = new UserGameRatings
            {
                UserGameRatingsId = 53,
                ApplicationUser = user21,
                Game = g34,
                Rating = 3
            };

            var ugr54 = new UserGameRatings
            {
                UserGameRatingsId = 54,
                ApplicationUser = user16,
                Game = g34,
                Rating = 4
            };

            var ugr55 = new UserGameRatings
            {
                UserGameRatingsId = 55,
                ApplicationUser = user18,
                Game = g34,
                Rating = 5
            };

            var ugr56 = new UserGameRatings
            {
                UserGameRatingsId = 56,
                ApplicationUser = user21,
                Game = g34,
                Rating = 2
            };

            var ugr57 = new UserGameRatings
            {
                UserGameRatingsId = 57,
                ApplicationUser = user18,
                Game = g34,
                Rating = 4
            };

            var userGameRatings = new List<UserGameRatings> { ugr1, ugr2, ugr3, ugr4, ugr5, ugr6, ugr7, ugr8, ugr9, ugr10,
                ugr11, ugr12, ugr13, ugr14, ugr15, ugr16, ugr17, ugr18, ugr19, ugr20,
                ugr21, ugr22 , ugr23, ugr24,ugr25,ugr26,ugr27,ugr28,ugr29,ugr30, ugr31, ugr32, ugr33, ugr34, ugr35,
                ugr36, ugr37, ugr38, ugr39, ugr40, ugr41, ugr42, ugr43, ugr44, ugr45, ugr46, ugr47, ugr48, ugr49,
                ugr50, ugr51, ugr52, ugr53, ugr54, ugr55, ugr56, ugr57
            };
            foreach (var userGameRating in userGameRatings)
            {
                context.UserGameRatings.AddOrUpdate(g => new {g.UserGameRatingsId}, userGameRating);
            }

            #endregion

            context.SaveChanges();
        }
    }
}
