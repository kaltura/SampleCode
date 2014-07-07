using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace WrapperCielo24
{
    public class Utils
    {
        /* Concatinates baseUri, actionPath and key-value pairs from the dictionary, returning a uri */
        public static Uri BuildUri(string baseUri, string actionPath, Dictionary<string, string> dictionary){
            string uriString = baseUri + actionPath + "?" + ToQuery(dictionary);
            return new Uri(uriString);
        }

        /* Creates a query string from key-value pairs in the dictionary */
        private static string ToQuery(Dictionary<string, string> dictionary){
            List<string> pairs = new List<string>();
            foreach(KeyValuePair<string, string> pair in dictionary){
                pairs.Add(pair.Key + "=" + pair.Value);
            }
            return String.Join("&", pairs);
        }

        /* Takes a json string (of the form {"k1":"v1", "k2":"v2",.....}), and deserializes it into a dictionary */
        public static Dictionary<string, string> DeserializeDictionary(string json)
        {
            Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return dictionary;
        }

        /* Encodes the supplied Url into an escaped format */
        public static string EncodeUrl(Uri uri)
        {
            return Uri.EscapeUriString(uri.ToString());
        }

        /* Unescapes a string */
        public static string UnescapeString(string uriString)
        {
            return Uri.UnescapeDataString(uriString);
        }
    }

    public enum CaptionFormat { SRT, SBV, DFXP, QT }

}