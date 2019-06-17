using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace OneDriveMapperWrapper.Service
{
    public class ScriptExecutor
    {
        public void Execute(Configuration cfg, WrapperDto dto)
        {
            if (Path.GetExtension(dto.scriptPath).Equals(cfg.possibleFileExt[0]))
            {
                Log.Debug("Script is a PowerShell file");
                if (File.Exists(dto.scriptPath))
                {
                    this.ExecutePS(dto);
                }
                else
                {
                    Log.Error("Script file not found in Temp directory. skip execution");
                }
            }
        }

        private string[] ExecutePS(WrapperDto dto)
        {
            Log.Debug("Executing PowerShell");
            string[] output = new string[3];

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                FileName = "PowerShell.exe",
                Arguments = String.Format($"-ExecutionPolicy ByPass -WindowStyle Hidden -File \"{0}\"", dto.scriptPath)
            };

            Log.Debug("Arguments: " + startInfo.Arguments);

            using (Process exeProcess = Process.Start(startInfo))
            {
                output[0] = exeProcess.StandardOutput.ReadToEnd();
                output[1] = exeProcess.StandardError.ReadToEnd();

                exeProcess.WaitForExit();

                output[2] = exeProcess.ExitCode.ToString();
            }

            Log.Debug("StandardOutput: " + output[0]);
            Log.Debug("StandardError: " + output[1]);
            Log.Debug("ExitCode: " + output[2]);

            return output;
        }
    }
}
