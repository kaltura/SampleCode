using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace WrapperCielo24
{
    public class Utils
    {
        public static Guid ExtractGuid();

        public static Uri BuildUri(string baseUri, string actionPath, Dictionary<string, string> dictionary){
            string uriString = baseUri + actionPath + "?" + dictionary.AsQueryable().ToString();
            return new Uri(uriString);
        }
    }

    public abstract class JobId
    {
        public string Id { get; set; }
    }

    enum CaptionFormat { SRT, SBV, DFXP, QT }

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
