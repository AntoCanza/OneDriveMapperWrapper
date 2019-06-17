using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneDriveMapperWrapper.Service
{
    public class ServiceMapper
    {
        readonly PreProcessChecks preCheck = new PreProcessChecks();
        readonly HttpsConnector connector = new HttpsConnector();
        readonly ScriptExecutor exec = new ScriptExecutor();
        public WrapperDto dto = new WrapperDto();

        public void DoWork(Configuration cfg)
        {
            Log.Information("Check-up");
            preCheck.DoCheck(cfg,dto);
            Log.Information("Calling HttpsConnector");
            connector.Connect(cfg, dto);
            Log.Information("Calling Script Executor");
            exec.Execute(cfg, dto);
        }
    }
}
