using Microsoft.EntityFrameworkCore;
using Recetas.Models;

namespace Recetas.Data
{
	public class RecipeDBContext : DbContext
	{
		public RecipeDBContext(DbContextOptions<RecipeDBContext> options) : base(options) { }

		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<Ingredient> Ingredients { get; set;}
		public DbSet<Step> Steps { get; set; }
		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.HasMany(c => c.Recipes)
				.WithOne(r => r.Category)
				.HasForeignKey(r => r.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Ingredient>()
				.HasOne(i => i.Recipe)
				.WithMany(r => r.Ingredients)
				.HasForeignKey(i => i.RecipeId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Step>()
				.HasOne(s => s.Recipe)
				.WithMany(r => r.Steps)
				.HasForeignKey(s => s.RecipeId)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}
