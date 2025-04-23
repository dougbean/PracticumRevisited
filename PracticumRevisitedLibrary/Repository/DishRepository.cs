using System.Collections.Generic;
using System.Linq;
using PracticumRevisitedLibrary.Model;

namespace PracticumRevisitedLibrary.Repository
{   
    public class DishRepository : IRepository
    {
        private IEnumerable<Dish> _dishes = new List<Dish>();

        public DishRepository()
        {
            if (!this._dishes.Any())
            { 
                _dishes = CreateMenu();
            }
        }

        private IEnumerable<Dish> CreateMenu()
        {
            var dishes = new List<Dish>();

            var steak = new Dish() { Label = "steak", DishType = DishType.entre, TimeOfDay = TimeOfDay.night, DuplicateAllowed = false };
            var eggs = new Dish() { Label = "eggs", DishType = DishType.entre, TimeOfDay = TimeOfDay.morning, DuplicateAllowed = false };
            var toast = new Dish() { Label = "toast", DishType = DishType.side, TimeOfDay = TimeOfDay.morning, DuplicateAllowed = false };
            var potatoes = new Dish() { Label = "potato", DishType = DishType.side, TimeOfDay = TimeOfDay.night, DuplicateAllowed = true };
            var coffee = new Dish() { Label = "coffee", DishType = DishType.drink, TimeOfDay = TimeOfDay.morning, DuplicateAllowed = true };
            var wine = new Dish() { Label = "wine", DishType = DishType.drink, TimeOfDay = TimeOfDay.night, DuplicateAllowed = false };
            var cake = new Dish() { Label = "cake", DishType = DishType.dessert, TimeOfDay = TimeOfDay.night, DuplicateAllowed = false };

            dishes.Add(steak);
            dishes.Add(eggs);
            dishes.Add(toast);
            dishes.Add(potatoes);
            dishes.Add(coffee);
            dishes.Add(wine);
            dishes.Add(cake);

            return dishes;
        }
      
        public Dish GetSelectedItem(int item, int timeOfDay)
        {
            var model = (from dish in _dishes
                         where (int)dish.TimeOfDay == timeOfDay
                         && (int)dish.DishType == item
                         select dish).FirstOrDefault();
            return model;
        }        
    }
}