using System.ComponentModel.DataAnnotations;

namespace eShop.Models
{
    public class Adresa
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Strada")]
        public string Strada { get; set; }

        [Display(Name = "Oras")]
        public string Oras { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
