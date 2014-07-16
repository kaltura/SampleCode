module Cielo24
  # Methods for authenticating.
  module Authentication

    @@VERSION = 1
    @@BASE_URL = "https://sandbox-dev.cielo24.com"

    @@LOGIN_PATH = "/api/account/login"
    @@LOGOUT_PATH = "/api/account/logout"
    @@UPDATE_PASSWORD_PATH = "/api/account/update_password"
    @@GENERATE_API_KEY_PATH = "/api/account/generate_api_key"
    @@REMOVE_API_KEY_PATH = "/api/account/remove_api_key"
    @@CREATE_JOB_PATH = "/api/job/new"
    @@AUTHORIZE_JOB_PATH = "/api/job/authorize"
    @@DELETE_JOB_PATH = "/api/job/del"
    @@GET_JOB_INFO_PATH = "/api/job/info"
    @@GET_JOB_LIST_PATH = "/api/job/list"
    @@ADD_MEDIA_TO_JOB_PATH = "/api/job/add_media"
    @@ADD_EMBEDDED_MEDIA_TO_JOB_PATH = "/api/job/add_media_url"
    @@GET_MEDIA_PATH = "/api/job/media"
    @@PERFORM_TRANSCRIPTION = "/api/job/perform_transcription"
    @@GET_TRANSCRIPTION_PATH = "/api/job/get_transcript"
    @@GET_CAPTION_PATH = "/api/job/get_caption"
    @@GET_ELEMENT_LIST_PATH = "/api/job/get_elementlist"
    @@GET_LIST_OF_ELEMENT_LISTS_PATH = "/api/job/list_elementlists"

    def log_in(options)
      data = get_json("/api/account/login", options)
      data["ApiToken"]
    end

    def login(username, password=nil, api_securekey=nil, use_headers=false)
      # Password or API Secure Key must be supplied but not both
      if (password.nil? and api_securekey.nil?) or (!password.nil? and !api_securekey.nil?)
        raise("Only password or API secure must be supplied for login.")
      end
      assert_argument(username, "Username")

      query_hash = init_version_dict
      headers = Hash.new

      if use_headers
        query_hash[:username] = username
        query_hash[:password] = password if (!password.nil?)
        query_hash[:securekey] = api_securekey if (!api_securekey.nil?)
      else
        headers[:'x-auth-user'] = username
        headers[:'x-auth-key'] = password if (!password.nil?)
        headers[:'x-auth-securekey'] = api_securekey if (!api_securekey.nil?)
      end

      #Uri requestUri = Utils.BuildUri(BASE_URL, LOGIN_PATH, queryDictionary)
      #WebUtils web = new WebUtils()
      #string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT, headers)
      #Dictionary<string, string> response = Utils.Deserialize<Dictionary<string, string>>(serverResponse)
      #return new Guid(response["ApiToken"])
    end

    def logout(api_token)
      query_hash = init_access_req_dict(apiToken)

      #Uri requestUri = Utils.BuildUri(BASE_URL, LOGOUT_PATH, queryDictionary);
      #WebUtils web = new WebUtils();
      #web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT); // Nothing returned
    end

    def update_password(api_token, new_password)
      assert_argument(new_password, "New Password")
      query_hash = init_access_req_dict(api_token)
      query_hash[:new_password] = new_password

      #Uri requestUri = Utils.BuildUri(BASE_URL, UPDATE_PASSWORD_PATH, null);
      #WebUtils web = new WebUtils();
      #web.HttpRequest(requestUri, HttpMethod.POST, WebUtils.BASIC_TIMEOUT, Utils.ToQuery(queryDictionary)); // Nothing returned
    end

    def generate_api_key(api_token, username, force_new = false)
      assert_argument(username, "Username/Account ID")
      query_hash = init_access_req_dict(api_token)
      query_hash[:account_id] = username
      query_hash[:force_new] = force_new

      #Uri requestUri = Utils.BuildUri(BASE_URL, GENERATE_API_KEY_PATH, queryDictionary);
      #WebUtils web = new WebUtils();
      #string serverResponse = web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT);
      #Dictionary<string, string> response = Utils.Deserialize<Dictionary<string, string>>(serverResponse);
      #return new Guid(response["ApiKey"]);
    end

    def remove_api_key(api_token, api_securekey)
      query_hash = init_access_req_dict(api_token)
      query_hash[:api_securekey] = api_securekey

      #Uri requestUri = Utils.BuildUri(BASE_URL, REMOVE_API_KEY_PATH, queryDictionary);
      #WebUtils web = new WebUtils();
      #web.HttpRequest(requestUri, HttpMethod.GET, WebUtils.BASIC_TIMEOUT); // Nothing returned
    end

    private # PRIVATE HELPER METHODS

    def init_job_req_dict(api_token, job_id)
      assert_argument(job_id, "Job ID")
      init_access_req_dict(api_token)[:job_id] = job_id
    end

    def init_access_req_dict(api_token)
      assert_argument(api_token, "API Token")
      init_version_dict()[:api_token] = api_token
    end

    def init_version_dict()
      {v: @@VERSION}
    end

    def assert_argument(arg, arg_name)
      if (arg.nil? or arg.length != 0)
        raise("Invalid argument: " + arg_name)
      end
    end
  end
end