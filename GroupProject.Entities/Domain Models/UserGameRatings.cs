using System.ComponentModel.DataAnnotations.Schema;

namespace GroupProject.Entities.Domain_Models
{
    public class UserGameRatings
    {
        public int UserGameRatingsId { get; set; }
        public int Rating { get; set; }

        //Navigation Properties
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
    }
}
