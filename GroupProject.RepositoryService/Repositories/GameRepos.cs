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

        public void AttachDevs(Game game)
        {
            
            Context.Entry(game).Collection("GameDevelopers").Load();
        }

        public void AttachCategories(Game game)
        {
            Context.Entry(game).Collection("GameCategories").Load();
        }

        public void AssignGameCategories(Game game, Category[] categories)
        {
            if (categories is null) return;

            game.GameCategories.Clear();

            AttachCategories(game);

            foreach (var cat in categories)
            {
                var category = DbContext.Categories.Find(cat.CategoryId);
                if (!(category is null))
                {
                    game.GameCategories.Add(category);
                }
            }

            DbContext.SaveChanges();
        }

        public void AssignGameDevelopers(Game game, Developer[] developers)
        {
            if (developers is null) return;

            game.GameDevelopers.Clear();

            AttachDevs(game);

            foreach (var dev in developers)
            {
                var developer = DbContext.Developers.Find(dev.DeveloperId);
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


    }
}
