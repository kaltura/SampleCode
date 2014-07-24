module Cielo24
  class WebUtils

    require 'json'
    require 'httpclient'
    require 'timeout'
    include Timeout
    include JSON

    VERIFY_MODE = nil
    BASIC_TIMEOUT = 60
    DOWNLOAD_TIMEOUT = 300
    @@LAST_URL = "none" # For logging purposes

    def self.LAST_URL
      return @@LAST_URL
    end

    def self.get_json(uri, method, timeout, query=nil, headers=nil, body=nil)
      response = http_request(uri, method, timeout, query, headers, body)
      return JSON.parse(response)
    end

    def self.http_request(uri, method, timeout, query=nil, headers = nil, body = nil)
      http_client = HTTPClient.new
      http_client.cookie_manager = nil

      # Timeout block:
      begin
        # Error is raised if the following block fails to execute in 'timeout' sec:
        Timeout.timeout(timeout) {

          response = http_client.request(method, uri, query, body, headers, nil)
          # Handle web errors
          if response.status_code == 200 or response.status_code == 204
            return response.body
          else
            json = JSON.parse(response.body)
            raise WebError.new(json["ErrorType"], json["ErrorComment"])
          end

        }
      rescue Timeout::Error
        raise StandardError.new("The HTTP session has timed out.")
      end

      @@LAST_URL = uri + (query.nil? ? "" : "?" + URI.encode_www_form(query))
    end
  end

  class WebError < StandardError
    attr_reader :type
    def initialize(type, comment)
      super comment
      @type = type
    end

    def to_s
      return @type + " - " + super.to_s
    end
  end
end