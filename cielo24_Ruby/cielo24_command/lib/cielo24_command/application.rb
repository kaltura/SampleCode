module Cielo24Command

  require 'thor'
  require 'cielo24'

  class Application < Thor

    include Cielo24

    ENV["THOR_COLUMNS"] = "300" # Otherwise descriptions/help gets truncated (...)

    # ALWAYS REQUIRED:
    username_option = [:u, :required => true, :desc => "cielo24 username", :banner => "username"]
    password_option = [:p, :required => false, :desc => "cielo24 password", :banner => "password"]
    securekey_option = [:k, :required => false, :desc => "The API Secure Key", :banner => "securekey"]
    api_token_option = [:N, :required => false, :desc => "The API token of the current session", :banner => "token"]
    server_url_option =[:s, :required => false, :desc => "cielo24 server URL [https://api.cielo24.com]", :banner => "server_url"]
    # JOB CONTROL:
    job_id_option = [:job_id, :aliases => "j", :required => true, :desc => "Job Id"]
    media_url_option = [:media_url, :aliases => 'm', :required => false, :desc => "Media URL"]
    media_file_option = [:media_file, :aliases => 'M', :required => false, :desc => "Local media file"]
    # OTHER OPTIONS:
    header_option = [:header, :aliases => 'h', :required => false, :desc => "Use headers", :default => false, :type => :boolean]
    fidelity_option = [:fidelity, :aliases => 'f', :required => true, :desc => "Fidelity " + Fidelity.all]
    priority_option = [:priority, :aliases => 'p', :required => true, :desc => "Priority " + Priority.all]
    source_language_option = [:source, :aliases => 's', :required => true, :desc => "Source language " + Language.all]
    target_language_option = [:target, :aliases => 't', :required => true, :desc => "Target language " + Language.all]
    job_name_option = [:job_name, :aliases => 'n', :required => false, :desc => "Job name"]
    job_options_option = [:job_options, :aliases => 'O', :required => false, :desc => "Job option"]
    turn_around_hours_option = [:turn_around_hours, :aliases => 'h', :required => false, :desc => "Turn around hours"]
    callback_url_option = [:callback_url, :aliases => 'c', :required => false, :desc => "Callback URL"]
    caption_format_option = [:caption_format, :aliases => 'c', :required => true, :desc => "Caption format " + CaptionFormat.all]
    elementlist_version_option = [:elementlist_version, :aliases => 'e', :required => false, :desc => "ElementList Version"]
    force_new_option = [:force_new, :aliases => 'F', :required => false, :desc => "Force new key"]
    new_password_option = [:new_password, :aliases => 'k', :required => true, :desc => "New password"]

    desc "login", 'Performs a login action'
    option *username_option
    option *password_option
    option *securekey_option
    option *header_option
    def login

    end

    desc "logout", 'Performs a logout action'
    option :N, :required => true, :desc => "The API token of the current session", :banner => "token"
    def logout

    end

    desc "create", 'Creates a job'
    option *fidelity_option
    option *priority_option
    option *source_language_option
    option *target_language_option
    option *media_url_option
    option *media_file_option
    option *job_name_option
    option *job_options_option
    option *turn_around_hours_option
    option *callback_url_option
    def create

    end

    desc "delete", 'Delete a job'
    option *job_id_option
    def delete

    end

    desc "authorize", 'Authorize a job'
    option *job_id_option
    def authorize

    end

    desc "add_media_to_job", 'Add media to job'
    option *job_id_option
    option *media_url_option
    option *media_file_option
    def add_media_to_job

    end

    desc "add_embedded_media_to_job", 'Add embedded media to job'
    option *job_id_option
    option :media_url, :aliases => 'm', :required => true, :desc => "Media URL"
    def add_embedded_media_to_job

    end

    desc "list", 'Lists current jobs'
    def list

    end

    desc "list_elementlists", 'List ElementLists'
    option *job_id_option
    def list_elementlists

    end

    desc "get_caption", 'Get Caption'
    option *job_id_option
    option *caption_format_option
    option *elementlist_version_option
    def get_caption

    end

    desc "get_transcript", 'Get Transcript'
    option *job_id_option
    option *elementlist_version_option
    def get_transcript

    end

    desc "get_elementlist", 'Get ElementList for JobId'
    option *job_id_option
    def get_elementlist

    end

    desc "get_media", 'Get Media'
    option *job_id_option
    def get_media

    end

    desc "generate_api_key", 'Generate API Secure Key'
    option *force_new_option
    def generate_api_key

    end

    desc "remove_api_key", 'Remove API Secure Key'
    option :api_key, :aliases => 'k', :required => true, :desc => "The API Key to remove"
    def remove_api_key

    end

    desc "update_password", 'Update Password'
    option *new_password_option
    def update_password

    end

    desc "job_info", 'Get Job Info'
    option *job_id_option
    def job_info

    end

    # TODO: override help methods from Thor class to display options that are always required
    # TODO: Concatinate enums to produce list of allowed values in options

  end
end