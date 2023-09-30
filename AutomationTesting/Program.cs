using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTesting.pub.dev;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static IWebDriver driver = new ChromeDriver();
    static void Main()
    {

        PubDev pubDev = new PubDev(driver);

        pubDev.RunTests();
        
        driver.Quit();
    } 
   
}
