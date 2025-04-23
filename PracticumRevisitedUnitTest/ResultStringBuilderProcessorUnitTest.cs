using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticumRevisitedLibrary.Model;
using PracticumRevisitedLibrary.Repository;
using PracticumRevisitedLibrary.Processors;

namespace PracticumRevisitedUnitTest
{   
    [TestClass]
    public class ResultStringBuilderProcessorUnitTest
    {
        private static IRepository _repository = new DishRepository();

        [TestMethod]
        public void ShouldDisplayErrorOnDuplicate()
        {
            //arrange
            var parsedOrder = new ParsedOrder()
            {
                TimeOfDay = TimeOfDay.morning,
                SelectedDishTypes = new List<int>() { 1, 2, 2, 3, 4}
            };

            var processableOrder = ArrangeProcessorOrder(parsedOrder);

            //act
            var resultStringBuilderProcessor = new ResultStringBuilderProcessor();
            resultStringBuilderProcessor.Process(processableOrder);
            
            string expected = "eggs, toast, error";
            Console.WriteLine(processableOrder.Result);

            //assert
            Assert.AreEqual(expected, processableOrder.Result);
        }

        [TestMethod]
        public void ShouldDisplayTwoCupsOfCoffee()
        {
            //arrange
            var parsedOrder = new ParsedOrder()
            {
                TimeOfDay = TimeOfDay.morning,
                SelectedDishTypes = new List<int>() { 1, 2, 3, 3 }
            };

            var processableOrder = ArrangeProcessorOrder(parsedOrder);

            //act
            var resultStringBuilderProcessor = new ResultStringBuilderProcessor();
            resultStringBuilderProcessor.Process(processableOrder);

            string expected = "eggs, toast, coffee(x2)";

            //assert
            Assert.AreEqual(expected, processableOrder.Result);
        }

       
        [TestMethod]
        public void InvalidBreakfastOrderShouldDisplayError()
        {
            //arrange
            var parsedOrder = new ParsedOrder()
            {
                TimeOfDay = TimeOfDay.morning,
                SelectedDishTypes = new List<int>() { 1, 2, 3, 4 }
            };

            var processableOrder = ArrangeProcessorOrder(parsedOrder);

            //act
            var resultStringBuilderProcessor = new ResultStringBuilderProcessor();
            resultStringBuilderProcessor.Process(processableOrder);

            string expected = "eggs, toast, coffee, error";

            //assert
            Assert.AreEqual(expected, processableOrder.Result);
        }


        private ProcessableOrder ArrangeProcessorOrder(ParsedOrder parsedOrder)
        {
            var processableOrder = new ProcessableOrder();
            processableOrder.ParsedOrder = parsedOrder;

            var dishTypeItemCountProcessor = new DishTypeItemCountProcessor();
            dishTypeItemCountProcessor.Process(processableOrder);

            var orderExpeditorProcessor = new OrderExpeditorProcessor(_repository); 
            orderExpeditorProcessor.Process(processableOrder);

            var duplicateNotAllowedProcessor = new DuplicateNotAllowedProcessor();
            duplicateNotAllowedProcessor.Process(processableOrder);
            return processableOrder;
        }
    }
}
