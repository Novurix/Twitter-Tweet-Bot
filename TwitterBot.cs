using System;
using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace bot
{
    public class TwitterBot
    {
        static WordsManager countries;
        static string[] previousCountries;

        static IWebDriver driver;
        public static void Main(string[] args)
        {
            driver = new Driver();
            countries = new WordsManager();

            loadSite("twitter.com");
        }

        static void loadSite(string site)
        {
            driver.Url = "https://" + site;
            Thread.Sleep(3000);

            login("/compose/tweet");
        }

        static void login(string url)
        {
            string user = "USER HERE";
            string pass = "PASSWORD HERE";

            IWebElement userInput = driver.FindElement(By.Name("session[username_or_email]"));
            IWebElement passInput = driver.FindElement(By.Name("session[password]"));

            userInput.SendKeys(user);
            passInput.SendKeys(pass);

            Thread.Sleep(1000);
            passInput.SendKeys(Keys.Enter);

            tweet(url);
        }

        static void tweet(string url)
        {
            Thread.Sleep(1000);

            for (int i = 0; i < 1000; i++)
            {
                Random randomCountry = new Random();
                int country = randomCountry.Next(0, countries.countries.Length);

                driver.Url = "https://twitter.com" + url;
                Thread.Sleep(4000);
                IWebElement tweetInput = driver.FindElement(By.XPath("//*[@id='react-root']/div/div/div[1]/div[2]/div/div/div/div[2]/div[2]/div/div[3]/div/div/div/div[1]/div/div/div/div/div[2]/div[1]/div/div/div/div/div/div/div/div/div/div[1]/div/div/div/div[2]/div/div/div/div/span/br"));
                string text = country.ToString();
                tweetInput.SendKeys("[TWEET: " + (i+1) + "]: " + countries.countries[country]);
                Thread.Sleep(3000);

                IWebElement tweetButton = driver.FindElement(By.XPath("//*[@id='react-root']/div/div/div[1]/div[2]/div/div/div/div[2]/div[2]/div/div[3]/div/div/div/div[1]/div/div/div/div/div[2]/div[2]/div/div/div[2]/div[4]"));
                tweetButton.Click();
                Thread.Sleep(3000);
            }
        }
    }

    class Driver : ChromeDriver
    {
        public Driver()
        {
            this.NetworkConditions = new ChromeNetworkConditions()
            { DownloadThroughput = 0, UploadThroughput = 0, Latency = TimeSpan.FromMilliseconds(1), IsOffline = false };
        }
    }
}
