using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticumRevisitedLibrary.Model;
using PracticumRevisitedLibrary.Services;

namespace PracticumRevisitedUnitTest
{
    [TestClass]
    public class ParserUnitTest
    {
        IParser _parser = new Parser();
        
        [TestMethod]
        public void TestParser_TimeOfDayShouldBeMorning()
        {
            string input = "morning, 1, 2, 3";

            ParsedOrder parsedOrder =_parser.ParseInput(input);

            TimeOfDay expected = TimeOfDay.morning;
            TimeOfDay actual = parsedOrder.TimeOfDay;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestParser_TimeOfDayShouldBeNight()
        {
            string input = "NIGHT, 3, 2, 1";

            ParsedOrder parsedOrder = _parser.ParseInput(input);

            TimeOfDay expected = TimeOfDay.night;
            TimeOfDay actual = parsedOrder.TimeOfDay;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestParser_TimeOfDayShouldBeMorning_InputNotLowerCase()
        {
            string input = "MORNING, 1, 2, 3";

            ParsedOrder parsedOrder = _parser.ParseInput(input);

            TimeOfDay expected = TimeOfDay.morning;
            TimeOfDay actual = parsedOrder.TimeOfDay;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestParser_SelectedDishTypesShouldMatchInput()
        {
            string input = "morning, 1, 2, 3"; 

            ParsedOrder parsedOrder = _parser.ParseInput(input);

            string expected = "1,2,3";
          
            //convert int list to string list
            IEnumerable<string> selectedDishTypes = (from type in parsedOrder.SelectedDishTypes
                                                    select type.ToString()).ToList();

            //flatten string list to a string
            string actual = String.Join(",", selectedDishTypes);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestParser_SelectedDishTypesShouldMatchInput_commaAtEndOfInputShouldNotMatter()
        {
            string input = "night 3, 2, 1,";

            ParsedOrder parsedOrder = _parser.ParseInput(input);

            string expected = "3,2,1";

            //convert int list to string list
            IEnumerable<string> selectedDishTypes = (from type in parsedOrder.SelectedDishTypes
                                                     select type.ToString()).ToList();

            //flatten string list to a string
            string actual = String.Join(",", selectedDishTypes);

            Assert.AreEqual(expected, actual);
        }
    }
}
