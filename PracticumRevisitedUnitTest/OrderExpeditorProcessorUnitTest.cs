using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticumRevisitedLibrary.Model;
using PracticumRevisitedLibrary.Repository;
using PracticumRevisitedLibrary.Processors;

namespace PracticumRevisitedUnitTest
{
    [TestClass]
    public class OrderExpeditorProcessorUnitTest
    {
        private static IRepository _repository = new DishRepository();
        
        [TestMethod]
        public void OrderShouldHaveThreeCupsOfCoffee()
        {
            //arrange
            var parsedOrder = new ParsedOrder()
            {
                TimeOfDay = TimeOfDay.morning,
                SelectedDishTypes = new List<int>() { 1, 2, 3, 3, 3 }
            };

            var processableOrder = new ProcessableOrder();
            processableOrder.ParsedOrder = parsedOrder;

            int expected = 3;

            //act
            var orderExpeditorProcessor = new OrderExpeditorProcessor(_repository); 
            orderExpeditorProcessor.Process(processableOrder);

            //assert
            int actual = (from v in processableOrder.Order
                          where (int) v.DishType == 3
                          select v).Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OrderShouldHaveTwoSidesOfPotatoes()
        {
            //arrange
            var parsedOrder = new ParsedOrder()
            {
                TimeOfDay = TimeOfDay.night,
                SelectedDishTypes = new List<int>() { 1, 2, 2, 3, 4 }
            };

            var processableOrder = new ProcessableOrder();
            processableOrder.ParsedOrder = parsedOrder;

            int expected = 2;

            //act
            var orderExpeditorProcessor = new OrderExpeditorProcessor(_repository); 
            orderExpeditorProcessor.Process(processableOrder);

            //assert
            int actual = (from v in processableOrder.Order
                          where (int)v.DishType == 2
                          select v).Count();

            Assert.AreEqual(expected, actual);
        }

        /* Though this is not a valid order, it is not the repsonsibility of the 
         OrderExpeditorVistor to validate it. */
        [TestMethod]
        public void OrderWillHaveTwoServingsOfCake()
        {
            //arrange
            var parsedOrder = new ParsedOrder()
            {
                TimeOfDay = TimeOfDay.night,
                SelectedDishTypes = new List<int>() { 1, 2, 3, 4, 4 }
            };

            var processableOrder = new ProcessableOrder();
            processableOrder.ParsedOrder = parsedOrder;

            int expected = 2;

            //act
            var orderExpeditorProcessor = new OrderExpeditorProcessor(_repository); 
            orderExpeditorProcessor.Process(processableOrder);

            //assert
            int actual = (from v in processableOrder.Order
                          where (int)v.DishType == 4
                          select v).Count();

            Assert.AreEqual(expected, actual);
        }
    }
}
