using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

namespace WrapperCielo24
{
    /*private class SynchronousHttpWebRequest : HttpWebRequest
    {
        private static const TimeSpan BASIC_TIMEOUT = new TimeSpan(TimeSpan.TicksPerSecond * 60); // 60 seconds
        private static const TimeSpan DOWNLOAD_TIMEOUT = new TimeSpan(TimeSpan.TicksPerMinute * 5);  // 5 minutes
        private AutoResetEvent allDone2 = new AutoResetEvent(false);

        public void GetResponse()
        {

        }

        private void GetResponseCallback(IAsyncResult result)
        {
            RequestState state = (RequestState)result.AsyncState;
            WebResponse response = state.Request.EndGetResponse(result);
            response.ToString();
        }

        private void GetRequestStreamCallback(IAsyncResult result)
        {
            RequestState state = (RequestState)result.AsyncState;
            Stream postStream = state.Request.EndGetRequestStream(result);
            postStream.Write(state.Buffer, 0, state.BufferSize);
            postStream.Dispose();
        }

    }*/

    class WebUtils
    {
        // Progress events/properties?
        // private static AutoResetEvent allDone = new AutoResetEvent(false);
        private static const TimeSpan BASIC_TIMEOUT = new TimeSpan(TimeSpan.TicksPerSecond * 60); // 60 seconds
        private static const TimeSpan DOWNLOAD_TIMEOUT = new TimeSpan(TimeSpan.TicksPerMinute * 5);  // 5 minutes
        

        /* A synchronous HTTP GET method that returns data received from the sever */
        public void HttpGet(Uri uri)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            request.Method = "GET";
            //RequestState state = new RequestState(request);
            IAsyncResult asyncResult = request.BeginGetResponse(null, null); // Begin getting response
            asyncResult.AsyncWaitHandle.WaitOne(BASIC_TIMEOUT); // Wait for 60 seconds for the above action to complete
            if(asyncResult.IsCompleted)
            {
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asyncResult); // End getting response
                Stream stream = response.GetResponseStream();
                byte[] byteResponse = new byte[stream.Length];
                stream.Read(byteResponse, 0, byteResponse.Length);
                UTF8Encoding enc = new UTF8Encoding();
                string str = enc.GetString(byteResponse, 0, byteResponse.Length);
            }

            //allDone.WaitOne(BASIC_TIMEOUT);
        }

        public void HttpPost(Uri uri)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
        }

        private void GetResponseCallback(IAsyncResult result)
        {
            RequestState state = (RequestState)result.AsyncState;
            WebResponse response = state.Request.EndGetResponse(result);
            response.ToString();
        }

        private void GetRequestStreamCallback(IAsyncResult result)
        {
            RequestState state = (RequestState)result.AsyncState;
            Stream postStream = state.Request.EndGetRequestStream(result);
            postStream.Write(state.Buffer, 0, state.BufferSize);
            postStream.Dispose();
        }
    }

    private class RequestState
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
    }
}
