using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringProcessor.Tests
{
    [TestClass]
    public class StringCalculatorTest
    {

        //private int ArrangeAct(string numbers)
        //{
        //    var calculator = new StringCalculator();
        //    return calculator.Add(numbers);
        //}

        //private StringCalculator _calculator = null; //重构用

        #region 需求1：写一个方法，能够接收0，1，2个数字参数，用逗号“，”分隔的字符串。
        [TestMethod]
        public void Add_NeedAddMethodWithStringParameters()
        {
            StringCalculator calculator = new StringCalculator();//refactor
            calculator.Add(string.Empty);
            Assert.IsTrue(true);
        }

        //step3
        //[TestMethod]
        //public void When1NumberAreUsedThenNoException()
        //{
        //    StringCalculator calculator = new StringCalculator();
        //    calculator.Add("1");
        //    Assert.IsTrue(true);
        //}

        //[TestMethod]
        //public void When2NumbersAreUsedThenNoException()
        //{
        //    StringCalculator calculator = new StringCalculator();
        //    calculator.Add("1,2");
        //    Assert.IsTrue(true);
        //}

        //上面两个测试方法重构成下面一个方法
        //step3
        [TestMethod]
        //[DataRow("")]
        [DataRow("1")]
        [DataRow("1,2")]
        public void Add_WhenLessThan2NumbersAreUsed_NoException(string numbers)
        {
            StringCalculator calculator = new StringCalculator();
            calculator.Add(numbers);
            Assert.IsTrue(true);
        }

        //step4
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Add_WhenNonNumberIsUsed_ThrowException()
        {
            StringCalculator calculator = new StringCalculator();
            calculator.Add("1,X");
        }

        //为满足 需求4：允许接收不固定数字个数的字符串。
        //下面的测试已经不需要了

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException), "参数应该是不能超过2个数字且用逗号‘，’分隔的字符串。")]
        //[DataRow("1,2,3")]
        //[DataRow("1,2,3,4")]
        //public void WhenMoreThan2NumbersAreUsedThenThrowException(string numbers)
        //{
        //    StringCalculator calculator = new StringCalculator();
        //    calculator.Add(numbers);
        //}

        #endregion


        //需求2：对于空字符串参数，方法将返回0。
        [TestMethod]
        public void Add_WhenEmptyStringIsUsed_Return0()
        {
            StringCalculator calculator = new StringCalculator();   //refactor         
            Assert.AreEqual(0, calculator.Add(""));
        }

        //step6
        #region 需求3：方法返回字符串里所有数字的和。
        [TestMethod]
        public void Add_ShouldReturnSumOf1Numbers()
        {
            StringCalculator calculator = new StringCalculator();
            int sum = calculator.Add("1");
            Assert.AreEqual(1, sum);
        }

        [TestMethod]
        public void Add_ShouldReturnSumOf2Numbers()
        {
            StringCalculator calculator = new StringCalculator();
            int sum = calculator.Add("1,2");
            Assert.AreEqual(1 + 2, sum);
        }

        [TestMethod]
        [DataRow(1, "1")]
        [DataRow(1 + 1, "1,1")]
        [DataRow(1 + 2, "1,2")]
        [DataRow(1 + 2 + 3, "1,2,3")] //需求4：允许接收不固定数字个数的字符串。
        [DataRow(1 + 2 + 3 + 5, "1,2,3,5")] //需求4：允许接收不固定数字个数的字符串。
        public void Add_ShouldReturnSumOfAllNumbers(int expected, string stringNumbers)
        {
            StringCalculator calculator = new StringCalculator();
            int sum = calculator.Add(stringNumbers);
            Assert.AreEqual(expected, sum);
        }
        #endregion

        //需求5：允许 Add 方法处理数字之间的新分隔符 (而不是逗号)
        [TestMethod]
        [DataRow(1 + 2, "1\n2")]
        [DataRow(1 + 2 + 14, "\n1,2\n14")]
        public void Add_ShouldAllowNewLineAsSeparator(int expected, string stringNumbers)
        {
            StringCalculator calculator = new StringCalculator();
            //int sum = calculator.Add("1,2\n5"); //重构
            //Assert.AreEqual(1+2+5, sum);
            int sum = calculator.Add(stringNumbers);
            Assert.AreEqual(expected, sum);
        }

        [TestMethod]
        [DataRow(1 + 2 + 3 + 11, "//;\n1;2\n3;11")]
        [DataRow(1 + 2 + 3 + 11, "//|\n1|2\n3|11")]
        public void Add_ShouldSupportSeparator(int expected, string stringNumbers)
        {
            StringCalculator calculator = new StringCalculator();
            //int sum = calculator.Add("//;\n1;2;\n14");//重构
            //Assert.AreEqual(1 + 2 + 14, sum);//重构
            int sum = calculator.Add(stringNumbers);
            Assert.AreEqual(expected, sum);
        }

        //需求7：字符串中包括负数将抛异常
        [TestMethod]
        [DataRow(1 + 2 + 3 + 11, "//;\n1;-2\n3;11")]
        [ExpectedException(typeof(ArgumentException),"参数字符串里不允许有负数。")]        
        public void Add_IfNegativeNumbersAreUsed_ThrowException(int expected, string stringNumbers)
        {
            StringCalculator calculator = new StringCalculator();
            int sum = calculator.Add(stringNumbers);
            Assert.AreEqual(expected, sum);
        }

        //需求8：字符串中数了超过1000应过滤掉。
        [TestMethod]
        [DataRow(1 + 2 + 3+1000, "//;\n1;2\n3;1000;1005")]
        [DataRow(1 + 2 + 3, "//;\n1;2\n3;1001")]
        public void Add_IfNumbersAreGreaterThan1000_IgnoreThoseNumbers(int expected, string stringNumbers)
        {
            StringCalculator calculator = new StringCalculator();
            int sum = calculator.Add(stringNumbers);
            Assert.AreEqual(expected, sum);
        }

        //需求9：分隔符可以是任意长度的字符串。 //[##]\n1##3##6
        //[TestMethod]
        //[DataRow(1 + 2 + 3 + 1000, "//[##]\n1##2\n3##1000##1005")]
        //[DataRow(1 + 2 + 3 + 1000, "//[#!#]\n1#!#2\n3#!#1000#!#1005")]
        //public void Add_ShouldSupportAnyLengthOfDelimiterString(int expected, string stringNumbers)
        //{
        //    StringCalculator calculator = new StringCalculator();
        //    int sum = calculator.Add(stringNumbers);
        //    Assert.AreEqual(expected, sum);
        //}

    }
}
