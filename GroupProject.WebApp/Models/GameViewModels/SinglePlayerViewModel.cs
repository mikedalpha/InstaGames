using System.Collections.Generic;
using System.Linq;
using GroupProject.Entities;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.WebApp.Models.GameViewModels
{
    public class SinglePlayerViewModel
    {
        private List<Category> _allCategories;
        private List<Game> _allGames;
        private Game _selectedGame;
        private ApplicationUser appuser;
        private List<UserGameRatings> _ratedGame;

        public SinglePlayerViewModel(List<Game> allGames, Game selectedGame , List<Category> allCategories , ApplicationUser user, List<UserGameRatings> ratedGame)
        {
            _allGames = allGames;
            _selectedGame = selectedGame;
            _allCategories = allCategories;
            appuser = user;
            _ratedGame = ratedGame;
        }

        public List<Game> MyList
        {
            get { return appuser.UserList.ToList(); }
        }

        public List<UserGameRatings> RatedGames
        {
            get { return _ratedGame; }
        }

        public Game SelectedGame => _selectedGame;

        public IEnumerable<Game> GamesForChildren
        {
            get { return _allGames.Where(g => g.Pegi.PegiAge < 16).ToList(); }
        }

        public IEnumerable<Game> BestActionGames
        {
            get { return _allGames
                .Where(g =>g.GameCategories.Any(c=>c.Type == "Action"))
                .OrderByDescending(g => g.Rating).ToList(); }
        }

        public IEnumerable<Game> GamesWeRecommend
        {
            get
            {
                return _allGames.Where(g => g.IsReleased && g.GameDevelopers.Any(d => d.IsInstaGamesDev))
                    .OrderByDescending(g => g.Rating).ToList();
            }
        }  
        
        public IEnumerable<Game> GamesReleased2021
        {
            get
            {
                return _allGames.Where(g => g.IsReleased && g.ReleaseDate.Year == 2021).OrderByDescending(g => g.Rating)
                    .ToList();
            }
        }

        public IEnumerable<Category> Categories
        {
            get { return _allCategories; }
        }
    }
}