using PracticumRevisitedLibrary.Model;

namespace PracticumRevisitedLibrary.Repository
{
    public interface IRepository
    {  
        Dish GetSelectedItem(int item, int timeOfDay);
    }
}
