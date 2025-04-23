using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticumRevisitedLibrary.Model;
using PracticumRevisitedLibrary.Repository;

namespace PracticumRevisitedUnitTest
{
    [TestClass]
    public class RepositoryUnitTest
    {
        private IRepository _repository = new DishRepository();
        
        [TestMethod]
        public void NullShouldBeReturnedForBreakfastDesert()
        {   
            //arrange
            int selectedDishType = 4; //not a valid dish type for morning
           
            //act
            var dish = _repository.GetSelectedItem(selectedDishType, (int)TimeOfDay.morning);
            
            //assert
            Assert.IsNull(dish);
        }

        [TestMethod]
        public void GetSelectedItem_should_return_coffee()
        {
            //arrange
            int selectedDishType = 3; 

            //act
            var dish = _repository.GetSelectedItem(selectedDishType, (int)TimeOfDay.morning);

            string expected = "coffee";

            //assert
            Assert.AreEqual(expected, dish.Label);
        }

        [TestMethod]
        public void GetSelectedItem_should_return_dessert()
        {
            //arrange
            int selectedDishType = 4;

            //act
            var dish = _repository.GetSelectedItem(selectedDishType, (int)TimeOfDay.night);

            string expected = "cake";

            //assert
            Assert.AreEqual(expected, dish.Label);
        }
    }
}
