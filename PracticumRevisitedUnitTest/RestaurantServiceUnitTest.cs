using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticumRevisitedLibrary.Repository;
using PracticumRevisitedLibrary.Services;
using PracticumRevisitedLibrary.Processors;

namespace PracticumRevisitedUnitTest
{
    [TestClass]
    public class RestaurantServiceUnitTest
    {  
        private static IRestaurantService _restaurantService;

        [TestInitialize()]
        public void Initialize()
        {
             var parser = new Parser();
             var repository = new DishRepository(); 
             List<IProcessor> processors = GetProcessors(repository);
             _restaurantService = new RestaurantService(parser, processors);
        }

        private List<IProcessor> GetProcessors(IRepository repository)
        {
            var processors = new List<IProcessor>();

            var dishTypeItemCountProcessor = new DishTypeItemCountProcessor();
            var orderExpeditorProcessor = new OrderExpeditorProcessor(repository);
            var duplicateNotAllowedProcessor = new DuplicateNotAllowedProcessor();
            var resultStringBuilderProcessor = new ResultStringBuilderProcessor();

            processors.Add(dishTypeItemCountProcessor);
            processors.Add(orderExpeditorProcessor);
            processors.Add(duplicateNotAllowedProcessor);
            processors.Add(resultStringBuilderProcessor);

            return processors;
        }

        [TestMethod]
        public void InvalidOrderShouldReturnValidEntryWithError()
        {
            string input = "morning, 1, 1, 2, 3";
            string expected = "eggs, error";

            string actual = _restaurantService.ProcessOrder(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ServiceShouldReturnEggsToastCoffee()
        {
            string input = "morning, 1, 2, 3";
            string expected = "eggs, toast, coffee";

            string actual = _restaurantService.ProcessOrder(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ServiceShouldReturnEggsToastCoffeeWithDifferentOrderOfInput()
        {
            string input = "morning, 2, 1, 3";
            string expected = "eggs, toast, coffee";

            string actual = _restaurantService.ProcessOrder(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ServiceShouldReturnEggsToastCoffeeError()
        {
            string input = "morning, 1, 2, 3, 4";
            string expected = "eggs, toast, coffee, error";

            string actual = _restaurantService.ProcessOrder(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ServiceShouldReturnEggsToastCoffeeX3()
        {
            string input = "morning 1, 2, 3, 3, 3";
            string expected = "eggs, toast, coffee(x3)";

            string actual = _restaurantService.ProcessOrder(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ServiceShouldReturnSteakPotatoWineCake()
        {
            string input = "night, 1, 2, 3, 4";
            string expected = "steak, potato, wine, cake";

            string actual = _restaurantService.ProcessOrder(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ServiceShouldReturnSteakPotatoWineError()
        {
            string input = "night, 1, 2, 3, 5";
            string expected = "steak, potato, wine, error";

            string actual = _restaurantService.ProcessOrder(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ServiceShouldReturnSteakError()
        {
            string input = "night, 1, 1, 2, 3, 5";
            string expected = "steak, error";

            string actual = _restaurantService.ProcessOrder(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExtraCommaInOrderInputShouldNotResultInErrorAppendedToOutput()
        {
            string input = "night, 1, 2, 3, 4,";
            string expected = "steak, potato, wine, cake";

            string actual = _restaurantService.ProcessOrder(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
