using System.Collections.Generic;
using System.Linq;
using PracticumRevisitedLibrary.Model;

namespace PracticumRevisitedLibrary.Processors
{   
    public class DuplicateNotAllowedProcessor : IProcessor
    {  
        public void Process(ProcessableOrder processable)
        {
            //sort the order first - entrée, side, drink, desert
            var sortedOrder = (from o in processable.Order
                               orderby o.DishType
                               select o).ToList();
           
            foreach (var dish in sortedOrder)
            {   
                var selectedDishTypeItemCount = GetSelectedDishTypeItemCount(processable.SelectedDishTypeItemCounts, dish);
                if (selectedDishTypeItemCount != null)
                {
                    var isDuplicateNotAllowedError = IsDuplicateNotAllowedError(dish, selectedDishTypeItemCount);

                    if (isDuplicateNotAllowedError)
                    {
                        processable.IsDuplicateNotAllowedError = true;
                        processable.DuplicateErrorDishType = selectedDishTypeItemCount.DishType;
                        break;
                    }  
                }
            }
        }

        private SelectedDishTypeItemCount GetSelectedDishTypeItemCount(IEnumerable<SelectedDishTypeItemCount> selectedDishTypeItemCounts, Dish dish)
        {
            var selectedDishTypeItemCount = (from s in selectedDishTypeItemCounts
                                             where s.DishType == (int)dish.DishType
                                             select s).FirstOrDefault();
            return selectedDishTypeItemCount;
        }

        private bool IsDuplicateNotAllowedError(Dish dish, SelectedDishTypeItemCount selectedDishTypeItemCount)
        {
            return (selectedDishTypeItemCount.SelectedItemCount > 1 && dish.DuplicateAllowed == false);
        }
    }
}
