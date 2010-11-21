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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Newtonsoft.Json;

namespace AutomatedTester.PagePerf
{
    static class Reporter
    {
        private static XmlNode node;
        private static Dictionary<string,object> breakDown = new Dictionary<string, object>();

        public static void Process(string pageId,string profileDir)
        {
            var files = Directory.GetFiles(profileDir, "*.har");

            foreach (var file in files)
            {
                Console.Write(RunPageSpeedScoring(file));
                File.Delete(file);
            }
        }

        private static string RunPageSpeedScoring(string harFile)
        {
            Process pageSpeedApp = new Process();
            pageSpeedApp.StartInfo.FileName = @"har_to_pagespeed.exe";
            pageSpeedApp.StartInfo.Arguments = harFile;
            pageSpeedApp.StartInfo.UseShellExecute = false;
            pageSpeedApp.StartInfo.RedirectStandardOutput = true;
            pageSpeedApp.Start();
            string results = pageSpeedApp.StandardOutput.ReadToEnd();
            pageSpeedApp.WaitForExit(2000);

            return FormatPageSpeedScore(results);
        }

        private static string FormatPageSpeedScore(string results)
        {
            StringBuilder score = new StringBuilder();
            Regex scores = new Regex(@"(_.*_\s+\(.*\))");
            MatchCollection matches = scores.Matches(results);
            foreach (var match in matches)
            {
                score.AppendLine(match.ToString().Replace('_', ' '));
            }

            return score.ToString();
        }

        private static XmlNode JsonToXml(string harContents)
        {
            return JsonConvert.DeserializeXmlNode(harContents);
        }
    }
}
