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
        public string HttpRequest(Uri uri, HttpMethod method, TimeSpan timeout, Dictionary<string, string> headers = null)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            request.Method = method.ToString();
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> pair in headers)
                {
                    request.Headers[pair.Key] = pair.Value;
                }
            }
            IAsyncResult asyncResult = request.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne(timeout); // Wait untill response is received, then proceed
            if (asyncResult.IsCompleted)
            {
                return ReadResponse(request, asyncResult);
            }
            else
            {
                throw new TimeoutException("The HTTP session has timed out.");
            }
        }

        public string UploadMedia(Uri uri, Stream fileStream, string contentType)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            request.Method = HttpMethod.POST.ToString();
            request.ContentType = contentType;

            IAsyncResult asyncRequest = request.BeginGetRequestStream(null, null);
            asyncRequest.AsyncWaitHandle.WaitOne(BASIC_TIMEOUT); // Wait untill stream is opened
            if (asyncRequest.IsCompleted)
            {
                try
                {
                    Stream stream = request.EndGetRequestStream(asyncRequest);
                    fileStream.CopyTo(stream);
                    fileStream.Flush();
                    stream.Flush();
                    fileStream.Dispose();
                    stream.Dispose();
                }
                catch (WebException err)
                {
                    throw new WebException("Unknown error: could not upload media.", err);
                }
            }
            else
            {
                throw new WebException("Timeout error: could not open stream for uploading.");
            }

            IAsyncResult asyncResponse = request.BeginGetResponse(null, null);
            // Media is actually still uploading asynchronously at this point. The following AsyncHandle fires back
            // when media is uploaded and response is received.
            asyncResponse.AsyncWaitHandle.WaitOne();
            if (asyncRequest.IsCompleted)
            {
                return ReadResponse(request, asyncResponse);
            }
            else
            {
                throw new TimeoutException("The HTTP session has timed out.");
            }
        }

        /* Helper method */
        private string ReadResponse(HttpWebRequest request, IAsyncResult asyncResponse)
        {
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asyncResponse);
                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream);
                string serverResponse = streamReader.ReadToEnd();
                stream.Dispose();
                return serverResponse;
            }
            catch (WebException error) // Catch (400) Bad Request error
            {
                Stream errorStream = error.Response.GetResponseStream();
                StreamReader streamReader = new StreamReader(errorStream);
                string errorJson = streamReader.ReadToEnd();
                Dictionary<string, string> responseDict = Utils.DeserializeDictionary(errorJson);
                throw new EnumWebException(responseDict["ErrorType"], responseDict["ErrorComment"]);
            }
        }
    }

    public enum HttpMethod { GET, POST, DELETE, PUT }

    public class EnumWebException : WebException
    {
        private string errorType;
        public string ErrorType { get { return this.errorType; } }

        public EnumWebException(string errType, string message)
            : base(errType + ": " + message)
        {
            this.errorType = errType;
        }
    }
}