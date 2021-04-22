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
      "https://mnogo-dok.ru/instrukcii/sendvalues/type/%D0%92%D0%B8%D0%B4%D0%B5%D0%BE/%D0%9F%D1%80%D0%BE%D0%B5%D0%BA%D1%82%D0%BE%D1%80%D1%8B/Acer/";

    private IWebDriver _driver;
    
    private readonly By _manualContainer = By.XPath("//div[@class = 'flex']");
    private readonly By _downloadButton = By.XPath("//a[@href='#download']");

    private List<string> _manualWebPages = new List<string>();
    
    [SetUp]
    public void SetUp()
    {
      _driver = new ChromeDriver();
      _driver.Navigate().GoToUrl(ManualCategoryWebPage);
      _driver.Manage().Window.Maximize();
      
      var containers = _driver.FindElements(_manualContainer);
      
      foreach (var container in containers)
      {
        foreach (var manualWebPage in container.FindElements(By.TagName("a")))
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
        _driver.Navigate().GoToUrl(manualWebPage);
        
        Thread.Sleep(100);
        
        var downloadButton = _driver.FindElement(_downloadButton);
        downloadButton.Click();
        
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