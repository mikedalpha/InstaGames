using GroupProject.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GroupProject.Database.EntitiesConfiguration
{
    class CategoryConfig : EntityTypeConfiguration<Category>
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
