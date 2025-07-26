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
    public class RecipesController : ControllerBase
    {
        private readonly RecipeService _service;

        public RecipesController(RecipeService service)
        {
            _service = service;
        }
        /// <summary>
        /// Récupère toutes les recettes.
        /// </summary>
        /// <returns>liste des recettes</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll() => Ok(_service.GetAll());
        /// <summary>
        /// Récupère une recette par son ID.
        /// </summary>
        /// <param name="id">ID de la recette</param>
        /// <returns>la recette</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var recipe = _service.GetById(id);
            return recipe == null ? NotFound() : Ok(recipe);
        }
        /// <summary>
        /// Ajoute une nouvelle recette.
        /// </summary>
        /// <param name="dto">Données de la recette à ajouter</param>
        /// <returns>la recette créée</returns>
        /// <response code="201">Recette créée avec succès.</response>
        /// <response code="401">Non autorisé.</response>
        [HttpPost]
        [Authorize] 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Add([FromBody] CreateRecipeDto dto)
        {
            var created = _service.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        /// <summary>
        /// supprimer une recette.
        /// </summary>
        /// <param name="id">ID de la recette à supprimer</param>
        /// <returns>204 No Content si la suppression a réussi</returns>
        /// <response code="204">Recette supprimée.</response>
        /// <response code="401">Non autorisé.</response>
        /// <response code="404">Recette non trouvée.</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
    
        public IActionResult Delete(int id)
        {
            var ok = _service.Delete(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
