using System;
using System.Collections.Generic;
using PracticumRevisitedLibrary.Repository;
using PracticumRevisitedLibrary.Services;
using PracticumRevisitedLibrary.Processors;

namespace PracticumRevisited
{
    /* Douglas Bean, 03-02-2015 */
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser();
            var repository = new DishRepository();
            List<IProcessor> processors = GetProcessors(repository);
            var restaurantService = new OrderService(parser, processors);
            
            while (true)
            {
                string input = Console.ReadLine();
                string result = restaurantService.ProcessOrder(input);
                Console.WriteLine(result);
            }
        }

        private static List<IProcessor> GetProcessors(IRepository repository)
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
    }
}
