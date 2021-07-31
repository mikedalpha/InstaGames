using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.Entities.Domain_Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string GameUrl { get; set; }
        public string Trailer { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float Rating { get; set; }
        public bool? IsEarlyAccess { get; set; }
        public bool IsReleased { get; set; }
        public Tag Tag { get; set; }
        public virtual Pegi Pegi { get; set; }
        public virtual ICollection<Category> GameCategories { get; set; }
        public virtual ICollection<Developer> GameDevelopers { get; set; }
        public virtual ICollection<ApplicationUser> Subscribers { get; set; }

        public Game()
        {
            GameCategories = new HashSet<Category>();
            GameDevelopers = new HashSet<Developer>();
            Subscribers = new HashSet<ApplicationUser>();
        }
    }
}
