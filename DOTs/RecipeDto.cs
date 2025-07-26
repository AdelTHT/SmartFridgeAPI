namespace SmartFridgeApi.DTOs
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<IngredientDto> Ingredients { get; set; } = new();
    }
}
