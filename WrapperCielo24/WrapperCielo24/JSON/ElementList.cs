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

        public override string ToString()
        {
            string segments = "";
            foreach (Segment seg in this.Segments)
            {
                segments += seg.ToString() + "\n";
            }
            return "Version: " + this.Version +
                   "\nLanguage: " + this.Language +
                   "\nStart Time (ms): " + this.StartTime +
                   "\nEnd Time (ms): " + this.EndTime +
                   "\nSegments: \n" + segments;
        }
    }

    public class Segment
    {
        public List<Sequence> Sequences { get; set; }
        public bool SpeakerChange { get; set; }
        public bool Interpolated { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string Style { get; set; }

        public override string ToString()
        {
            string sequences = "";
            foreach (Sequence seq in this.Sequences)
            {
                sequences += seq.ToString() + "\n";
            }
            return "Speaker change: " + this.SpeakerChange +
                   "\nInterpolated: " + this.Interpolated +
                   "\nStart Time (ms): " + this.StartTime +
                   "\nEnd Time (ms): " + this.EndTime +
                   "\nStyle: " + this.Style +
                   "\nSequences: \n" + sequences;
        }
    }

    public class Sequence
    {
        public List<Token> Tokens { get; set; }
        public bool Interpolated { get; set; }
        public int StartTime { get; set; }      // Milliseconds
        public int EndTime { get; set; }        // Milliseconds
        public float ConfidenceScore { get; set; }
        public string Style { get; set; }

        public override string ToString()
        {
            string tokens = "";
            foreach (Token tok in this.Tokens)
            {
                tokens += tok.ToString() + "\n";
            }
            return "Confidence score: " + this.ConfidenceScore +
                   "\nInterpolated: " + this.Interpolated +
                   "\nStart Time (ms): " + this.StartTime +
                   "\nEnd Time (ms): " + this.EndTime +
                   "\nStyle: " + this.Style +
                   "\nTokens: \n" + tokens;
        }
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

        public override string ToString()
        {
            string tags = "";
            foreach (Tag tag in this.Tags)
            {
                tags += tag.ToString() + "\n";
            }
            return "Type display: " + this.TypeDisplay +
                   "\nInterpolated: " + this.Interpolated +
                   "\nType: " + this.Type +
                   "\nStart Time (ms): " + this.StartTime +
                   "\nEnd Time (ms): " + this.EndTime +
                   "\nStyle: " + this.Style +
                   "\nSequences: \n" + tags;
        }
    }
}
