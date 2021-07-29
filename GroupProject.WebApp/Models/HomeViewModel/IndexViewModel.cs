using System.Collections.Generic;
using System.Linq;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.WebApp.Models.HomeViewModel
{
    public class IndexViewModel
    {
        private List<Game> allGames;

        public IndexViewModel(List<Game> games)
        {
            allGames = games;
        }

        public List<Game> Games
        {
            get { return allGames; }
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

        public Game RandomGame
        {
            get
            {
                return allGames.Find(g=>g.GameId==4);
            }
        }

        public List<Game> Games3
        {
            get { return Games.Where(g => g.Pegi == "/Content/images/Pegi/3.jpg").OrderBy(g => g.ReleaseDate).ToList(); }
        }

        public List<Game> Games7
        {
            get { return Games.Where(g => g.Pegi == "/Content/images/Pegi/7.jpg").OrderBy(g => g.ReleaseDate).ToList(); }
        }

        public List<Game> Games12
        {
            get { return Games.Where(g => g.Pegi == "/Content/images/Pegi/12.jpg").OrderBy(g => g.ReleaseDate).ToList(); }
        }

        public List<Game> Games16
        {
            get { return Games.Where(g => g.Pegi == "/Content/images/Pegi/16.jpg").OrderBy(g => g.ReleaseDate).ToList(); }
        }

        public List<Game> Games18
        {
            get { return Games.Where(g => g.Pegi == "/Content/images/Pegi/18.jpg").OrderBy(g => g.ReleaseDate).ToList(); }
        }
    }
} 