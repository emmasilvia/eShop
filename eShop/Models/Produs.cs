using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.Models
{
    public class Produs
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Denumire produs")]
        public string Denumire { get; set; }

        [Display(Name = "Pret produs")]
        public double Pret { get; set; }

        [Display(Name = "Descriere produs")]
        public string Descriere { get; set; }

        [Display(Name = "Imaginea produsului")]
        public string Imagine { get; set; }

        public List<Produs_Ingredient> listaProduse_Ingrediente { get; set; }

        [Display(Name = "Restaurante")]
        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; }

    }
}
