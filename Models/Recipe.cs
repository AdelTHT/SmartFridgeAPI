namespace SmartFridgeApi.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; } // liste des ingrédients utilisés
        public int? UserId { get; set; } // null si recette publique
        public User User { get; set; }
    }
}