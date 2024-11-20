using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recetas.Data;
using Recetas.Models;

namespace Recetas.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IngredientController : ControllerBase
	{
		private readonly RecipeDBContext _context;

		public IngredientController(RecipeDBContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
		{
			return await _context.Ingredients
				.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Ingredient>> GetIngredient(int id)
		{
			var ingredient = await _context.Ingredients
				.FirstOrDefaultAsync(i => i.Id == id); 
			if(ingredient == null)
			{
				return NotFound();
			}

			return ingredient;
		}

		[HttpPost]
		public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
		{
			_context.Ingredients.Add(ingredient);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetIngredients), new { id = ingredient.Id }, ingredient);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutIngredient(int id, Ingredient ingredient)
		{
			if(id != ingredient.Id)
			{
				return BadRequest();
			}

			_context.Entry(ingredient).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}catch(DbUpdateConcurrencyException)
			{
				if(!_context.Ingredients.Any(i => i.Id == id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}	
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteIngredient(int id)
		{
			var ingredient = await _context.Ingredients.FindAsync(id);

			if(ingredient == null)
			{
				return NotFound();
			}

			_context.Ingredients.Remove(ingredient);
			await _context.SaveChangesAsync();
			return NoContent();
		}
		
	}
}
