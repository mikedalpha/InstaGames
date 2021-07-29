using System.Collections.Generic;

namespace GroupProject.Entities.Domain_Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Game> CategoryGames { get; set; }

        public Category()
        {
            CategoryGames = new HashSet<Game>();
        }
    }
}