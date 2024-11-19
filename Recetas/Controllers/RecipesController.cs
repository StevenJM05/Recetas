﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recetas.Data;
using Recetas.Models;

namespace Recetas.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecipesController : ControllerBase
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

		[HttpPost]
		public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
		{
			_context.Recipes.Add(recipe);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetRecipe), new {id = recipe.Id}, recipe);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
		{
			if(id != recipe.Id)
			{
				return BadRequest();
			}

			_context.Entry(recipe).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}catch(DbUpdateConcurrencyException)
			{
				if(!_context.Recipes.Any(e => e.Id == id))
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
		public async Task<IActionResult> DeleteRecipe(int id)
		{
			var recipe = await _context.Recipes.FindAsync(id);
			if (recipe == null)
			{
				return NotFound();
			}

			_context.Recipes.Remove(recipe);
			await _context.SaveChangesAsync();

			return NoContent();
		}

	}
}
