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

using System.IO;
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


        public PagePerfFirefoxDriver(FirefoxProfile profile) 
            : base(UpdateProfile(profile))
        {
        }

        public PagePerfFirefoxDriver(FirefoxBinary binary, FirefoxProfile profile)
            : base(binary,UpdateProfile(profile))
        {
        }
        #endregion


        #region public methods
        public void Process(string pageId)
        {
            Reporter.Process(pageId, ProfileDir);
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
                Reporter.Process("", ProfileDir);    
            }

            base.Quit();
        }

        private string ProfileDir
        {
            get {
                return Profile.ProfileDirectory +
                       string.Concat(Path.DirectorySeparatorChar, "firebug", Path.DirectorySeparatorChar, "netexport",
                                     Path.DirectorySeparatorChar, "logs", Path.DirectorySeparatorChar); }
        }

        private static FirefoxProfile UpdateProfile(FirefoxProfile firefoxProfile)
        {
            firefoxProfile.AddExtension("firebug-1.6.0.xpi");
            firefoxProfile.SetPreference("extensions.firebug.currentVersion", "9.99");    // don't display firstrun
            firefoxProfile.SetPreference("extensions.firebug.DBG_NETEXPORT", false);
            firefoxProfile.SetPreference("extensions.firebug.onByDefault", true);
            //firefoxProfile.SetPreference("extensions.firebug.defaultPanelName", "net");  //needed?
            firefoxProfile.SetPreference("extensions.firebug.net.enableSites", true);
            //firefoxProfile.SetPreference("extensions.firebug.previousPlacement", 1);  // neeeded?


            firefoxProfile.AddExtension("fireStarter-0.1.a5.xpi");
            firefoxProfile.AddExtension("netExport-0.8b9.xpi");
            firefoxProfile.SetPreference("extensions.firebug.netexport.alwaysEnableAutoExport", true);
            firefoxProfile.SetPreference("extensions.firebug.netexport.autoExportToFile", true);
            firefoxProfile.SetPreference("extensions.firebug.netexport.autoExportToServer", false);
            // firefoxProfile.SetPreference("extensions.firebug.netexport.beaconServerURL", "http://localhost/test/test.aspx"); // set this to send the har data to a remote server
            firefoxProfile.SetPreference("extensions.firebug.netexport.showPreview", false); // Don't preview.
            firefoxProfile.SetPreference("extensions.firebug.netexport.sendToConfirmation", false); // Don't ask for confirmation. This was seemeed to crash FF / Se when set to automatically run.
            firefoxProfile.SetPreference("extensions.firebug.netexport.pageLoadedTimeout", 1500);



            return firefoxProfile;
        }
        #endregion
    }
}
