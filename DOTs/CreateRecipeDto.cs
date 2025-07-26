using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartFridgeApi.DTOs
{
    public class CreateRecipeDto
    {
        [Required(ErrorMessage = "Le titre est obligatoire.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "La description est obligatoire.")]
        public string Description { get; set; } = string.Empty;

        public List<int> IngredientIds { get; set; } = new List<int>();
    }
}
