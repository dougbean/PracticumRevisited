using System;
using System.Collections.Generic;
using System.Linq;
using PracticumRevisitedLibrary.Model;

namespace PracticumRevisitedLibrary.Processors
{   
    public class DishTypeItemCountProcessor : IProcessor
    {  
        public void Process(ProcessableOrder processable)
        {
            List<int> dishTypes = GetListOfEnumValues<DishType>();
            processable.SelectedDishTypeItemCounts = GetSelectedDishTypeItemCounts(processable.ParsedOrder, dishTypes);
        }

        public List<int> GetListOfEnumValues<T>()
        {
            var enumValues = new List<int>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                enumValues.Add((int)value);
            }
            return enumValues;
        }

        private IEnumerable<SelectedDishTypeItemCount> GetSelectedDishTypeItemCounts(ParsedOrder parsedOrder, List<int> dishTypes)
        {
            var selectedDishTypeItemCounts = new List<SelectedDishTypeItemCount>();

            foreach (var dishType in dishTypes)
            {
                var itemCount = GetItemCount(parsedOrder.SelectedDishTypes, dishType);
                var selectedDishTypeItemCount = new SelectedDishTypeItemCount()
                {
                    DishType = dishType,
                    SelectedItemCount = itemCount
                };
                selectedDishTypeItemCounts.Add(selectedDishTypeItemCount);
            }
            return selectedDishTypeItemCounts;
        }

        private int GetItemCount(IEnumerable<int> selectedDishTypes, int dishType)
        {   
            int selectedItemCount = (from item in selectedDishTypes
                                      where item == dishType
                                      select item).Count();
            return selectedItemCount;
        }
    }
}
