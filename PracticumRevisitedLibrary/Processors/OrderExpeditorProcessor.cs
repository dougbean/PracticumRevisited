using System.Collections.Generic;
using System.Linq;
using PracticumRevisitedLibrary.Model;
using PracticumRevisitedLibrary.Repository;

namespace PracticumRevisitedLibrary.Processors
{  
    public class OrderExpeditorProcessor : IProcessor
    {
        private IRepository _repository;

        public OrderExpeditorProcessor(IRepository repository)
        {
            _repository = repository;
        }

        public void Process(ProcessableOrder processable)
        {
            var order = new List<Dish>();

            //sort the order first - entrée, side, drink, desert
            var sortedOrder = (from dishType in processable.ParsedOrder.SelectedDishTypes
                               orderby dishType
                               select dishType).ToList();
            
            foreach (var item in sortedOrder)
            {
                if (_repository != null)
                {
                    var dish = _repository.GetSelectedItem(item, (int)processable.ParsedOrder.TimeOfDay);

                    if (dish != null)
                    { 
                        order.Add(dish);
                    }
                    else
                    {
                        var error = new Dish() { Label = "error", DishType = DishType.invalid};
                        order.Add(error);
                    }
                }
            }
            processable.Order = order;
        }
    }
}
