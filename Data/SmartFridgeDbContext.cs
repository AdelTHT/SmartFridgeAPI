using Microsoft.EntityFrameworkCore;
using SmartFridgeApi.Models;

namespace SmartFridgeApi.Data
{
    public class SmartFridgeDbContext : DbContext
    {
        public SmartFridgeDbContext(DbContextOptions<SmartFridgeDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });
        }
    }
}
