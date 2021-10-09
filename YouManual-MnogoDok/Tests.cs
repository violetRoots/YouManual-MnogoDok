using System.Linq;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace YouManual_MnogoDok
{
    [TestFixture]
    public class Tests
    {
        private const string ManualCategoryWebPage =
         "https://mnogo-dok.ru/instrukcii/sendvalues/type/%D0%91%D1%8B%D1%82%D0%BE%D0%B2%D0%BE%D0%B9+%D0%B8%D0%BD%D1%81%D1%82%D1%80%D1%83%D0%BC%D0%B5%D0%BD%D1%82/%D0%A8%D1%83%D1%80%D1%83%D0%BF%D0%BE%D0%B2%D0%B5%D1%80%D1%82%D1%8B+%D0%B8+%D0%B4%D1%80%D0%B5%D0%BB%D0%B8/Bosch/";

        private IWebDriver _driver;

        private readonly By _manualContainer = By.XPath("//div[@class = 'flex']");
        private readonly By _downloadButton = By.XPath("//a[@href='#download']");


        private List<string> _manualWebPages = new List<string>();

        [SetUp]
        public void SetUp()
        {
            //var service = ChromeDriverService.CreateDefaultService();
            //string startupPath = System.IO.Directory.GetCurrentDirectory();
            //service.LogPath = "C:\\Users\\Дом_ПК\\Desktop\\debug.log";
            //service.EnableVerboseLogging = true;

            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(ManualCategoryWebPage);
            _driver.Manage().Window.Maximize();

            var containers = _driver.FindElements(_manualContainer);

            foreach (var container in containers)
            {
                var manualButtons = container.FindElements(By.XPath("//a[@class='']")).ToList();
                manualButtons.AddRange(container.FindElements(By.XPath("//a[@class='inst_hidden']")));
                foreach (var manualWebPage in manualButtons)
                {
                    _manualWebPages.Add(manualWebPage.GetAttribute("href"));
                }
            }
        }

        [Test]
        public void Test1()
        {
            foreach (var manualWebPage in _manualWebPages)
            {
                if (!manualWebPage.Contains("mnogo-dok.ru")) continue;

                _driver.Navigate().GoToUrl(manualWebPage);

                Thread.Sleep(500);

                var downloadButton = _driver.FindElement(_downloadButton);
                downloadButton?.Click();

                Thread.Sleep(500);

                _driver.Navigate().Back();

                Thread.Sleep(500);
            }
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}