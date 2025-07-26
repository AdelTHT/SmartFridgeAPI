namespace SmartFridgeApi.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; } // pour séparer les ingrédients par utilisateur
        public User User { get; set; }
    }
}