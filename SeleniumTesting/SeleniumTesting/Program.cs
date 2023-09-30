using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static string baseUrl = "https://pub.dev/";

    static IWebDriver driver = new ChromeDriver();

    static IWebElement searchTextBox;

    static void Main()
    {
        driver.Navigate().GoToUrl(baseUrl);

        searchTextBox = driver.FindElement(By.Name("q"));

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
        driver.Quit();
    }

    public static Boolean TestCase1()
    {
        // Verify That the text box exists and displayed
        try
        {
            if (searchTextBox.Displayed)
            {
                SuccessMessage(1);
                return true;
            }
            else
            {
                SuccessMessage(1);
                return false;
            }
        }
        catch (NoSuchElementException e)
        {
            FailMessage(1);
            return false;
        }
    }
    public static void TestCase2()
    {
        // Verify that the search text box accepts text/input
        string tc2Input = "twitter";

        searchTextBox.SendKeys(tc2Input);

        if (tc2Input.Equals(searchTextBox.GetAttribute("value")))
        {
            SuccessMessage(2);
        }
        else
        {
            FailMessage(2);
        }
        searchTextBox.Clear();
    }
    public static void TestCase3()
    {
        // Test the search functionality by entering a valid search term and verifying that the correct results are displayed.
        string tc3Input = "flutter";

        var resultCount = Search(tc3Input);
        if (resultCount > 0)
        {
            var isMatching = CheckResult(tc3Input);
            if (isMatching)
            {
                SuccessMessage(3);
            }
            else
            {
                FailMessage(3);
            }
        }
        else
        {
            FailMessage(3);
        }
    }
    public static void TestCase4()
    {
        // Verify that the search functionality works correctly when the search query is empty
        string tc4Input = "";

        var resultCount = Search(tc4Input);     
       
        if (resultCount > 0)
        {
            SuccessMessage(4);
        }
        else
        {
            FailMessage(4);
        }

    }

    public static void TestCase5()
    {
        // Verify that the search function is case-insensitive
        string tc5Input = "google";

        var tc5Result = Search(tc5Input);
        var tc5UpperCaseResult = Search(tc5Input.ToUpper());

        if (tc5Result == tc5UpperCaseResult)
        {
            SuccessMessage(5);
        }
        else
        {
            FailMessage(5);
        }

    }
    public static void TestCase6()
    {
        // Check that the search functionality works as expected when no results are found
        string tc6Input = "@#$%";

        var tc6ResultCount = Search(tc6Input);

        if (tc6ResultCount == 0)
        {
            SuccessMessage(6);
        }
        else
        {
            FailMessage(6);
        }

    }
    public static void TestCase7()
    {
        // Check that the search functionality works as expected when the search term is a misspelling
        string tc7Input = "flute";

        var resultCount = Search(tc7Input);
        if (resultCount > 0)
        {
            var isMatching = CheckResult(tc7Input);
            if (isMatching)
            {
                SuccessMessage(7);
            }
            else
            {
                FailMessage(7);
            }
        }
        else
        {
            FailMessage(7);
        }

    }
    public static void TestCase8()
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
                SuccessMessage(8);
            }
            else
            {
                FailMessage(8);
            }
        }
        else
        {
            FailMessage(8);
        }

    }
    public static int Search(string searchQuery)
    {
        searchTextBox.SendKeys(searchQuery);
        searchTextBox.Submit();

        var result = int.Parse(driver.FindElement(By.ClassName("count")).Text);

        searchTextBox = driver.FindElement(By.Name("q"));
        searchTextBox.Clear();
        return result;
    }

    public static Boolean CheckResult(string searchQuery)
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
