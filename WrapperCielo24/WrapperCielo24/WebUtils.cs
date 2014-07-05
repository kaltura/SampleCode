using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

namespace WrapperCielo24
{
    class WebUtils
    {
        // Progress events/properties?
        private static TimeSpan BASIC_TIMEOUT = new TimeSpan(TimeSpan.TicksPerSecond * 60); // 60 seconds
        private static TimeSpan DOWNLOAD_TIMEOUT = new TimeSpan(TimeSpan.TicksPerMinute * 5);  // 5 minutes

        /* A synchronous method that performs an HTTP request returning data received from the sever as a string */
        public string HttpRequest(Uri uri, HttpMethod method=HttpMethod.GET, Dictionary<string, string> headers=null)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);            
            request.Method = method.ToString();
            foreach (KeyValuePair<string, string> pair in headers)
            {
                request.Headers[pair.Key] = pair.Value;
            }
            IAsyncResult asyncResult = request.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne(BASIC_TIMEOUT); // Wait untill response is received, then proceed
            if(asyncResult.IsCompleted)
            {
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asyncResult);
                    Stream stream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(stream);
                    string serverResponse = streamReader.ReadToEnd();
                    stream.Dispose();
                    return serverResponse;
                }
                catch (WebException err) // Catch (400) Bad Request error
                {
                    Stream errorStream = err.Response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(errorStream);
                    string errorJson = streamReader.ReadToEnd();
                    Dictionary<string, string> responseDict = Utils.DeserializeDictionary(errorJson);
                    throw new EnumWebException(responseDict["ErrorType"], responseDict["ErrorComment"]);
                }
            }
            else
            {
                throw new TimeoutException("The HTTP session has timed out.");
            }
        }

        public string UploadMedia(Uri uri)
        {
            // TODO
            throw new NotImplementedException();
        }
    }

    public enum HttpMethod { GET, POST, DELETE, PUT }

    public class EnumWebException : WebException
    {
        private string errorType;
        public string ErrorType { get { return this.errorType; } }

        public EnumWebException(string errType, string message) : base(errType + ": " + message)
        {
            this.errorType = errType;
        }
    }
}