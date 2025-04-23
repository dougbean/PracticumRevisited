using System.Collections.Generic;
using PracticumRevisitedLibrary.Model;
using PracticumRevisitedLibrary.Processors;

namespace PracticumRevisitedLibrary.Services
{
    public class RestaurantService : IRestaurantService
    {
        private IParser _parser;
        private IList<IProcessor> _processors;

        public RestaurantService(IParser parser, IList<IProcessor> processors)
        {
            _parser = parser;
            _processors = processors;
        }

        public string ProcessOrder(string input)
        {
            ParsedOrder parsedOrder = _parser.ParseInput(input);

            var processableOrder = new ProcessableOrder() {ParsedOrder = parsedOrder};

            foreach (var processor in _processors)
            {
                processor.Process(processableOrder);
            }

            return processableOrder.Result;
        }
    }
}
