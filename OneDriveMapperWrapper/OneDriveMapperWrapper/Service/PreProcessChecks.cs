using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OneDriveMapperWrapper.Service
{
    class PreProcessChecks
    {
        public void DoCheck(Configuration cfg, WrapperDto dto)
        {
            PreCheckFileSystem(cfg, dto);
        }

        private void PreCheckFileSystem(Configuration cfg, WrapperDto dto)
        {
            dto.tempDirPath = AppDomain.CurrentDomain.BaseDirectory + cfg.tempDir;
            if (!Directory.Exists(dto.tempDirPath))
            {
                Log.Debug("Create Temp Dir");
                Directory.CreateDirectory(dto.tempDirPath);
            }
        }
    }
}
