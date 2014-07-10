using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandLineTool
{
    class VerbOptions
    {
        [VerbOption("login")]
        public Options login { get; set; }
        [VerbOption("logout")]
        public Options logout { get; set; }
        [VerbOption("create")]
        public Options create { get; set; }
        [VerbOption("delete")]
        public Options delete { get; set; }
        [VerbOption("authorize")]
        public Options authorize { get; set; }
        [VerbOption("add_media_to_job")]
        public Options add_media_to_job { get; set; }
        [VerbOption("add_embedded_media_to_job")]
        public Options add_embedded_media_to_job { get; set; }
        [VerbOption("list")]
        public Options list { get; set; }
        [VerbOption("list_elementlists")]
        public Options list_elementlists { get; set; }
        [VerbOption("get_caption")]
        public Options get_caption { get; set; }
        [VerbOption("get_transcript")]
        public Options get_transcript { get; set; }
        [VerbOption("get_elementlist")]
        public Options get_elementlist { get; set; }
        [VerbOption("get_media")]
        public Options get_media { get; set; }
        [VerbOption("generate_api_key")]
        public Options generate_api_key { get; set; }
        [VerbOption("remove_api_key")]
        public Options remove_api_key { get; set; }
        [VerbOption("update_password")]
        public Options update_password { get; set; }
        [VerbOption("job_info")]
        public Options job_info { get; set; }
    }
}