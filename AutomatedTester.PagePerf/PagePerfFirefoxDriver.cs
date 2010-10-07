/*
Copyright 2010 - David Burns

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System.Threading;
using OpenQA.Selenium.Firefox;

namespace AutomatedTester.PagePerf
{
    public class PagePerfFirefoxDriver : FirefoxDriver
    {
        #region Constructors
        public PagePerfFirefoxDriver()
            : base(UpdateProfile(new FirefoxProfile()))
        {
        }

        public PagePerfFirefoxDriver(string testId) 
            : base(UpdateProfile(new FirefoxProfile()))
        {
            TestId = testId;
        }

        public PagePerfFirefoxDriver(string testId, FirefoxProfile profile) 
            : base(UpdateProfile(profile))
        {
            TestId = testId;
        }

        public PagePerfFirefoxDriver(string testId, FirefoxBinary binary, FirefoxProfile profile)
            : base(binary,UpdateProfile(profile))
        {
            TestId = testId;
        }
        #endregion

        #region properties
        public string TestId { get; set; }
        #endregion

        #region public methods
        public void Process(string pageId)
        {
            Reporter.Process(TestId, pageId, ProfileDir);
        }

        public new void Quit()
        {
            QuitAndCloseBrowser(true);
        }

        public void Quit(bool process)
        {
            QuitAndCloseBrowser(false);
        }
        #endregion

        #region private methods
        private void QuitAndCloseBrowser(bool process)
        {
            if (process)
            {
                Navigate().GoToUrl("http://example.com");
                Thread.Sleep(500);
                Reporter.Process(TestId, "", ProfileDir);    
            }

            base.Quit();
        }

        private string ProfileDir
        {
            get { return Profile.ProfileDirectory + "\\firebug\\netexport\\logs\\"; }
        }

        private static FirefoxProfile UpdateProfile(FirefoxProfile firefoxProfile)
        {
            firefoxProfile.AddExtension("firebug-1.6X.0a7.xpi");
            firefoxProfile.AddExtension("netExport-0.7b13-mob.xpi");
            firefoxProfile.AddExtension("fireStarter-0.1.a5.xpi");
            firefoxProfile.SetPreference("extensions.firebug.netexport.autoExportActive", true);
            firefoxProfile.SetPreference("extensions.firebug.DBG_NETEXPORT", true);
            firefoxProfile.SetPreference("extensions.firebug.onByDefault", true);
            firefoxProfile.SetPreference("extensions.firebug.defaultPanelName", "net");
            firefoxProfile.SetPreference("extensions.firebug.net.enableSites", true);
            firefoxProfile.SetPreference("extensions.firebug.previousPlacement", 1);

            return firefoxProfile;
        }
        #endregion
    }
}
