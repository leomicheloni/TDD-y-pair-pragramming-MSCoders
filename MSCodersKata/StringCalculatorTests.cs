using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace MSCodersKata
{
    [TestClass]
    public class StringCalculatorTests
    {
        [TestMethod]
        public void Add_EmptyString_ReturnsZero()
        {
            Calculator calculator = new Calculator();
            var result = calculator.Add("");
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Add_SingleNumber_ReturnsThatNumber()
        {
            Calculator calculator = new Calculator();
            var result = calculator.Add("3");
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Add_Space_ReturnsZero()
        {
            Calculator calculator = new Calculator();

            var result = calculator.Add(" ");

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Add_TwoNumbers_ReturnsSum()
        {
            Calculator calculator = new Calculator();

            var result = calculator.Add("1,2");

            Assert.AreEqual(3, result);
        }
        [TestMethod]
        public void Add_NumbersWithCarriageReturn_Returns_Sum()
        {
            Calculator calculator = new Calculator();

            var result = calculator.Add("1\n2,3");

            Assert.AreEqual(6, result);
        }
        [TestMethod]
        public void Add_NumbersWithDelimmiterSpecified_Returns_Sum()
        {
            Calculator calculator = new Calculator();

            var result = calculator.Add("//;\n1;2");

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Add_NumbersWithOneNegativeNumber_ThrowsException()
        {
            Calculator calculator = new Calculator();

            const string expectedValue = "negatives not allowed - '-1'";
        
            try
            {
                var result = calculator.Add("-1,2");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(expectedValue, ex.Message);
            }
            
        }
    }

    internal class Calculator
    {
        const string CHOICE_DELIMITER = "//";
        internal int Add(string numbers)
        {
            if (String.IsNullOrWhiteSpace(numbers))
                return 0;
            char[] delimiters = new [] { ',', '\n' };
            if(numbers.StartsWith(CHOICE_DELIMITER))
            {
                string[] values= numbers.Replace(CHOICE_DELIMITER, "").Split('\n');

                delimiters[0] = values[0].ToCharArray().First();
                numbers = values[1];
            }

            List<int> negativeNumbers = numbers.Split(delimiters)
                .Select(number => int.Parse(number))
                .Where(x => x < 0)
                .ToList();

            if (negativeNumbers.Count > 0)
            {
                throw new Exception($"negatives not allowed - '{negativeNumbers.First()}'");
            }

            return numbers.Split(delimiters).Select(number => int.Parse(number)).Sum();
        }
    }
}
