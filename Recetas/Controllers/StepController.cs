using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recetas.Data;
using Recetas.Models;

namespace Recetas.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StepController : ControllerBase
	{
		private readonly RecipeDBContext _context;

		public StepController(RecipeDBContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Step>>> GetSteps()
		{
			return await _context.Steps
				.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Step>> GetStep(int id)
		{
			var step = await _context.Steps
				.FirstOrDefaultAsync(i => i.Id == id);
			if (step == null)
			{
				return NotFound();
			}

			return step;
		}

		[HttpPost]
		public async Task<ActionResult<Step>> PostStep(Step step)
		{
			_context.Steps.Add(step);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetSteps), new {id =  step.Id}, step);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutStep(int id, Step step)
		{
			if(id != step.Id)
			{
				return BadRequest();
			}

			_context.Entry(step).State = EntityState.Modified;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch(DbUpdateConcurrencyException)
			{
				if(!_context.Steps.Any(i => i.Id == id))
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
		public async Task<IActionResult> DeleteStep(int id)
		{
			var step = await _context.Steps.FindAsync(id);
			if(step == null)
			{
				return NotFound();
			}

			_context.Steps.Remove(step);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
