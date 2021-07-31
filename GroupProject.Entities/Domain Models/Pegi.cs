using System.Collections.Generic;

namespace GroupProject.Entities.Domain_Models
{
    public class Pegi
    {
        public int PegiId { get; set; }
        public string PegiPhoto { get; set; }
        public byte PegiAge { get; set; }
        public virtual ICollection<Game> Games { get; set; }

        public Pegi()
        {
            Games = new HashSet<Game>();
        }
    }
}