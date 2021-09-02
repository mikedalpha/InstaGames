namespace GroupProject.Entities.Domain_Models
{
    public class UserGameRatings
    {
        public int UserGameRatingsId { get; set; }

        public int Rating { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Game Game { get; set; }
    }
}
