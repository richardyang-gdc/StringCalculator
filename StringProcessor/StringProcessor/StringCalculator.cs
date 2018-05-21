using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringProcessor
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            int sum = 0;
            var delimiters = new string[] { ",", "\n" };

            //需求6：支持不同的分隔符，//[分隔符]\n数字 //;\n1;2;4
            if (numbers.StartsWith("//"))
            {
                var newDelimiter = numbers[2].ToString();
                //var newDelimiter = GetDelimiters(numbers);//需求9：分隔符可以是任意长度的字符串。 //[##]\n1##3##6
                var s = new string[] { newDelimiter };
                Array.Resize(ref delimiters, delimiters.Length + 1);//需求6
                delimiters[delimiters.Length - 1] = newDelimiter;          //需求6    
                numbers = numbers.Substring(3);
                //numbers = numbers.Substring(numbers.IndexOf(']') +1);//需求9：分隔符可以是任意长度的字符串。 //[##]\n1##3##6
            }

            String[] numbersArray = numbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            //if(numbersArray.Length > 2)// //需求4：允许接收不固定数字个数的字符串。
            //{
            //    throw new ArgumentException("参数应该是不能超过2个数字且用逗号‘，’分隔的字符串。");
            //} 
            //else 
            //{
            foreach (var number in numbersArray)
            {
                if (!string.IsNullOrEmpty(number))//added when fix step1/step2 failed test
                {
                    var intNumber = Convert.ToInt16(number);
                    if (intNumber < 0)//需求7：字符串中包括负数将抛异常
                    {
                        throw new ArgumentException("参数字符串里不允许有负数。");//需求7：字符串中包括负数将抛异常
                    }

                    if (intNumber <= 1000)//需求8：字符串中数了超过1000应过滤掉。
                    {
                        sum += intNumber;
                    }
                }
            }
            //}

            return sum;
        }

        //需求9：分隔符可以是任意长度的字符串。 //[##]\n1##3##6
        private string GetDelimiters(string stringNumbers)
        {
            var leftSquareBracketIndex = stringNumbers.IndexOf('[');
            var rightSquareBracketIndex = stringNumbers.IndexOf(']');

            var delimiterStartIndex = leftSquareBracketIndex + 1;
            var delimiterLength = rightSquareBracketIndex - leftSquareBracketIndex - 1;

            return stringNumbers.Substring(delimiterStartIndex, delimiterLength);
        }

        //public int AddNumbers(string args)
        //{
        //    if (string.IsNullOrEmpty(args))
        //    {
        //        return 0;
        //    }

        //    var delimeters = new List<char>()
        //                            {
        //                                '\n',','
        //                            };

        //    if (args[0] == '/')
        //    {
        //        var customDelimeter = args[2];
        //        delimeters.Add(customDelimeter);
        //        args = args.Remove(0,
        //                           3);

        //    }

        //    var numbers = args.ToCharArray().Where(x => !delimeters.Contains(x)).ToList();

        //    if (numbers.Any(x => x == '-'))
        //    {
        //        StringBuilder stringBuilder = new StringBuilder();
        //        for (int i = 0;
        //             i < numbers.Count;
        //             i++)
        //        {
        //            if (numbers[i] == '-')
        //            {
        //                stringBuilder.Append("-");
        //                stringBuilder.Append(numbers[++i]);
        //                stringBuilder.Append(", ");
        //            }
        //        }

        //        throw new Exception(string.Format("negatives {0} not allowed", stringBuilder.ToString()));
        //    }

        //    var sum = numbers.Sum(x => (int)Char.GetNumericValue(x));

        //    return sum;
        //}
    }
}
