using System.ComponentModel.DataAnnotations;
using GroupProject.Entities;

namespace GroupProject.WebApp.Models.HomeViewModel
{
    public class PricingPlanViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public Plan? SubscribePlan { get; set; }
    }
}