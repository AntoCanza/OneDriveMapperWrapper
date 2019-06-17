using OneDriveMapperWrapper.Exceptions;
using Serilog;
using System;
using System.IO;
using System.Net;

namespace OneDriveMapperWrapper.Service
{
    public class HttpsConnector
    {
        public void Connect(Configuration cfg, WrapperDto dto)
        {
            try
            {
                int i = 0;
                while (this.CheckForInternetConnection(cfg) == false && i <= cfg.maxConnectionCheck)
                {
                    i++;
                    Log.Debug($"NO Internet Connection: wait {0} sec for next try", cfg.treadSleepSeconds);
                    System.Threading.Thread.Sleep(cfg.treadSleepSeconds * 1000);
                }

                if (i >= cfg.maxConnectionCheck)
                {
                    Log.Information("NO Internet Connection");
                    throw new HttpsConneException("Internet Connection not available");
                }

                this.GetFile(cfg, dto);
            }
            catch (Exception ex)
            {
                throw new HttpsConneException(ex.Message);
            }
        }


        private void GetFile(Configuration cfg, WrapperDto dto)
        {

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(cfg.scriptUrl);
            httpRequest.Method = WebRequestMethods.Http.Get;
            HttpWebResponse httpResponse = null;
            Stream httpResponseStream = null;

            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                httpResponseStream = httpResponse.GetResponseStream();

                int bufferSize = 1024;
                byte[] buffer = new byte[bufferSize];
                int bytesRead = 0;

                dto.scriptPath = dto.tempDirPath + Path.GetFileName(cfg.scriptUrl);
                if (File.Exists(dto.scriptPath))
                {
                    File.Delete(dto.scriptPath);
                }

                using (FileStream fileStream = File.Create(dto.scriptPath))
                {
                    while ((bytesRead = httpResponseStream.Read(buffer, 0, bufferSize)) != 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                    }
                }

                Log.Information("File download successfully");
            }
            catch (Exception ex)
            {
                //On downloading exception we need to catch and execute the local script if any
                dto.httpError = ex.Message;
                dto.scriptPath = dto.tempDirPath + cfg.scriptName;
                Log.Error("Error on downloading script via HTTPS.. will check it into Temp directory");
                Log.Error(ex.Message);
            }
            finally
            {
                if (httpResponseStream != null)
                {
                    httpResponseStream.Flush();
                    httpResponseStream.Close();
                }

                if (httpResponse != null)
                {
                    httpResponse.Close();
                }
            }

        }

        private bool CheckForInternetConnection(Configuration cfg)
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead(cfg.internetConnUrl))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
