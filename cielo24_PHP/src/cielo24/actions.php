<?php

class Actions {

        public const int API_VERSION = 1;
        private string $BASE_URL = "https://api.cielo24.com";

        private const LOGIN_PATH = "/api/account/login";
        private const LOGOUT_PATH = "/api/account/logout";
        private const UPDATE_PASSWORD_PATH = "/api/account/update_password";
        private const GENERATE_API_KEY_PATH = "/api/account/generate_api_key";
        private const REMOVE_API_KEY_PATH = "/api/account/remove_api_key";
        private const CREATE_JOB_PATH = "/api/job/new";
        private const AUTHORIZE_JOB_PATH = "/api/job/authorize";
        private const DELETE_JOB_PATH = "/api/job/del";
        private const GET_JOB_INFO_PATH = "/api/job/info";
        private const GET_JOB_LIST_PATH = "/api/job/list";
        private const ADD_MEDIA_TO_JOB_PATH = "/api/job/add_media";
        private const ADD_EMBEDDED_MEDIA_TO_JOB_PATH = "/api/job/add_media_url";
        private const GET_MEDIA_PATH = "/api/job/media";
        private const PERFORM_TRANSCRIPTION = "/api/job/perform_transcription";
        private const GET_TRANSCRIPTION_PATH = "/api/job/get_transcript";
        private const GET_CAPTION_PATH = "/api/job/get_caption";
        private const GET_ELEMENT_LIST_PATH = "/api/job/get_elementlist";
        private const GET_LIST_OF_ELEMENT_LISTS_PATH = "/api/job/list_elementlists";


}