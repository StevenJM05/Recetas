using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recetas.Data;
using Recetas.Models;

namespace Recetas.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly RecipeDBContext _context;

		public CategoryController(RecipeDBContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
		{
			return await _context.Categories
				.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Category>> GetCategory(int id)
		{
			var category = await _context.Categories
				.FirstOrDefaultAsync(c => c.Id == id);

			if (category == null)
			{
				return NotFound();
			}

			return category;
		}

		[HttpPost]
		public async Task<ActionResult<Category>> PostCategory(Category category)
		{
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetCategories), new {id = category.Id}, category);

		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutCategory(int id, Category category)
		{
			if(id != category.Id)
			{
				return BadRequest();
			}
			
			_context.Entry(category).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch(DbUpdateConcurrencyException)
			{
				if(!_context.Categories.Any(c => c.Id == id))
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
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category == null) 
			{ 
				return NotFound();
			}
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();

			return NoContent();
		}


	}
}
