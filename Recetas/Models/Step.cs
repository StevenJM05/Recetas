﻿namespace Recetas.Models
{
	public class Step
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; } = string.Empty;
		public int Order { get; set; }
		public int RecipeId { get; set; }
		public Recipe Recipe { get; set; }
	}
}
