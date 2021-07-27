using GroupProject.Entities;
using System.Data.Entity.ModelConfiguration;
using System.Security.Permissions;

namespace GroupProject.Database.EntitiesConfiguration
{
    class GameConfig: EntityTypeConfiguration<Game>
    {
        internal GameConfig()
        {
            Property(g => g.Title)
                .IsRequired()
                .HasMaxLength(50);

            Property(g => g.ReleaseDate)
               .IsRequired()
               .HasColumnType("date");

            Property(g => g.Description)
                .IsRequired()
                .HasMaxLength(10000);

            Property(g => g.Photo)
                 .IsRequired();

            Property(g => g.IsEarlyAccess)
                .IsOptional();
            
            //Game-Category many to many relation
            HasMany(g => g.GameCategories)
                 .WithMany(c => c.CategoryGames)
                 .Map(map =>
                 {
                     map.ToTable("GamesCategories");
                     map.MapLeftKey("GameId");
                     map.MapRightKey("CategoryId");
                 });

            //Game-developer many to many 
            HasMany(g => g.GameDevelopers)
                 .WithMany(c => c.DeveloperGames)
                 .Map(map =>
                 {
                     map.ToTable("GamesDevelopers");
                     map.MapLeftKey("GameId");
                     map.MapRightKey("DeveloperId");
                 });

        }
    }
}
