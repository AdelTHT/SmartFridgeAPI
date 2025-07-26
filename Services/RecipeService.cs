using Microsoft.EntityFrameworkCore;
using SmartFridgeApi.Models;
using SmartFridgeApi.DTOs;
using SmartFridgeApi.Data;

namespace SmartFridgeApi.Services
{
    public class RecipeService
    {
        private readonly SmartFridgeDbContext _context;

        public RecipeService(SmartFridgeDbContext context)
        {
            _context = context;
        }

        public List<RecipeDto> GetAll()
        {
            return _context.Recipes
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .Select(r => new RecipeDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Ingredients = r.RecipeIngredients.Select(ri => new IngredientDto
                    {
                        Id = ri.Ingredient.Id,
                        Name = ri.Ingredient.Name
                    }).ToList()
                }).ToList();
        }

        public RecipeDto? GetById(int id)
        {
            return _context.Recipes
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .Where(r => r.Id == id)
                .Select(r => new RecipeDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Ingredients = r.RecipeIngredients.Select(ri => new IngredientDto
                    {
                        Id = ri.Ingredient.Id,
                        Name = ri.Ingredient.Name
                    }).ToList()
                }).FirstOrDefault();
        }

        public RecipeDto Add(CreateRecipeDto dto)
        {
            var recipe = new Recipe
            {
                Title = dto.Title,
                Description = dto.Description,
                RecipeIngredients = dto.IngredientIds
                    .Select(id => new RecipeIngredient { IngredientId = id })
                    .ToList()
            };
            _context.Recipes.Add(recipe);
            _context.SaveChanges();

            // Recharger la recette complète pour bien avoir les noms d'ingrédients
            recipe = _context.Recipes
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .First(r => r.Id == recipe.Id);

            return new RecipeDto
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Ingredients = recipe.RecipeIngredients.Select(ri => new IngredientDto
                {
                    Id = ri.Ingredient.Id,
                    Name = ri.Ingredient.Name
                }).ToList()
            };
        }

        public bool Delete(int id)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            if (recipe == null) return false;
            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
            return true;
        }
    }
}
