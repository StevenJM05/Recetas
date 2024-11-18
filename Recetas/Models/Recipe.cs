namespace Recetas.Models
{
	public class Recipe
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int PreparationTime { get; set; } 
		public int CookingTime { get; set; }
		public int Servings { get; set; }

		public List<Ingredient> Ingredients { get; set;} = new List<Ingredient>();
		public List<Step> Steps { get; set; } = new List<Step>();	

		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
