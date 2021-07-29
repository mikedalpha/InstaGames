using System.Data.Entity.ModelConfiguration;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.Database.EntitiesConfiguration
{
    internal class CategoryConfig : EntityTypeConfiguration<Category>
    {
        internal CategoryConfig()
        {

            Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(50);

            Property(c => c.Description)
                .HasMaxLength(10000);

        }
    }
}
