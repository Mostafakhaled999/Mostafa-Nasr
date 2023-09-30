using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTesting
{
    public class ConsoleMessages
    {
        public static void SuccessMessage(int textCaseNumber)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Test Case {textCaseNumber} Succeed");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void FailMessage(int textCaseNumber)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Test Case {textCaseNumber} Failed");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
