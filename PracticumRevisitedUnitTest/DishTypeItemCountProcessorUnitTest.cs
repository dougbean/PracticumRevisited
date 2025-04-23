using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticumRevisitedLibrary.Model;
using PracticumRevisitedLibrary.Processors;

namespace PracticumRevisitedUnitTest
{   
    [TestClass]
    public class DishTypeItemCountProcessorUnitTest
    {
        [TestMethod]
        public void ItemCountShouldBeTwo()
        {
            //arrange
            var parsedOrder = new ParsedOrder()
            {
                TimeOfDay = TimeOfDay.morning,
                SelectedDishTypes = new List<int>() { 1, 2, 2, 3 }
            };

            var processableOrder = new ProcessableOrder();
            processableOrder.ParsedOrder = parsedOrder;

            int expected = 2;

            //act
            var processor = new DishTypeItemCountProcessor();
            processor.Process(processableOrder);

            //assert
            int actual = (from v in processableOrder.SelectedDishTypeItemCounts
                          where v.DishType == 2
                          select v.SelectedItemCount).FirstOrDefault();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ItemCountShouldBeOne()
        {
            //arrange
            var parsedOrder = new ParsedOrder()
            {
                TimeOfDay = TimeOfDay.morning,
                SelectedDishTypes = new List<int>() { 1, 2, 2, 3 }
            };

            var processableOrder = new ProcessableOrder();
            processableOrder.ParsedOrder = parsedOrder;

            int expected = 1;

            //act
            var processor = new DishTypeItemCountProcessor();
            processor.Process(processableOrder);

            //assert
            int actual = (from v in processableOrder.SelectedDishTypeItemCounts
                          where v.DishType == 1
                          select v.SelectedItemCount).FirstOrDefault();

            Assert.AreEqual(expected, actual);
        }
    }
}
