Page Performance Recorder with Firefox Driver .NET
==================================================

This is a project to use Firefox driver to record what is on the page
by capturing the [HAR](http://groups.google.com/group/http-archive-specification/web/har-1-1-spec?hl=en) using 
Selenium 2, Firebug and NetExport.


Example
-------

	using AutomatedTester.PagePerf;
	using NUnit.Framework;
	using OpenQA.Selenium;

	namespace AutomatedTester.PagePerf.Test
	{
		[TestFixture]
		public class PagePerfDriverTest
		{
			private PagePerfFirefoxDriver driver = null;
			
			[SetUp]
			public void SetUp()
			{
				driver =  new PagePerfFirefoxDriver();
			}

			[TearDown]
			public void TearDown()
			{
				driver.Quit()
			}

			[Test]
			public void ShouldLoadDriverAndCreateAHarFile()
			{
				driver.Navigate().GoToUrl("http://www.theautomatedtester.co.uk/");
			} 
		}
	}