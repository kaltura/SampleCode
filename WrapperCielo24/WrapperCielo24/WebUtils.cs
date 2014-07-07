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
        // TODO: Progress events/properties ???
        public static readonly TimeSpan BASIC_TIMEOUT = new TimeSpan(TimeSpan.TicksPerSecond * 60); // 60 seconds
        public static readonly TimeSpan DOWNLOAD_TIMEOUT = new TimeSpan(TimeSpan.TicksPerMinute * 5);  // 5 minutes

        /* A synchronous method that performs an HTTP request returning data received from the sever as a string */
        public string HttpRequest(Uri uri, HttpMethod method, TimeSpan timeout, Dictionary<string, string> headers=null)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);            
            request.Method = method.ToString();
            if (headers != null) {
                foreach (KeyValuePair<string, string> pair in headers) {
                    request.Headers[pair.Key] = pair.Value;
                }
            }
            IAsyncResult asyncResult = request.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne(timeout); // Wait untill response is received, then proceed
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

        public string UploadMedia(Uri uri, Stream fileStream, Dictionary<string, string> headers=null)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            request.Method = HttpMethod.POST.ToString();
            foreach (KeyValuePair<string, string> pair in headers)
            {
                request.Headers[pair.Key] = pair.Value;
            }
            IAsyncResult asyncResult = request.BeginGetRequestStream(null, null);
            asyncResult.AsyncWaitHandle.WaitOne(); // Wait untill stream is obtained
            if (asyncResult.IsCompleted)
            {
                try
                {
                    Stream stream = request.EndGetRequestStream(asyncResult);
                    for (int i = 0; i < fileStream.Length; i++)
                    {
                        stream.WriteByte((byte) fileStream.ReadByte()); // Upload media
                    }
                    fileStream.Dispose();
                    stream.Dispose();
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
                throw new WebException("Unknown error: could not upload media.");
            }

            IAsyncResult asyncResult2 = request.BeginGetResponse(null, null);
            asyncResult2.AsyncWaitHandle.WaitOne(BASIC_TIMEOUT);
            if (asyncResult.IsCompleted)
            {
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asyncResult2);
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