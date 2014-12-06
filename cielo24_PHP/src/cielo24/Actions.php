<?php

class Actions {

    public $BASE_URL = "https://api.cielo24.com";
    const API_VERSION = 1;

    const LOGIN_PATH = "/api/account/login";
    const LOGOUT_PATH = "/api/account/logout";
    const UPDATE_PASSWORD_PATH = "/api/account/update_password";
    const GENERATE_API_KEY_PATH = "/api/account/generate_api_key";
    const REMOVE_API_KEY_PATH = "/api/account/remove_api_key";
    const CREATE_JOB_PATH = "/api/job/new";
    const AUTHORIZE_JOB_PATH = "/api/job/authorize";
    const DELETE_JOB_PATH = "/api/job/del";
    const GET_JOB_INFO_PATH = "/api/job/info";
    const GET_JOB_LIST_PATH = "/api/job/list";
    const ADD_MEDIA_TO_JOB_PATH = "/api/job/add_media";
    const ADD_EMBEDDED_MEDIA_TO_JOB_PATH = "/api/job/add_media_url";
    const GET_MEDIA_PATH = "/api/job/media";
    const PERFORM_TRANSCRIPTION = "/api/job/perform_transcription";
    const GET_TRANSCRIPTION_PATH = "/api/job/get_transcript";
    const GET_CAPTION_PATH = "/api/job/get_caption";
    const GET_ELEMENT_LIST_PATH = "/api/job/get_elementlist";
    const GET_LIST_OF_ELEMENT_LISTS_PATH = "/api/job/list_elementlists";

    public function __construct() { }

    public function __construct1($base_url) {
        $this->BASE_URL = $base_url;
    }

    public function __call($name, $arguments) {
        // TODO
    }

    /// ACCESS CONTROL ///

    /* Performs a Login action. If useHeaders is true, puts username and password into HTTP headers */
    public function login($username, $password = null, $api_securekey = null, $use_headers = false) {
        $this->assert_argument($username, "Username");

        # Password or API Secure Key must be supplied but not both
        if ($password == null and $api_securekey == null) {
            throw new InvalidArgumentException("Password or API Secure Key must be supplied for login.");
        }

        $query_dict = $this->init_version_dict();
        $headers = array();

        if ($use_headers) {
            $headers["x-auth-user"] = $username;
            if ($password != null) {
                $headers["x-auth-key"] = $password;
            }
            if ($api_securekey != null) {
                $headers["x-auth-securekey"] = $api_securekey;
            }
        } else {
            $query_dict["username"] = $username;
            if ($password != null) {
                $query_dict["password"] = $password;
            }
            if ($api_securekey != null) {
                $query_dict["securekey"] = $api_securekey;
            }
        }

        $response = WebUtils::getJson($this->BASE_URL, Actions::LOGIN_PATH, HTTP_METH_GET, WebUtils::BASIC_TIMEOUT, $query_dict, $headers);
        return $response["ApiToken"];
    }

