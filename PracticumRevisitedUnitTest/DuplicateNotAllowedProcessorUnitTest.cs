using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticumRevisitedLibrary.Model;
using PracticumRevisitedLibrary.Repository;
using PracticumRevisitedLibrary.Processors;

namespace PracticumRevisitedUnitTest
{
    [TestClass]
    public class DuplicateNotAllowedProcessorUnitTest
    {
        private static IRepository _repository = new DishRepository();
        
        [TestMethod]
        public void DuplicateErrorShouldBeCaught()
        {
            //arrange
            var parsedOrder = new ParsedOrder()
            {
                TimeOfDay = TimeOfDay.night,
                SelectedDishTypes = new List<int>() { 1, 4, 4, 3 }
            };
            
            var order = new List<Dish>();
            var respository = new DishRepository();

            foreach (var item in parsedOrder.SelectedDishTypes)
            {
                var dish = respository.GetSelectedItem(item, (int)parsedOrder.TimeOfDay);
                order.Add(dish);
            }

            var processableOrder = new ProcessableOrder();
            processableOrder.ParsedOrder = parsedOrder;
            processableOrder.Order = order;

            var dishTypeItemCountProcessor = new DishTypeItemCountProcessor();
            dishTypeItemCountProcessor.Process(processableOrder);

            bool expected = true;

            //act
            var duplicateNotAllowedProcessor = new DuplicateNotAllowedProcessor();
            duplicateNotAllowedProcessor.Process(processableOrder);

            //assert
            Assert.AreEqual(expected, processableOrder.IsDuplicateNotAllowedError);
        }

        [TestMethod]
        public void DuplicateErrorShouldBeDishTypeFour()
        {
            //arrange
            var parsedOrder = new ParsedOrder()
            {
                TimeOfDay = TimeOfDay.night,
                SelectedDishTypes = new List<int>() { 1, 4, 4, 3 }
            };

            var order = new List<Dish>();
            var respository = new DishRepository();

            foreach (var item in parsedOrder.SelectedDishTypes)
            {
                var dish = respository.GetSelectedItem(item, (int)parsedOrder.TimeOfDay);
                order.Add(dish);
            }

            var processableOrder = new ProcessableOrder();
            processableOrder.ParsedOrder = parsedOrder;
            processableOrder.Order = order;

            var dishTypeItemCountProcessor = new DishTypeItemCountProcessor();
            dishTypeItemCountProcessor.Process(processableOrder);

            int expected = 4;

            //act
            var duplicateNotAllowedProcessor = new DuplicateNotAllowedProcessor();
            duplicateNotAllowedProcessor.Process(processableOrder);

            //assert
            Assert.AreEqual(expected, processableOrder.DuplicateErrorDishType);
        }

        [TestMethod]
        public void DuplicateErrorTypeShouldBeWine()
        {
            //arrange
            var parsedOrder = new ParsedOrder()
            {
                TimeOfDay = TimeOfDay.night,
                SelectedDishTypes = new List<int>() { 1, 2, 3, 3, 4 }
            };

            var processableOrder = new ProcessableOrder();
            processableOrder.ParsedOrder = parsedOrder;

            var orderExpeditorProcessor = new OrderExpeditorProcessor(_repository); 
            orderExpeditorProcessor.Process(processableOrder);

            var dishTypeItemCountProcessor = new DishTypeItemCountProcessor();
            dishTypeItemCountProcessor.Process(processableOrder);

            int expected = 3;

            //act
            var duplicateNotAllowedProcessor = new DuplicateNotAllowedProcessor();
            duplicateNotAllowedProcessor.Process(processableOrder);

            //assert
            Assert.AreEqual(expected, processableOrder.DuplicateErrorDishType);
        }
    }
}
