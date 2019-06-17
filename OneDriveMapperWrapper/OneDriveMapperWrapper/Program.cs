using OneDriveMapperWrapper.Service;
using Serilog;
using System;

namespace OneDriveMapperWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Debug()
                   .WriteTo.Console()
                   .WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day)
                   .CreateLogger();

            Log.Information("*****************************************************");
            Log.Information("Starting");

            ServiceMapper service = new ServiceMapper();

            //Replace values into class def
            Configuration cfg = new Configuration();

            try
            {
                service.DoWork(cfg);
                Log.Information("ENDs");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong");
            }
            finally
            {
                Log.Information("*****************************************************");
                Log.CloseAndFlush();
            }
        }
    }
}
