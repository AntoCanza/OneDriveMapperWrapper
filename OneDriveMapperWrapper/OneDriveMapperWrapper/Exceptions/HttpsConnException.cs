using System;
using System.Collections.Generic;
using System.Text;

namespace OneDriveMapperWrapper.Exceptions
{
    public class HttpsConneException : Exception
    {
        public HttpsConneException(string msg)
            : base(msg)
        {
        }
    }
}
