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
        
        /* Performs a Login action. If useHeaders is true, puts username and password into HTTP headers */
        public Guid Login(string username, string password, bool useHeaders=false)
        {
            this.AssertArgument(username, "Username");
            this.AssertArgument(password, "Password");

            Dictionary<string, string> queryDictionary = new Dictionary<string, string>();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            queryDictionary.Add("v", VERSION.ToString());

            if (!useHeaders)
            {
                queryDictionary.Add("username", username);
                queryDictionary.Add("password", password);
            }
            else
            {
                headers.Add("x-auth-user", username);
                headers.Add("x-auth-key", password);
            }

            Uri requestUri = Utils.BuildUri(BASE_URI, LOGIN_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT, headers);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            return new Guid(response["ApiToken"]);
        }

        /* Performs a Login action. If useHeaders is true, puts securekey into HTTP headers */
        public Guid Login(string username, Guid securekey, bool useHeaders = false)
        {
            this.AssertArgument(username, "Username");

            Dictionary<string, string> queryDictionary = new Dictionary<string, string>();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            queryDictionary.Add("v", VERSION.ToString());

            if (!useHeaders)
            {
                queryDictionary.Add("username", username);
                queryDictionary.Add("securekey", securekey.ToString("N"));
            }

            else
            {
                headers.Add("x-auth-user", username);
                headers.Add("x-auth-securekey", securekey.ToString("N"));
            }

            Uri requestUri = Utils.BuildUri(BASE_URI, LOGIN_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT, headers);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);
            
            return new Guid(response["ApiToken"]);
        }

        /* Performs a Logout action */
        public void Logout(Guid apiToken)
        {
            Dictionary<string, string> queryDictionary = this.InitAccessReqDict(apiToken);
            Uri requestUri = Utils.BuildUri(BASE_URI, LOGOUT_PATH, queryDictionary);
            WebUtils web = new WebUtils();
            web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT); // Nothing returned
        }

        /* Updates password */
        public void UpdatePassword(Guid apiToken, string accountPassword)
        {
            this.AssertArgument(accountPassword, "New Password");

            Dictionary<string, string> queryDictionary = this.InitAccessReqDict(apiToken);
            queryDictionary.Add("account_password", accountPassword);

            Uri requestUri = Utils.BuildUri(BASE_URI, UPDATE_PASSWORD_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            // TODO: returns a MISSING_PARAMETERS error, although everything is supplied

            web.HttpRequest(requestUri, HttpMethod.POST, WebUtils.BASIC_TIMEOUT); // Nothing returned
        }

        /* Returns a new Secure API key */
        public Guid GenerateAPIKey(Guid apiToken, string username, bool forceNew = false)
        {
            this.AssertArgument(username, "Username/Account ID");

            Dictionary<string, string> queryDictionary = this.InitAccessReqDict(apiToken);
            queryDictionary.Add("account_id", username);
            queryDictionary.Add("force_new", forceNew.ToString());

            Uri requestUri = Utils.BuildUri(BASE_URI, GENERATE_API_KEY_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            return new Guid(response["ApiKey"]);
        }

        /* Deactivates the supplied Secure API key */
        public void RemoveAPIKey(Guid apiToken, Guid apiSecurekey)
        {
            Dictionary<string, string> queryDictionary = this.InitAccessReqDict(apiToken);
            queryDictionary.Add("api_securekey", apiSecurekey.ToString("N"));

            Uri requestUri = Utils.BuildUri(BASE_URI, REMOVE_API_KEY_PATH, queryDictionary);
            WebUtils web = new WebUtils();
            web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT); // Nothing returned
        }


        /// JOB CONTROL ///
        
        /* Creates a new job. Returns an array of Guids where 'JobId' is the 0th element and 'TaskId' is the 1st element */
        public Guid[] CreateJob(Guid apiToken, string jobName = null, string sourceLanguage = "en")
        {
            Dictionary<string, string> queryDictionary = this.InitAccessReqDict(apiToken);
            if (jobName != null) { queryDictionary.Add("job_name", jobName); }
            queryDictionary.Add("source_language", sourceLanguage);

            Uri requestUri = Utils.BuildUri(BASE_URI, CREATE_JOB_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            Guid jobId = new Guid(response["JobId"].ToString());
            Guid taskId = new Guid(response["TaskId"].ToString());

            return new Guid[] { jobId, taskId };
        }

        /* Authorizes a job with jobId */
        public void AuthorizeJob(Guid apiToken, Guid jobId)
        {
            Dictionary<string, string> queryDictionary = InitJobReqDict(apiToken, jobId);

            Uri requestUri = Utils.BuildUri(BASE_URI, AUTHORIZE_JOB_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT); // Nothing returned
        }

        /* Deletes a job with jobId */
        public Guid DeleteJob(Guid apiToken, Guid jobId)
        {
            Dictionary<string, string> queryDictionary = InitJobReqDict(apiToken, jobId);

            Uri requestUri = Utils.BuildUri(BASE_URI, DELETE_JOB_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT);
            
            // TODO: server returns Guid as a raw string, not inside of a json object

            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            return new Guid(response["TaskId"]);
        }

        public string GetJobInfo(Guid apiToken, Guid jobId)
        {
            Dictionary<string, string> queryDictionary = InitJobReqDict(apiToken, jobId);

            Uri requestUri = Utils.BuildUri(BASE_URI, GET_JOB_INFO_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            // TODO

            return null;
        }

        public string GetJobList(Guid apiToken)
        {            
            Dictionary<string, string> queryDictionary = new Dictionary<string, string>();
            queryDictionary.Add("v", VERSION.ToString());
            queryDictionary.Add("api_token", apiToken.ToString());

            Uri requestUri = Utils.BuildUri(BASE_URI, GET_JOB_LIST_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            // TODO

            return null;
        }

        public Guid AddMediaToJob(Guid apiToken, Guid jobId, Uri mediaUrl)
        {
            if (mediaUrl == null || mediaUrl.ToString().Equals("")) { throw new ArgumentException("Invalid Media URL"); }

            Dictionary<string, string> queryDictionary = InitJobReqDict(apiToken, jobId);
            queryDictionary.Add("media_url", Utils.EncodeUrl(mediaUrl));

            Uri requestUri = Utils.BuildUri(BASE_URI, ADD_MEDIA_TO_JOB_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            return new Guid(response["TaskId"]);
        }

        public Guid AddMediaToJob(Guid apiToken, Guid jobId, Stream fileStream)
        {
            if (fileStream == null) { throw new ArgumentException("Invalid File"); }

            Dictionary<string, string> queryDictionary = InitJobReqDict(apiToken, jobId);
            Uri requestUri = Utils.BuildUri(BASE_URI, ADD_MEDIA_TO_JOB_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            // TODO: Content-Type: video/mp4 ???
            string serverResponse = web.UploadMedia(requestUri, fileStream, "video/mp4");
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);
            
            return new Guid(response["TaskId"]);
        }

        public Guid AddEmbeddedMediaToJob(Guid apiToken, Guid jobId, Uri mediaUrl)
        {
            if (mediaUrl == null || mediaUrl.ToString().Equals("")) { throw new ArgumentException("Invalid Media URL"); }

            Dictionary<string, string> queryDictionary = InitJobReqDict(apiToken, jobId);
            queryDictionary.Add("media_url", Utils.EncodeUrl(mediaUrl));

            Uri requestUri = Utils.BuildUri(BASE_URI, ADD_EMBEDDED_MEDIA_TO_JOB_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            return new Guid(response["TaskId"]);
        }

        public Uri GetMedia(Guid apiToken, Guid jobId)
        {
            Dictionary<string, string> queryDictionary = InitJobReqDict(apiToken, jobId);

            Uri requestUri = Utils.BuildUri(BASE_URI, GET_MEDIA_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            // TODO: Url that's being returned does not match the Url used in AddMediaToJob. Returns "api.sandbox.cielo24.com"

            return new Uri(Utils.UnescapeString(response["MediaUrl"]));
        }

        public string GetTranscript(Guid apiToken, Guid jobId, CaptionOptions captionOptions = null)
        {
            Dictionary<string, string> queryDictionary = InitJobReqDict(apiToken, jobId);
            if (captionOptions != null) { queryDictionary.Add("caption_options", captionOptions.ToString()); }

            Uri requestUri = Utils.BuildUri(BASE_URI, GET_TRANSCRIPTION_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            return web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.DOWNLOAD_TIMEOUT); // Transcript text
        }

        public string GetCaption(Guid apiToken, Guid jobId, CaptionFormat captionFormat=CaptionFormat.SRT, CaptionOptions captionOptions = null)
        {
            Dictionary<string, string> queryDictionary = InitJobReqDict(apiToken, jobId);
            queryDictionary.Add("caption_format", captionFormat.ToString());
            if (captionOptions != null) { queryDictionary.Add("caption_options", captionOptions.ToString()); }

            Uri requestUri = Utils.BuildUri(BASE_URI, GET_CAPTION_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            return web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.DOWNLOAD_TIMEOUT); // Caption text
        }

        public string GetElementList(Guid apiToken, Guid jobId)
        {
            Dictionary<string, string> queryDictionary = InitJobReqDict(apiToken, jobId);

            Uri requestUri = Utils.BuildUri(BASE_URI, GET_ELEMENT_LIST_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);
            
            // TODO

            return null;
        }

        public string GetListOfElementLists(Guid apiToken, Guid jobId)
        {
            Dictionary<string, string> queryDictionary = InitJobReqDict(apiToken, jobId);

            Uri requestUri = Utils.BuildUri(BASE_URI, GET_LIST_OF_ELEMENT_LISTS_PATH, queryDictionary);
            WebUtils web = new WebUtils();

            string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT);
            Dictionary<string, string> response = Utils.DeserializeDictionary(serverResponse);

            // TODO

            return null;
        }


        /// PRIVATE HELPER METHODS ///
        
        /* Returns a dictionary with version, api_token and job_id key-value pairs (parameters used in almost every job-control action). */
        private Dictionary<string, string> InitJobReqDict(Guid apiToken, Guid jobId)
        {
            // Guids are non-nullable. No need to assert.
            Dictionary<string, string> queryDictionary = this.InitAccessReqDict(apiToken);
            queryDictionary.Add("job_id", jobId.ToString("N"));
            return queryDictionary;
        }

        /* Returns a dictionary with version and api_token key-value pairs (parameters used in almst every access-control action). */
        private Dictionary<string, string> InitAccessReqDict(Guid apiToken)
        {
            Dictionary<string, string> queryDictionary = new Dictionary<string, string>();
            queryDictionary.Add("v", VERSION.ToString());
            queryDictionary.Add("api_token", apiToken.ToString("N"));
            return queryDictionary;
        }

        /* If arg is invalid (null or empty), throws an ArgumentException */
        private void AssertArgument(string arg, string arg_name)
        {
            if (arg == null || arg.Equals("")) { throw new ArgumentException("Invalid " + arg_name); }
        }
    }
}