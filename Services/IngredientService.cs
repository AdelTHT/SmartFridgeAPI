using SmartFridgeApi.Models;
using SmartFridgeApi.DTOs;
using SmartFridgeApi.Data;

namespace SmartFridgeApi.Services
{
    public class IngredientService
    {
        private readonly SmartFridgeDbContext _context;

        public IngredientService(SmartFridgeDbContext context)
        {
            _context = context;
        }

        public List<IngredientDto> GetAll()
        {
            return _context.Ingredients
                .Select(i => new IngredientDto { Id = i.Id, Name = i.Name })
                .ToList();
        }

        public IngredientDto Add(CreateIngredientDto dto, int userId)
        {
            var ingredient = new Ingredient { Name = dto.Name, UserId = userId };
            _context.Ingredients.Add(ingredient);
            _context.SaveChanges();
            return new IngredientDto { Id = ingredient.Id, Name = ingredient.Name };
        }

        public bool Delete(int id, int userId)
        {
            var ingredient = _context.Ingredients.FirstOrDefault(i => i.Id == id && i.UserId == userId);
            if (ingredient == null) return false;
            _context.Ingredients.Remove(ingredient);
            _context.SaveChanges();
            return true;
        }
        public bool Update(int id, UpdateIngredientDto dto, int userId)
        {
            var ingredient = _context.Ingredients.FirstOrDefault(i => i.Id == id && i.User.Id == userId);
            if (ingredient == null) return false;
            if (string.IsNullOrWhiteSpace(dto.Name)) return false;

            ingredient.Name = dto.Name;
            _context.SaveChanges();
            return true;
        }

    }
}
