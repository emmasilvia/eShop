using System.ComponentModel.DataAnnotations;

namespace eShop.DTO
{
    public class Adresa
    {

        public int Id { get; set; }

        [Display(Name = "Strada")]
        public string Strada { get; set; }

        [Display(Name = "Oras")]
        public string Oras { get; set; }
    }
}
