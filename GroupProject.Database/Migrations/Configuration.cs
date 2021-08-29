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
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }

            if (!context.Roles.Any(x => x.Name == "Subscriber"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Subscriber" };
                manager.Create(role);
            }

            if (!context.Roles.Any(x => x.Name == "Unsubscribed"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Unsubscribed" };
                manager.Create(role);
            }

            #endregion

            #region Adding Admins 

            if (!context.Users.Any(x => x.UserName == "GameMaster"))
            {
                var passwordHash = new PasswordHasher();
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser()
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
                    Messages = new List<Message> { new Message() { SubmitDate = DateTime.Now , Text = "Hello fellow admins I am testing the Message section!"} }
                };

                manager.Create(user);

                manager.AddToRole(user.Id, "Admin");
            }


            if (!context.Users.Any(x => x.UserName == "mikedalpha"))
            {
                var passwordHash = new PasswordHasher();
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser()
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

                manager.Create(user);

                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(x => x.UserName == "kwstaskor"))
            {
                var passwordHash = new PasswordHasher();
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser()
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

                manager.Create(user);

                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(x => x.UserName == "konstantinala"))
            {
                var passwordHash = new PasswordHasher();
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser()
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

                manager.Create(user);

                manager.AddToRole(user.Id, "Admin");
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

            var developers = new List<Developer>() { d1, d2, d3, d4 };
            foreach (var developer in developers)
            {
                context.Developers.AddOrUpdate(d => new { d.FirstName, d.LastName }, developer);
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
                GameDevelopers = new Collection<Developer>() { d1 }
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
                GameDevelopers = new Collection<Developer>() { d1 }
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
                GameDevelopers = new Collection<Developer>() { d1 }
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
                GameDevelopers = new Collection<Developer>() { d2 }
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
                GameDevelopers = new Collection<Developer>() { d1, d3 }
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
                GameDevelopers = new Collection<Developer>() { d1 }
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
                GameDevelopers = new Collection<Developer>() { d1, d2 }
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
                GameDevelopers = new Collection<Developer>() { d4, d1 }
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
                GameDevelopers = new Collection<Developer>() { d3, d4 }
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
                GameDevelopers = new Collection<Developer>() { d3 }
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

            var games = new List<Game>() { g1, g2, g3, g4, g5, g6, g7, g8, g9, g10, g11, g12, g13, g14, g15, g16, g17, g18, g19, g20, g21, g22, g23 };
            foreach (var game in games)
            {
                context.Games.AddOrUpdate(c => c.Title, game);
            }
            #endregion

            #region Seed UserGameRatings

            var user1 = context.Users.First(u => u.UserName == "GameMaster");
            var user2 = context.Users.First(u => u.UserName == "mikedalpha");
            var user3 = context.Users.First(u => u.UserName == "kwstaskor");
            var user4 = context.Users.First(u => u.UserName == "konstantinala");

            var ugr1 = new UserGameRatings
            {
                ApplicationUser = user1,
                Game = g1,
                Rating = 4
            };

            var ugr2 = new UserGameRatings
            {
                ApplicationUser = user2,
                Game = g1,
                Rating = 3
            };

            var ugr3 = new UserGameRatings
            {
                ApplicationUser = user3,
                Game = g1,
                Rating = 5
            };

            var ugr4 = new UserGameRatings
            {
                ApplicationUser = user1,
                Game = g2,
                Rating = 5
            };

            var ugr5 = new UserGameRatings
            {
                ApplicationUser = user2,
                Game = g2,
                Rating = 5
            };

            var ugr6 = new UserGameRatings
            {
                ApplicationUser = user4,
                Game = g2,
                Rating = 5
            };

            var ugr7 = new UserGameRatings
            {
                ApplicationUser = user4,
                Game = g4,
                Rating = 5
            };

            var ugr8 = new UserGameRatings
            {
                ApplicationUser = user3,
                Game = g4,
                Rating = 4
            };

            var ugr9 = new UserGameRatings
            {
                ApplicationUser = user2,
                Game = g5,
                Rating = 5
            };

            var ugr10 = new UserGameRatings
            {
                ApplicationUser = user3,
                Game = g5,
                Rating = 4
            };

            var ugr11 = new UserGameRatings
            {
                ApplicationUser = user4,
                Game = g5,
                Rating = 5
            };

            var ugr12 = new UserGameRatings
            {
                ApplicationUser = user1,
                Game = g6,
                Rating = 1
            }; 
            
            
            var ugr13 = new UserGameRatings
            {
                ApplicationUser = user2,
                Game = g6,
                Rating = 5
            };

            var ugr14 = new UserGameRatings
            {
                ApplicationUser = user4,
                Game = g6,
                Rating = 4
            };

            var userGameRatings = new List<UserGameRatings> { ugr1, ugr2, ugr3, ugr4, ugr5, ugr6, ugr7, ugr8, ugr9, ugr10, ugr11, ugr12, ugr13 };
            foreach (var userGameRating in userGameRatings)
            {
                context.UserGameRatings.AddOrUpdate(g => new { g.ApplicationUserId, g.GameId, g.Rating }, userGameRating);
            }

            #endregion

            context.SaveChanges();
        }
    }
}
