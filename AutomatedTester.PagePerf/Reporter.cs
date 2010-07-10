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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;

namespace AutomatedTester.PagePerf
{
    static class Reporter
    {
        private static XmlNode node;
        private static Dictionary<string,object> breakDown = new Dictionary<string, object>();

        public static void Process(string testId,string pageId,string profileDir)
        {
            var harFile = string.Empty;
            var query =
                from file in Directory.GetFiles(profileDir, "*.har")
                where file.Contains(testId)
                select file;

            foreach (var entry in query)
            {
                harFile = entry;
            }

            var allText = File.ReadAllText(harFile);
            File.Delete(harFile);
            ProcessHar(allText);
        }

        private static void ProcessHar(string harContents)
        {
            node = JsonToXml(harContents);
            XmlNodeList responses = node.SelectNodes("//entries/response");            
            breakDown["responsecount"] = responses.Count;
            breakDown["loadtimes"] = GetLoadTimes();

            breakDown["totalSizeOfPage"] = TotalSizeOfPage();

            // Writing HAR file just for debugging 
            File.WriteAllText(@"c:\development\har.xml", node.OuterXml.ToString());
        }

        private static int TotalSizeOfPage()
        {
            XmlNodeList bodysizes = node.SelectNodes("//entries/response/bodySize");
            int totalSize = 0;
            foreach (XmlNode size in bodysizes)
            {
                int itemSize = (Int32.Parse(size.InnerText));
                if (itemSize > 0)
                {
                    totalSize += itemSize;
                }
            }

            XmlNodeList headersizes = node.SelectNodes("//entries/response/headersSize");
            foreach (XmlNode size in headersizes)
            {
                int itemSize = (Int32.Parse(size.InnerText));
                if (itemSize > 0)
                {
                    totalSize += itemSize;
                }
            }

            return totalSize/1024;
        }

        private static double GetLoadTimes()
        {
            XmlNode time = node.SelectSingleNode("//onLoad");
            double loadTimes = Int32.Parse(time.InnerText);

            return loadTimes/1000;
        }

        private static XmlNode JsonToXml(string harContents)
        {
            return JsonConvert.DeserializeXmlNode(harContents);
        }
    }
}
