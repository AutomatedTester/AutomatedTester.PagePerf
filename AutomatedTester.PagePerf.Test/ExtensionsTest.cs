using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using AutomatedTester.PagePerf;

namespace AutomatedTester.PagePerf.Test
{
    public class ExtensionsTest
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
           driver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void ShouldLoadTheBrowserAndGetWebTimings()
        {
            driver.Url = "http://www.theautomatedtester.co.uk";
            Dictionary<string, object> result = driver.WebTimings();
            /* The dictionary returned will contain something like the following.
             * The values are in milliseconds since 1/1/1970
             * 
             * connectEnd: 1280867925716
             * connectStart: 1280867925687
             * domainLookupEnd: 1280867925687
             * domainLookupStart: 1280867925687
             * fetchStart: 1280867925685
             * legacyNavigationStart: 1280867926028
             * loadEventEnd: 1280867926262
             * loadEventStart: 1280867926155
             * navigationStart: 1280867925685
             * redirectEnd: 0
             * redirectStart: 0
             * requestEnd: 1280867925716
             * requestStart: 1280867925716
             * responseEnd: 1280867925940
             * responseStart: 1280867925919
             * unloadEventEnd: 1280867925940
             */ 
        }
    }
}
