using System.Collections.Generic;
using eShop.Models;

namespace eShop.DTO
{
    public class DropDownProdusNou
    {

        public DropDownProdusNou()
        {
            listaRestaurante = new List<Models.Restaurant>();
            listaIngrediente = new List<Models.Ingredient>();
        }

        public List<Models.Restaurant> listaRestaurante { get; set; }
        public List<Models.Ingredient> listaIngrediente { get; set; }
    }
}
