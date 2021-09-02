using System.Collections.Generic;
using System.Linq;
using GroupProject.Entities;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.WebApp.Models.HomeViewModel
{
    public class IndexViewModel
    {
        private List<Game> allGames;
        private ApplicationUser appuser;
        private List<UserGameRatings> _ratedGame;

        public IndexViewModel(List<Game> games , ApplicationUser user, List<UserGameRatings> ratedGame)
        {
            allGames = games;
            appuser = user;
            _ratedGame = ratedGame;
        }

        public IndexViewModel(ApplicationUser user)
        {
            appuser = user;
        }

        public List<Game> Games
        {
            get { return allGames; }
        }

        public List<Game> MyList
        {
            get { return appuser.UserList.ToList(); }
        }

        public List<UserGameRatings> RatedGames
        {
            get { return _ratedGame; }
        }

        public List<Game> SliderGames
        {
             get { return Games.OrderByDescending(g => g.Rating).Take(3).ToList(); }
        }

        public List<Game> LatestGames
        {
            get { return Games.Where(g => g.IsReleased == true).OrderByDescending(g => g.ReleaseDate).ToList(); }
        }

        public List<Game> UpcomingGames
        {
            get { return Games.Where(g => g.IsReleased == false).OrderBy(g => g.ReleaseDate).ToList(); }
        }

        public List<Game> TopTen
        {
            get
            {
                return Games.Where(g=>g.Rating > 0).OrderByDescending(g => g.Rating).Take(10).ToList();
            }
        }

        public List<Game> DevelopedByInstaGames
        {
            get { return Games.Where(g => g.GameDevelopers.Any(d => d.IsInstaGamesDev) && g.IsReleased).OrderByDescending(g=>g.Rating).ToList(); }
        }

        public Game RandomGame { get; set; }
    }
} 