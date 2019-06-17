using NUnit.Framework;
using OneDriveMapperWrapper;
using OneDriveMapperWrapper.Service;
using System;
using System.IO;

namespace UnitTest
{
    [TestFixture]
    public class ServiceTest
    {
        public WrapperDto dto = new WrapperDto();

        [TestCase]
        public void MakeValidConnection()
        {
            HttpsConnector connector = new HttpsConnector();
            Configuration cfg = new Configuration
            {
                scriptUrl = "https://--VALID_WEB_SERVER--/OneDriveMapper.ps1"
            };

            connector.Connect(cfg, dto);

            Assert.IsTrue(File.Exists(AppDomain.CurrentDomain.BaseDirectory + cfg.scriptName));
        }
    }
}