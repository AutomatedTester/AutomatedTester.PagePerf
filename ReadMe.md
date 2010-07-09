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
			[Test]
			public void ShouldLoadDriverAndCreateAHarFile()
			{
				driver =  new PagePerfFirefoxDriver("theautomatedtester");
				driver.Navigate().GoToUrl("http://www.theautomatedtester.co.uk/");
				driver.Quit();
			} 
		}
	}