    /* Performs a Logout action */
    public function logout($api_token) {
        $query_dict = $this->init_access_req_dict($api_token);

        //TODO
        $request_uri = Utils.BuildUri(BASE_URL, LOGOUT_PATH, queryDictionary);
        web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT); // Nothing returned
    }

    /* Updates password */
    public function update_password($api_token, $new_password) {
        $this->assert_argument($new_password, "New Password");

        $query_dict = $this->init_access_req_dict($api_token);
        $query_dict["new_password"] = $new_password;

        // TODO
        $request_uri = Utils.BuildUri(BASE_URL, UPDATE_PASSWORD_PATH, queryDictionary);
        web.HttpRequest(requestUri, HttpMethod.POST, WebUtils.BASIC_TIMEOUT, Utils.ToQuery(queryDictionary)); // Nothing returned
    }

    /* Returns a new Secure API key */
    public function generate_API_key($api_token, $username, $force_new = false) {
        $this->assert_argument($username, "Username");

        $query_dict = $this->init_access_req_dict($api_token);
        $query_dict["account_id"] = $username;
        $query_dict["force_new"] = ($force_new) ? 'true' : 'false';

        // TODO
        $request_uri = Utils.BuildUri($this->BASE_URL, Actions::GENERATE_API_KEY_PATH, $query_dict);
        $server_response = web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT);
        $response = Utils.Deserialize<Dictionary<string, string>>($server_response);
        return $response["ApiKey"];
    }

    /* Deactivates the supplied Secure API key */
    public function remove_API_key($api_token, $api_securekey) {
        $query_dict = $this->init_access_req_dict($api_token);
        $query_dict["api_securekey"] = $api_securekey;

        //TODO
        $request_uri = Utils.BuildUri($this->BASE_URL, Actions::REMOVE_API_KEY_PATH, $query_dict);
        web.HttpRequest($request_uri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT); // Nothing returned
    }

    /// JOB CONTROL ///

    /* Creates a new job. Returns an array of Guids where 'JobId' is the 0th element and 'TaskId' is the 1st element */
    public function create_job($api_token, $job_name = null, $language = "en") {
        $query_dict = $this->init_access_req_dict($api_token);
        if($job_name) {
            $query_dict["job_name"] = $job_name;
        }
        $query_dict["language"] = $language;

        //TODO

        $request_uri = Utils.BuildUri($this->BASE_URL, Actions::CREATE_JOB_PATH, $query_dict);
        $server_response = web.HttpRequest($request_uri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT);
        $response = Utils.Deserialize<CreateJobResult>($server_response);

        return $response;
    }

    /* Authorizes a job with job_id */
    public function authorize_job($api_token, $job_id) {
        $query_dict = $this->init_job_req_dict($api_token, $job_id);

        //TODO
        $request_uri = Utils.BuildUri($this->BASE_URL, Actions::AUTHORIZE_JOB_PATH, $query_dict);
        web.HttpRequest($request_uri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT); // Nothing returned
    }

    /* Deletes a job with job_id */
    public function delete_job($api_token, $job_id) {
        //TODO
        $response = $this->get_job_response($api_token, $job_id, Actions::DELETE_JOB_PATH);
        return $response["TaskId"];
    }

    /* Gets information about a job with job_id */
    public function get_job_info($api_token, $job_id) {
        //TODO
    }

    /* Gets a list of jobs */
    public function get_job_list($api_token) {
        $query_dict = $this->init_access_req_dict($api_token);

        //TODO
        $request_uri = Utils.BuildUri($this->BASE_URL, Actions::GET_JOB_LIST_PATH, $query_dict);
        $server_response = web.HttpRequest($request_uri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT);
        $job_list = Utils.Deserialize<JobList>($server_response);
        return $job_list;
    }

    /* Uploads a file from fileStream to job with job_id */
    public function add_media_to_job($api_token, $job_id, $file_stream) {

    }

    /* Provides job with job_id a url to media */
    public function add_media_to_job1($api_token, $job_id, $media_url) {

    }

    /* Provides job with job_id a url to media */
    public function add_embedded_media_to_job($api_token, $job_id, $media_url) {

    }

    /* Helper method for AddMediaToJob and AddEmbeddedMediaToJob methods */
    private function sendM_media_url($api_token, $job_id, $media_url, $path) {

    }

    /* Returns a Uri to the media from job with job_id */
    public function get_media($api_token, $job_id) {

    }

    /* Makes a PerformTranscription call */
    public function perform_transcription($api_token,
                                         $job_id,
                                         $fidelity,
                                         $priority,
                                         $callback_uri = null,
                                         $turnaround_hours = null,
                                         $target_language = null,
                                         $options = null) {

    }

    /* Returns a transcript from a job with job_id */
    public function get_transcript($api_token, $job_id, $transcript_options = null) {

    }

    /* Returns a caption from a job with job_id OR if buildUri is true, returns a string representation of the uri */
    public function get_caption($api_token, $job_id, $caption_format, $caption_options = null) {

    }

    /* Returns an element list */
    public function get_element_list($api_token, $job_id, $element_list_version = null) {

    }

    /* Returns a list of elements lists */
    public function get_list_of_element_lists($api_token, $job_id) {

    }

    /// PRIVATE HELPER METHODS ///

    private function get_job_response($api_token, $job_id, $path) {
        return null;
    }

    /* Returns a dictionary with version, api_token and job_id key-value pairs (parameters used in almost every job-control action). */
    private function init_job_req_dict($api_token, $job_id) {
        $this->assert_argument($api_token, "Job ID");
        $dict = $this->init_access_req_dict($api_token);
        $dict["job_id"] = $job_id;
        return $dict;
    }

    /* Returns a dictionary with version and api_token key-value pairs (parameters used in almost every access-control action). */
    private function init_access_req_dict($api_token) {
        $this->assert_argument($api_token, "API Token");
        $dict = $this->init_version_dict();
        $dict["api_token"] = $api_token;
        return $dict;
    }

    /* Returns a dictionary with version key-value pair (parameter used in every action). */
    private function init_version_dict() {
        return array("v" => Actions::API_VERSION);
    }

    /* If arg is invalid (null or empty), throws an InvalidArgumentException */
    private function assert_argument($arg, $arg_name) {
        if ($arg == null) { throw new InvalidArgumentException("Invalid " . $arg_name); }
        elseif (gettype($arg) == "string" and strlen($arg) == 0) { throw new InvalidArgumentException("Invalid " . $arg_name); }
    }
}