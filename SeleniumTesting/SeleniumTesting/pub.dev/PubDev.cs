using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTesting.pub.dev
{
    public class PubDev
    {
        const string baseUrl = "https://pub.dev/";

        IWebDriver driver;

        IWebElement searchTextBox;
        public PubDev(IWebDriver chromeDriver) 
        {
            driver = chromeDriver;
            driver.Navigate().GoToUrl(baseUrl);

            searchTextBox = driver.FindElement(By.Name("q"));
        }
        public void RunTests()
        {
            var testCase1Result = TestCase1();
            if (testCase1Result)
            {
                TestCase2();
                TestCase3();
                TestCase4();
                TestCase5();
                TestCase6();
                TestCase7();
                TestCase8();
            }
        }
        public Boolean TestCase1()
        {
            // Verify That the text box exists and displayed
            try
            {
                if (searchTextBox.Displayed)
                {
                    ConsoleMessages.SuccessMessage(1);
                    return true;
                }
                else
                {
                    ConsoleMessages.FailMessage(1);
                    return false;
                }
            }
            catch (NoSuchElementException e)
            {
                ConsoleMessages.FailMessage(1);
                return false;
            }
        }
        public void TestCase2()
        {
            // Verify that the search text box accepts text/input
            string tc2Input = "twitter";

            searchTextBox.SendKeys(tc2Input);

            if (tc2Input.Equals(searchTextBox.GetAttribute("value")))
            {
                ConsoleMessages.SuccessMessage(2);
            }
            else
            {
                ConsoleMessages.FailMessage(2);
            }
            searchTextBox.Clear();
        }
        public void TestCase3()
        {
            // Test the search functionality by entering a valid search term and verifying that the correct results are displayed.
            string tc3Input = "flutter";

            var resultCount = Search(tc3Input);
            if (resultCount > 0)
            {
                var isMatching = CheckResult(tc3Input);
                if (isMatching)
                {
                    ConsoleMessages.SuccessMessage(3);
                }
                else
                {
                    ConsoleMessages.FailMessage(3);
                }
            }
            else
            {
                ConsoleMessages.FailMessage(3);
            }
        }
        public void TestCase4()
        {
            // Verify that the search functionality works correctly when the search query is empty
            string tc4Input = "";

            var resultCount = Search(tc4Input);

            if (resultCount > 0)
            {
                ConsoleMessages.SuccessMessage(4);
            }
            else
            {
                ConsoleMessages.FailMessage(4);
            }

        }

        public void TestCase5()
        {
            // Verify that the search function is case-insensitive
            string tc5Input = "google";

            var tc5Result = Search(tc5Input);
            var tc5UpperCaseResult = Search(tc5Input.ToUpper());

            if (tc5Result == tc5UpperCaseResult)
            {
                ConsoleMessages.SuccessMessage(5);
            }
            else
            {
                ConsoleMessages.FailMessage(5);
            }

        }
        public void TestCase6()
        {
            // Check that the search functionality works as expected when no results are found
            string tc6Input = "@#$%";

            var tc6ResultCount = Search(tc6Input);

            if (tc6ResultCount == 0)
            {
                ConsoleMessages.SuccessMessage(6);
            }
            else
            {
                ConsoleMessages.FailMessage(6);
            }

        }
        public void TestCase7()
        {
            // Check that the search functionality works as expected when the search term is a misspelling
            string tc7Input = "flute";

            var resultCount = Search(tc7Input);
            if (resultCount > 0)
            {
                var isMatching = CheckResult(tc7Input);
                if (isMatching)
                {
                    ConsoleMessages.SuccessMessage(7);
                }
                else
                {
                    ConsoleMessages.FailMessage(7);
                }
            }
            else
            {
                ConsoleMessages.FailMessage(7);
            }

        }
        public void TestCase8()
        {
            // Check that the search functionality works as expected when there are special characters in the search term.
            string tc8Input = "+&flutter++&*";
            string tc8CorrectInput = "flutter";

            var resultCount = Search(tc8Input);
            if (resultCount > 0)
            {
                var isMatching = CheckResult(tc8CorrectInput);
                if (isMatching)
                {
                    ConsoleMessages.SuccessMessage(8);
                }
                else
                {
                    ConsoleMessages.FailMessage(8);
                }
            }
            else
            {
                ConsoleMessages.FailMessage(8);
            }

        }
        public int Search(string searchQuery)
        {
            searchTextBox.SendKeys(searchQuery);
            searchTextBox.Submit();

            var result = int.Parse(driver.FindElement(By.ClassName("count")).Text);

            searchTextBox = driver.FindElement(By.Name("q"));
            searchTextBox.Clear();
            return result;
        }

        public Boolean CheckResult(string searchQuery)
        {
            IWebElement serachResultElement = driver.FindElement(By.ClassName("packages-title"));
            try
            {
                var searchResultText = serachResultElement.Text;
                if (searchResultText.Contains(searchQuery))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }
    }
}
