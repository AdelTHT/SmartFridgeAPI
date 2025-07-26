using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SmartFridgeApi.DTOs;
using SmartFridgeApi.Services;

namespace SmartFridgeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientService _ingredientService;

        public IngredientsController(IngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }
        /// <summary>
        /// Récupère tous les ingrédients.
        /// </summary>
        /// <response code="200">Liste des ingrédients récupérée avec succès.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {

            int userId = 1;
            var list = _ingredientService.GetAll();
            return Ok(list);
        }

        /// <summary>
        /// Ajoute un ingrédient.
        /// </summary>
        /// <param name="dto">L'ingrédient à ajouter.</param>
        /// <response code="201">Ingrédient ajouté avec succès.</response>
        /// <response code="401">Non autorisé.</response>
        [HttpPost]
        [Authorize] 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Add([FromBody] CreateIngredientDto dto)
        {
            int userId = 1;
            var ingredient = _ingredientService.Add(dto, userId);
            return CreatedAtAction(nameof(GetAll), new { id = ingredient.Id }, ingredient);
        }
        /// <summary>
        /// Met à jour un ingrédient.  
        /// /// </summary>
        /// <param name="id">ID de l'ingrédient à mettre à jour.</param> 
        /// <param name="dto">Nouvelles données de l'ingrédient.</param>
        /// <response code="204">Ingrédient mis à jour avec succès.</response
        /// <response code="401">Non autorisé.</response>
        /// <response code="404">Ingrédient non trouvé.</response>
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] UpdateIngredientDto dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var ok = _ingredientService.Update(id, dto, userId); 
            if (!ok) return NotFound();
            return NoContent();
        }


        /// <summary>
        /// Supprime un ingrédient.
        /// </summary>
        /// <param name="id">ID de l'ingrédient à supprimer.</param>
        /// <response code="204">Ingrédient supprimé.</response>
        /// <response code="401">Non autorisé.</response>
        /// <response code="404">Non trouvé.</response>
        [HttpDelete("{id}")]
        [Authorize] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            int userId = 1;
            var ok = _ingredientService.Delete(id, userId);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
