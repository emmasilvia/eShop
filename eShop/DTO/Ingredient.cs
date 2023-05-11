using System.ComponentModel.DataAnnotations;

namespace eShop.DTO
{
    public class Ingredient
    {

        public int Id { get; set; }

        [Display(Name = "Denumire")]
        [Required(ErrorMessage = "Denumirea este obligatorie")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Denumirea trebuie sa fie intre 3-50 caractere")]
        public string Denumire { get; set; }
    }
}
