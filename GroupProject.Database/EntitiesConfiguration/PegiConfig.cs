using System.Data.Entity.ModelConfiguration;
using System.Threading;
using GroupProject.Entities.Domain_Models;

namespace GroupProject.Database.EntitiesConfiguration
{
    internal class PegiConfig :EntityTypeConfiguration<Pegi>
    {
        internal PegiConfig()
        {

            ToTable("Pegi");

            Property(p => p.PegiAge)
                .IsRequired()
                .HasColumnType("tinyint");

            Property(p => p.PegiPhoto)
                .IsRequired();
        }
    }
}
