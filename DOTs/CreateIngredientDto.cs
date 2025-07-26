using System.ComponentModel.DataAnnotations;

namespace SmartFridgeApi.DTOs
{
    public class CreateIngredientDto
    {
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(50, ErrorMessage = "Le nom ne doit pas dépasser 50 caractères.")]
        public string Name { get; set; } = string.Empty;
    }
}
