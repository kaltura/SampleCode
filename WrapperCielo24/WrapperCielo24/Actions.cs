using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Principal;
using System.Net;

namespace WrapperCielo24
{
    public class Actions
    {
        private const string BASE_URI = "https://sandbox-dev.cielo24.com";
        public const int VERSION = 1;

        private const string LOGIN_PATH = "/api/account/login";
        private const string LOGOUT_PATH = "/api/account/logout";
        private const string UPDATE_PASSWORD_PATH = "/api/account/update_password";
        private const string GENERATE_API_KEY_PATH = "/api/account/generate_api_key";
        private const string REMOVE_API_KEY_PATH = "/api/account/remove_api_key";
        private const string CREATE_JOB_PATH = "/api/job/new";
        private const string AUTHORIZE_JOB_PATH = "/api/job/authorize";
        private const string DELETE_JOB_PATH = "/api/job/del";
        private const string GET_JOB_INFO_PATH = "/api/job/list";
        private const string GET_JOB_LIST_PATH = "/api/job/list";
        private const string ADD_MEDIA_TO_JOB_PATH = "/api/job/add_media";
        private const string ADD_EMBEDDED_MEDIA_TO_JOB_PATH = "/api/job/add_media_url";
        private const string GET_MEDIA_PATH = "/api/job/media";
        private const string GET_TRANSCRIPTION_PATH = "/api/job/get_transcript";
        private const string GET_CAPTION_PATH = "/api/job/get_caption";
        private const string GET_ELEMENT_LIST_PATH = "/api/job/get_elementlist";
        private const string GET_LIST_OF_ELEMENT_LISTS_PATH = "/api/job/list_elementlists";

        /// ACCESS CONTROL ///
        
        /* Performs a Login action. If headers is true, puts username and password into HTTP headers */
        public Guid Login(string username, string password, bool usingHeaders=false)
        {
            this.AssertArgument(username, "Username");
            this.AssertArgument(password, "Password");

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            dictionary.Add("v", VERSION.ToString());

            if (!usingHeaders)
            {
                dictionary.Add("username", username);
                dictionary.Add("password", password);
            }
            else
            {
                headers.Add("x-auth-user", username);
                headers.Add("x-auth-key", password);
            }

            Uri requestUri = Utils.BuildUri(BASE_URI, LOGIN_PATH, dictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, headers);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            return new Guid(response["ApiToken"]);
        }

        /* Performs a Login action. If headers is true, puts securekey into HTTP header */
        public Guid Login(string username, Guid securekey, bool usingHeaders = false)
        {
            this.AssertArgument(username, "Username");

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            dictionary.Add("v", VERSION.ToString());

            if (!usingHeaders)
            {
                dictionary.Add("username", username);
                dictionary.Add("securekey", securekey.ToString("N"));
            }

            else
            {
                headers.Add("x-auth-user", username);
                headers.Add("x-auth-securekey", securekey.ToString("N"));
            }

            Uri requestUri = Utils.BuildUri(BASE_URI, LOGIN_PATH, dictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, headers);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);
            
            return new Guid(response["ApiToken"]);
        }

        /* Performs a Logout action */
        public void Logout(Guid apiToken)
        {
            Dictionary<string, string> dictionary = this.InitAccessReqDict(apiToken);
            Uri requestUri = Utils.BuildUri(BASE_URI, LOGOUT_PATH, dictionary);
            WebUtils web = new WebUtils();

            web.HttpRequest(requestUri, HttpMethod.GET); // Ignore response
        }

        /* Updates password */
        public void UpdatePassword(Guid apiToken, string accountPassword)
        {
            this.AssertArgument(accountPassword, "New Password");

            Dictionary<string, string> dictionary = this.InitAccessReqDict(apiToken);
            dictionary.Add("account_password", accountPassword);

            Uri requestUri = Utils.BuildUri(BASE_URI, UPDATE_PASSWORD_PATH, dictionary);
            WebUtils web = new WebUtils();

            web.HttpRequest(requestUri, HttpMethod.POST); // Ignore response
        }

