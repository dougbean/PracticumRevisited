using System.Collections.Generic;

namespace PracticumRevisitedLibrary.Model
{   
    public class ParsedOrder
    {  
        public TimeOfDay TimeOfDay { get; set; }
        public List<int> SelectedDishTypes { get; set; }
    }
}
