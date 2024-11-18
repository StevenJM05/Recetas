using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recetas.Data;
using Recetas.Models;

namespace Recetas.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecipesController :ControllerBase
	{
		private readonly RecipeDBContext _context;

		public RecipesController(RecipeDBContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
		{
			return await _context.Recipes
				.Include(r => r.Category)
				.Include(r => r.Ingredients)
				.Include(r => r.Steps)
				.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Recipe>> GetRecipe(int id)
		{
			var recipe = await _context.Recipes
				.Include(r => r.Category)
				.Include(r => r.Ingredients)
				.Include(r => r.Steps)
				.FirstOrDefaultAsync(r => r.Id == id);

			if (recipe == null)
			{
				return NotFound();
			}

			return recipe;
		}

	}
}
