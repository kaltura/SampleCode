using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrapperCielo24.JSON
{
    public class ElementList
    {
        public int Version { get; set; }
        public string Language { get; set; }
        public int StartTime { get; set; }      // Milliseconds
        public int EndTime { get; set; }        // Milliseconds
        public List<Segment> Segments { get; set; }
    }

    public class Segment
    {
        public List<Sequence> Sequences { get; set; }
        public bool SpeakerChange { get; set; }
        public bool Interpolated { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string Style { get; set; }
    }

    public class Sequence
    {
        public List<Token> Tokens { get; set; }
        public bool Interpolated { get; set; }
        public int StartTime { get; set; }      // Milliseconds
        public int EndTime { get; set; }        // Milliseconds
        public float ConfidenceScore { get; set; }
        public string Style { get; set; }
    }

    public class Token
    {
        public TokenType Type { get; set; }
        public string TypeDisplay { get; set; }
        public bool Interpolated { get; set; }
        public int StartTime { get; set; }      // Milliseconds
        public int EndTime { get; set; }        // Milliseconds
        public List<Tag> Tags { get; set; }
        public string Style { get; set; }
    }
}
