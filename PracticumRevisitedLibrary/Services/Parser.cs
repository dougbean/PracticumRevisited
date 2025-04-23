using System;
using System.Collections.Generic;
using System.Linq;
using PracticumRevisitedLibrary.Model;

namespace PracticumRevisitedLibrary.Services
{   
    public class Parser : IParser
    {  
        public ParsedOrder ParseInput(string input)
        {
            string[] inputs = SplitInputLineByWhiteSpace(input);

            var parsedOrder = new ParsedOrder(); 
            parsedOrder.SelectedDishTypes = new List<int>();

            string timeOfDayString = inputs[0];
            if (inputs.Length == 1)
            {   
                throw new Exception("order is not valid, selected dish types are not provided.");
            }

            if (inputs.Length == 2)
            {
                ParseTimeOfDay(timeOfDayString, parsedOrder);
                ParseSelectedDishType(inputs[1], parsedOrder);
            }

            if (inputs.Length > 2)
            { 
                ParseTimeOfDay(timeOfDayString, parsedOrder);
                
                List<string> selectedDishTypes = inputs.ToList();
                selectedDishTypes.RemoveAt(0);
                
                foreach (var item in selectedDishTypes)
                {   
                    ParseSelectedDishType(item, parsedOrder);
                }
            }

            return parsedOrder;
        }

        private void ParseTimeOfDay(string timeOfDayString, ParsedOrder parsedOrder)
        {
            TimeOfDay timeOfDay;
            string cleanedTimeOfDayString = timeOfDayString.Replace(",", "");

            bool isSuccess = Enum.TryParse(cleanedTimeOfDayString.ToLower(), out timeOfDay);
            if (isSuccess)
            {
                parsedOrder.TimeOfDay = timeOfDay;
            }
            else
            {
                throw new Exception("Time of Day selection is not valid.");
            }
        }

        private void ParseSelectedDishType(string item, ParsedOrder parsedOrder)
        {
            int selectedDishType = 0;
            string cleanedItem = item.Replace(",", "");

            bool isSuccess = Int32.TryParse(cleanedItem, out selectedDishType);
            if (isSuccess)
            {
                parsedOrder.SelectedDishTypes.Add(selectedDishType);
            }
            else
            {   
                throw new Exception("selected dish type is not an integer");
            }
        }

        private string[] SplitInputLineByWhiteSpace(string input)
        {
            string[] inputs = input.Split(' ');
            return inputs;
        }
    }
}
