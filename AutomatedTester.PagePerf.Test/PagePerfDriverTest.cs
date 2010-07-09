using AutomatedTester.PagePerf;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AutomatedTester.PagePerf.Test
{
    [TestFixture]
    public class PagePerfDriverTest
    {
        private PagePerfFirefoxDriver driver = null;
        [Test]
        public void ShouldLoadDriverAndCreateAHarFile()
        {
            driver =  new PagePerfFirefoxDriver("theautomatedtester");
            driver.Navigate().GoToUrl("http://www.theautomatedtester.co.uk/");
            driver.Quit();
        } 
    }
}
