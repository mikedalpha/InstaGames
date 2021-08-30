using System.Data.Entity.ModelConfiguration;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.Database.EntitiesConfiguration
{
    internal class UserGameRatingConfig : EntityTypeConfiguration<UserGameRatings>
    {

        public UserGameRatingConfig()
        {
            HasRequired(ug => ug.Game)
                .WithMany(g => g.UserGameRatings)
                .Map(m => m.MapKey("GameId"))
                .WillCascadeOnDelete(true);

            HasRequired(ug => ug.ApplicationUser)
                .WithMany(a => a.UserGameRatings)
                .Map(m=>m.MapKey("UserId"))
                .WillCascadeOnDelete(true);

            Property(ug => ug.Rating).IsRequired();
        }
    }
}
