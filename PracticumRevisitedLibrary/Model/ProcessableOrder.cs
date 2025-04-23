using System.Collections.Generic;

namespace PracticumRevisitedLibrary.Model
{   
    public class ProcessableOrder 
    {
        public IEnumerable<Dish> Order { get; set; } 
        public ParsedOrder ParsedOrder { get; set; }
        public IEnumerable<SelectedDishTypeItemCount> SelectedDishTypeItemCounts { get; set; }
        public bool IsDuplicateNotAllowedError { get; set; }
        public int DuplicateErrorDishType { get; set; }
        public string Result { get; set; }
    }
}
