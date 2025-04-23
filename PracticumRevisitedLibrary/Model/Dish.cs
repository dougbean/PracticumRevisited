
namespace PracticumRevisitedLibrary.Model
{
    public class Dish
    {
        public string Label { get; set; }
        public DishType DishType { get; set; }
        public TimeOfDay TimeOfDay { get; set; }
		public bool DuplicateAllowed {get; set;}
    }
}