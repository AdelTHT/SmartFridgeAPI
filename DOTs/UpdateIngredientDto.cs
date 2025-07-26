using System.ComponentModel.DataAnnotations;

namespace SmartFridgeApi.DTOs
{
    public class UpdateIngredientDto
    {
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
