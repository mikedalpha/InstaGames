using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupProject.Entities.Domain_Models
{
    public class Developer
    {
        public int DeveloperId { get; set; }

        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Works at Instagames")]
        public bool IsInstaGamesDev { get; set; }

        public virtual ICollection<Game> DeveloperGames { get; set; }

        public Developer()
        {
            DeveloperGames = new HashSet<Game>();
        }

    }
}
