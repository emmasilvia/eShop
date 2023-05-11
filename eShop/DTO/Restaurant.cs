using System.ComponentModel.DataAnnotations;

namespace eShop.DTO
{
    public class Restaurant
    {
        public int Id { get; set; }


        [Display(Name = "Nume Restaurante")]
        [Required(ErrorMessage = "Numele restaurantului este obligatorie")]
        public string Nume { get; set; }

        [Display(Name = "Imagine Restaurante")]
        [Required(ErrorMessage = "Imaginea pt restaurant este obligatorie")]
        public string Poza { get; set; }

        [Display(Name = "Descriere")]
        [Required(ErrorMessage = "Descrierea restaurantului este obligatorie")]
        public string Descriere { get; set; }

        public Adresa Adresa { get; set; }
    }
}
