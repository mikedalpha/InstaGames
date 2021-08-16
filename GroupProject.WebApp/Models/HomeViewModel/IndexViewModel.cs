using System.Collections.Generic;
using System.Linq;
using GroupProject.Entities;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.WebApp.Models.HomeViewModel
{
    public class IndexViewModel
    {
        private List<Game> allGames;
        private Game _randomGame;
        private ApplicationUser appuser;

        public IndexViewModel(List<Game> games , Game randomGame, ApplicationUser user)
        {
            allGames = games;
            _randomGame = randomGame;
            appuser = user;
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

        public List<Game> TopFive
        {
            get { return Games.OrderByDescending(g => g.Rating).Take(5).ToList(); }
        }


        public List<Game> DevelopedByInstaGames
        {
            get { return Games.Where(g => g.GameDevelopers.Any(d => d.IsInstaGamesDev) && g.IsReleased).OrderByDescending(g=>g.Rating).ToList(); }
        }


        public Game RandomGame => _randomGame;

        
    }
} 