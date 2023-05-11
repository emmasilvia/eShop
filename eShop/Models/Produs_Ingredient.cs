namespace eShop.Models
{
    public class Produs_Ingredient
    {
        public int ProdusId { get; set; }
        public Produs Produs { get; set; }
        public int IngredientId { get; set; }
        
        public Ingredient Ingredient { get; set; }
    }
}