        /* Returns a Secure API key */
        public Guid GenerateAPIKey(Guid apiToken, string username, bool forceNew = false)
        {
            this.AssertArgument(username, "Username/Account ID");

            Dictionary<string, string> dictionary = this.InitAccessReqDict(apiToken);
            dictionary.Add("username", username);
            dictionary.Add("force_new", forceNew.ToString());

            Uri requestUri = Utils.BuildUri(BASE_URI, GENERATE_API_KEY_PATH, dictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            return new Guid(response["ApiKey"]);
        }

        /* Removes the supplied API key */
        public Guid RemoveAPIKey(Guid apiToken, Guid apiSecurekey)
        {
            Dictionary<string, string> dictionary = this.InitAccessReqDict(apiToken);
            dictionary.Add("api_securekey", apiSecurekey.ToString());

            Uri requestUri = Utils.BuildUri(BASE_URI, REMOVE_API_KEY_PATH, dictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            return new Guid(response["ApiKey"]);
        }


        /// JOB CONTROL ///
        
        public string CreateJob(Guid apiToken, string jobName = null, string sourceLanguage = "en")
        {
            Dictionary<string, string> dictionary = this.InitAccessReqDict(apiToken);
            dictionary.Add("source_language", sourceLanguage);
            if (jobName != null) { dictionary.Add("job_name", jobName); }

            Uri requestUri = Utils.BuildUri(BASE_URI, CREATE_JOB_PATH, dictionary);
            WebUtils web = new WebUtils();
            
            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            // TODO

            return null;
        }

        public void AuthorizeJob(Guid apiToken, Guid jobId)
        {
            Dictionary<string, string> dictionary = InitJobReqDict(apiToken, jobId);

            Uri requestUri = Utils.BuildUri(BASE_URI, AUTHORIZE_JOB_PATH, dictionary);
            WebUtils web = new WebUtils();

            web.HttpRequest(requestUri, HttpMethod.GET); // Ignore response
        }

        public Guid DeleteJob(Guid apiToken, Guid jobId)
        {
            Dictionary<string, string> dictionary = InitJobReqDict(apiToken, jobId);

            Uri requestUri = Utils.BuildUri(BASE_URI, DELETE_JOB_PATH, dictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            return new Guid(response["TaskId"]);
        }

        public string GetJobInfo(Guid apiToken, Guid jobId)
        {
            Dictionary<string, string> dictionary = InitJobReqDict(apiToken, jobId);

            Uri requestUri = Utils.BuildUri(BASE_URI, GET_JOB_INFO_PATH, dictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            // TODO

            return null;
        }

        public string GetJobList(Guid apiToken)
        {            
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("v", VERSION.ToString());
            dictionary.Add("api_token", apiToken.ToString());

            Uri requestUri = Utils.BuildUri(BASE_URI, GET_JOB_LIST_PATH, dictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            // TODO

            return null;
        }

        public Guid AddMediaToJob(Guid apiToken, Guid jobId, Uri mediaUrl)
        {
            Dictionary<string, string> dictionary = InitJobReqDict(apiToken, jobId);

            if (mediaUrl == null || mediaUrl.ToString().Equals("")) { throw new ArgumentException("Invalid Media URL"); }
            dictionary.Add("media_url", mediaUrl.ToString());

            Uri requestUri = Utils.BuildUri(BASE_URI, ADD_MEDIA_TO_JOB_PATH, dictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            return new Guid(response["TaskId"]);
        }

        public Guid AddMediaToJob(Guid apiToken, Guid jobId, StreamReader localFile)
        {
            Dictionary<string, string> dictionary = InitJobReqDict(apiToken, jobId);

            if (localFile == null) { throw new ArgumentException("Invalid File"); }

            Uri requestUri = Utils.BuildUri(BASE_URI, ADD_MEDIA_TO_JOB_PATH, dictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.POST);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            // TODO
            // POST request

            return new Guid("");
        }

        public Guid AddEmbeddedMediaToJob(Guid apiToken, Guid jobId, Uri mediaUrl)
        {
            if (mediaUrl == null || mediaUrl.ToString().Equals("")) { throw new ArgumentException("Invalid Media URL"); }

            Dictionary<string, string> dictionary = InitJobReqDict(apiToken, jobId);
            dictionary.Add("media_url", mediaUrl.ToString());

            Uri requestUri = Utils.BuildUri(BASE_URI, ADD_EMBEDDED_MEDIA_TO_JOB_PATH, dictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            return new Guid(response["TaskId"]);
        }

        public Uri GetMedia(Guid apiToken, Guid jobId)
        {
            Dictionary<string, string> dictionary = InitJobReqDict(apiToken, jobId);

            Uri requestUri = Utils.BuildUri(BASE_URI, GET_MEDIA_PATH, dictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            return new Uri(response["MediaUrl"]);
        }

        public string GetTranscript(Guid apiToken, Guid jobId, CaptionOptions captionOptions = null)
        {
            Dictionary<string, string> dictionary = InitJobReqDict(apiToken, jobId);
            if (captionOptions != null) { dictionary.Add("caption_options", captionOptions.ToString()); }

            Uri requestUri = Utils.BuildUri(BASE_URI, GET_TRANSCRIPTION_PATH, dictionary);
            WebUtils web = new WebUtils();

            return web.HttpRequest(requestUri, HttpMethod.GET); // Transcript text
        }

        public string GetCaption(Guid apiToken, Guid jobId, CaptionFormat captionFormat=CaptionFormat.SRT, CaptionOptions captionOptions = null)
        {
            Dictionary<string, string> dictionary = InitJobReqDict(apiToken, jobId);
            dictionary.Add("caption_format", captionFormat.ToString());
            if (captionOptions != null) { dictionary.Add("caption_options", captionOptions.ToString()); }

            Uri requestUri = Utils.BuildUri(BASE_URI, GET_CAPTION_PATH, dictionary);
            WebUtils web = new WebUtils();

            return web.HttpRequest(requestUri, HttpMethod.GET); // Transcript text
        }

        public string GetElementList(Guid apiToken, Guid jobId)
        {
            Dictionary<string, string> dictionary = InitJobReqDict(apiToken, jobId);

            Uri requestUri = Utils.BuildUri(BASE_URI, GET_ELEMENT_LIST_PATH, dictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);
            
            // TODO

            return null;
        }

        public string GetListOfElementLists(Guid apiToken, Guid jobId)
        {
            Dictionary<string, string> dictionary = InitJobReqDict(apiToken, jobId);

            Uri requestUri = Utils.BuildUri(BASE_URI, GET_LIST_OF_ELEMENT_LISTS_PATH, dictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            // TODO

            return null;
        }


        /// PRIVATE HELPER METHODS ///
        
        /* Returns a dictionary with version, api_token and job_id key-value pairs (parameters used in almost every job-control action). */
        private Dictionary<string, string> InitJobReqDict(Guid apiToken, Guid jobId)
        {
            // Guids are non-nullable. No need to assert.
            Dictionary<string, string> dictionary = this.InitAccessReqDict(apiToken);
            dictionary.Add("job_id", jobId.ToString());
            return dictionary;
        }

        /* Returns a dictionary with version and api_token key-value pairs (parameters used in almst every access-control action). */
        private Dictionary<string, string> InitAccessReqDict(Guid apiToken)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("v", VERSION.ToString());
            dictionary.Add("api_token", apiToken.ToString("N"));
            return dictionary;
        }

        /* If arg is invalid (null or empty), throws an ArgumentException */
        private void AssertArgument(string arg, string arg_name)
        {
            if (arg == null || arg.Equals("")) { throw new ArgumentException("Invalid " + arg_name); }
        }
    }
}