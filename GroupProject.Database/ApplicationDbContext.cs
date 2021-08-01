using System.Data.Entity;
using GroupProject.Database.EntitiesConfiguration;
using GroupProject.Entities;
using GroupProject.Entities.Domain_Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GroupProject.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Pegi> Pegi { get; set; }

        public ApplicationDbContext(): base("GroupProject")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new GameConfig());
            modelBuilder.Configurations.Add(new DeveloperConfig());
            modelBuilder.Configurations.Add(new CategoryConfig());
            modelBuilder.Configurations.Add(new MessageConfig());
            modelBuilder.Configurations.Add(new PegiConfig());
        }

       
    }
}
