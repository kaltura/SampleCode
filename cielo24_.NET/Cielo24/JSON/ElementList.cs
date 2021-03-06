﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cielo24.JSON
{
    public class ElementList : JsonBase
    {
        [JsonProperty("version")]
        public int Version { get; set; }
        [JsonProperty("start_time")]
        public int StartTime { get; set; }      // Milliseconds
        [JsonProperty("end_time")]
        public int EndTime { get; set; }        // Milliseconds
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("segments")]
        public List<Segment> Segments { get; set; }
        [JsonProperty("speakers")]
        public List<Speaker> Speakers { get; set; }
    }

    public class Segment : JsonBase
    {
        [JsonProperty("sequences")]
        public List<Sequence> Sequences { get; set; }
        [JsonProperty("speaker_change")]
        public bool SpeakerChange { get; set; }
        [JsonProperty("speaker_id")]
        public bool SpeakerId { get; set; }
        [JsonProperty("interpolated")]
        public bool Interpolated { get; set; }
        [JsonProperty("start_time")]
        public int StartTime { get; set; }
        [JsonProperty("end_time")]
        public int EndTime { get; set; }
    }

    public class Sequence : JsonBase
    {
        [JsonProperty("tokens")]
        public List<Token> Tokens { get; set; }
        [JsonProperty("interpolated")]
        public bool Interpolated { get; set; }
        [JsonProperty("start_time")]
        public int StartTime { get; set; }      // Milliseconds
        [JsonProperty("end_time")]
        public int EndTime { get; set; }        // Milliseconds
        [JsonProperty("confidence_score")]
        public float ConfidenceScore { get; set; }
    }

    public class Token : JsonBase
    {
        [JsonProperty("interpolated")]
        public bool Interpolated { get; set; }
        [JsonProperty("start_time")]
        public int StartTime { get; set; }      // Milliseconds
        [JsonProperty("end_time")]
        public int EndTime { get; set; }        // Milliseconds
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("type")]
        public TokenType Type { get; set; }
        [JsonProperty("display_as")]
        public string TypeDisplay { get; set; }
        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }
    }

    public class Speaker : JsonBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("gender")]
        public SpeakerGender Gender { get; set; }
    }

    public class ElementListVersion : JsonBase
    {
        [JsonProperty("version")]
        public DateTime Version { get; set; }
        [JsonProperty("iwp_name")]
        public string IWP { get; set; }
    }
}