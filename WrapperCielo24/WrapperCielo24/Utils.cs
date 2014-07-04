using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace WrapperCielo24
{
    public class Utils
    {
        //public static abstract Guid ExtractGuid();

        public static Uri BuildUri(string baseUri, string actionPath, Dictionary<string, string> dictionary){
            string uriString = baseUri + actionPath + "?" + ToQuery(dictionary);
            return new Uri(uriString);
        }

        private static string ToQuery(Dictionary<string, string> dictionary){
            List<string> pairs = new List<string>();
            foreach(KeyValuePair<string, string> pair in dictionary){
                pairs.Add(pair.Key + "=" + pair.Value);
            }
            return String.Join("&", pairs);
        }
    }

    public abstract class JobId
    {
        public string Id { get; set; }
    }

    public enum CaptionFormat { SRT, SBV, DFXP, QT }

    /* CUSTOM EXCEPTIONS */
    public class AuthenticationException : WebException
    {
        private string errorDetails = null;
        public string ErrorDetails { get { return this.errorDetails; } }

        public AuthenticationException(string message, string errDet=null) : base(message)
        {
            this.errorDetails = errDet;
        }
    }
}
