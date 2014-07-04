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
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asyncResult);
                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream);
                string serverResponse = streamReader.ReadToEnd();
                stream.Dispose();
                return serverResponse;
            }
            else
            {
                throw new TimeoutException("The HTTP session has timed out.");
            }
        }

        public string UploadMedia(Uri uri)
        {
            throw new NotImplementedException();
        }
    }

    public enum HttpMethod { GET, POST, DELETE, PUT }

    /*class RequestState
    {
        // This class retains the request state through asyncronous calls.
        public int BufferSize { get; set; }
        public byte[] Buffer { get; set; }
        public HttpWebRequest Request { get; set; }

        public RequestState(HttpWebRequest request)
        {
            this.Request = request;
        }

        public RequestState(HttpWebRequest request, byte[] buffer) : this(request)
        {
            this.Buffer = buffer;
            this.BufferSize = buffer.Count();
        }
    }*/
}