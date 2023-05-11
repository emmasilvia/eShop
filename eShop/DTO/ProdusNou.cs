using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eShop.DTO
{
    public class ProdusNou
    {
        public int Id { get; set; }

        [Display(Name = "Denumire produs")]
        [Required(ErrorMessage = "Denumirea produsului este obligatorie")]
        public string Denumire { get; set; }

        [Display(Name = "Pret produs")]
        [Required(ErrorMessage = "Pretul produsului este obligatoriu")]
        public double Pret { get; set; }

        [Display(Name = "Descriere produs")]
        [Required(ErrorMessage = "Descrierea produsului este obligatorie")]
        public string Descriere { get; set; }

        [Display(Name = "Imaginea produsului")]
        [Required(ErrorMessage = "Imaginea produsului este obligatorie")]
        public string Imagine { get; set; }

        [Display(Name = "Selecteaza ingredientele")]
        [Required(ErrorMessage = "Ingredientele produsului sunt obligatorii")]
        public List<int> listaIdIngrediente { get; set; }


        [Display(Name = "Selecteaza un restaurant")]
        [Required(ErrorMessage = "Restaurantul este obligatoriu")]
        public int RestaurantId { get; set; }

    }
}
