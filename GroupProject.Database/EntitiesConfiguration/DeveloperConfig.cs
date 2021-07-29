using System.Data.Entity.ModelConfiguration;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.Database.EntitiesConfiguration
{
    class DeveloperConfig : EntityTypeConfiguration<Developer>
    {
        internal DeveloperConfig()
        {
            
            Property(g => g.FirstName)
              .IsRequired()
              .HasMaxLength(50);

            Property(g => g.LastName)
              .IsRequired()
              .HasMaxLength(50);

        }
    }
}
