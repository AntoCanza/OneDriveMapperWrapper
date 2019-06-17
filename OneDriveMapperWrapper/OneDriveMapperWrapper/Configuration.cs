using System;
using System.Collections.Generic;
using System.Text;

namespace OneDriveMapperWrapper
{
    public class Configuration
    {
        public string internetConnUrl= "http://clients3.google.com/generate_204";
        public string scriptUrl = "https://URL_TO_YOUR_WEBSERVER/OneDriveMapper.ps1";
        public string scriptName = "OneDriveMapper.ps1";
        public string[] possibleFileExt = new string[] { ".ps1", ".cmd", ".bat" };
        public string tempDir = @"Temp\";
        public int maxConnectionCheck = 60;
        public int treadSleepSeconds = 5;
    }
}
