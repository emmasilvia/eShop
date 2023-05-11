using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eShop.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Denumire")]
        [Required(ErrorMessage = "Denumirea este obligatorie")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Denumirea trebuie sa fie intre 3-50 caractere")]
        public string Denumire { get; set; }

        public List<Produs_Ingredient> listaProduse_Ingrediente { get; set; }
    }
}
