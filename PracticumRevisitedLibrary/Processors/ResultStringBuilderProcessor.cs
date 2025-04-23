using System.Collections.Generic;
using System.Linq;
using System.Text;
using PracticumRevisitedLibrary.Model;

namespace PracticumRevisitedLibrary.Processors
{
    public class ResultStringBuilderProcessor : IProcessor
    {   
        public void Process(ProcessableOrder processable)
        {
            List<Dish> dishes = GetListWithoutDuplicates(processable);
            processable.Result = GetPrintString(processable, dishes);
        }

        private List<Dish> GetListWithoutDuplicates(ProcessableOrder processable)
        {
            var dishes = new List<Dish>();
            foreach (var item in processable.Order)
            {  
                var alreadyAddedList = dishes.Where(x => x.DishType == item.DishType).ToList();
                if (!alreadyAddedList.Any())
                {
                    var dish = new Dish()
                    {
                        DishType = item.DishType,
                        DuplicateAllowed = item.DuplicateAllowed,
                        Label = item.Label,
                        TimeOfDay = item.TimeOfDay
                    };
                    dishes.Add(dish);    
                }
            }
            return dishes;
        }
      
        private string GetPrintString(ProcessableOrder processable, List<Dish> dishes)
        {   
            var builder = new StringBuilder();
            int increment = 1;

            foreach (var dish in dishes)
            {
                builder.Append(dish.Label);
                
                var selectedDishTypeItemCount = processable.SelectedDishTypeItemCounts.FirstOrDefault((x => x.DishType == (int)dish.DishType));
                if (selectedDishTypeItemCount != null)
                {
                    bool currentDishAllowableDuplicate = IsCurrentDishAllowableDuplicate(selectedDishTypeItemCount, dish);
                    if (currentDishAllowableDuplicate)
                    {
                        string extra = string.Format("(x{0})", selectedDishTypeItemCount.SelectedItemCount);
                        builder.Append(extra);
                    }
                   
                    bool duplicateErrorOnCurrentDish = IsDuplicateErrorOnCurrentDish(processable, dish);
                    if (duplicateErrorOnCurrentDish)
                    {
                        builder.Append(", error");
                        break;
                    }
                    
                    if (increment < dishes.Count() && (dish.Label != "error"))
                    {
                        builder.Append(", ");
                    }
                    increment++;

                    if (dish.Label == "error")
                    {
                        break;
                    }
                }
            }

            return builder.ToString();
        }

        public bool IsCurrentDishAllowableDuplicate(SelectedDishTypeItemCount selectedDishTypeItemCount, Dish dish)
        {
            return (selectedDishTypeItemCount != null && 
                    selectedDishTypeItemCount.SelectedItemCount > 1 &&
                    dish.DuplicateAllowed);
        }

        public bool IsDuplicateErrorOnCurrentDish(ProcessableOrder processable, Dish dish)
        {
            return (processable.IsDuplicateNotAllowedError && processable.DuplicateErrorDishType == (int)dish.DishType);
        }
    }
}
