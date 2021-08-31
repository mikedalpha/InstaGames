using System.Linq;
using GroupProject.Database;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.RepositoryService.Repositories
{
    public class GameRepos : Repository<Game>
    {
        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public GameRepos(ApplicationDbContext context) : base(context)
        {

        }

        public bool GameExists(int id)
        {
            return DbContext.Games.Count(g => g.GameId == id) > 0;
        }

        public void AssignGameCategories(Game game, int[] categories)
        {
            if (categories is null) return;

            ClearCategories(game);

            foreach (var cat in categories)
            {
                var category = DbContext.Categories.Find(cat);
                if (!(category is null))
                {
                    game.GameCategories.Add(category);
                }
            }

            DbContext.SaveChanges();
        }

        public void AssignGameDevelopers(Game game, int[] developers)
        {
            if (developers is null) return;

           ClearDevelopers(game);

            foreach (var dev in developers)
            {
                var developer = DbContext.Developers.Find(dev);
                if (!(developer is null))
                {
                    game.GameDevelopers.Add(developer);
                }
            }

            DbContext.SaveChanges();
        }

        public void AssignGamePegi(Game game, int pegiId)
        {
            var pegi = DbContext.Pegi.Find(pegiId);

            if (pegi != null)
            {
                game.Pegi = pegi;
            }

            DbContext.SaveChanges();
        }

        private void ClearDevelopers(Game game)
        {
            game.GameDevelopers.Clear();
            
        }
        private void ClearCategories(Game game)
        {
            game.GameCategories.Clear();
        }

        public void LoadCategories(Game game)
        {
            DbContext.Games.Attach(game);
            DbContext.Entry(game).Collection("GameCategories").Load();
        } 
        
        public void LoadDevelopers(Game game)
        {
            DbContext.Games.Attach(game);
            DbContext.Entry(game).Collection("GameDevelopers").Load();
        }
    }
}
