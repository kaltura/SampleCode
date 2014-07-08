using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrapperCielo24
{
    public class TranscriptOptions
    {
        public int CharactersPerCaptionLine { get; set; }   // Default: 0
        public bool CreateParagraphs { get; set; }          // Default: true
        public DateTime? ElementListVersion { get; set; }   // Default: ""
        public string SpeakerChangeToken { get; set; }      // Default: ">>"
        public int NewLinesAfterParagraph { get; set; }     // Default: 2
        public int NewLinesAfterSentence { get; set; }      // Default: 0
        public bool RemoveDisfluencies { get; set; }        // Default: true
        public bool MaskProfanity { get; set; }             // Default: false
        public List<string> RemoveSoundsList { get; set; }  // Default: empty
        public bool RemoveSoundReferences { get; set; }     // Default: true
        public bool ReplaceSlang { get; set; }              // Default: false
        public char[] SoundBoundaries { get; set; }         // Default: ('[' , ']')
        public bool TimecodeEveryParagraph { get; set; }    // Default: true
        public string TimeCodeFormat { get; set; }          // Default: [%H:%M:%S.%f]
        public int TimeCodeInterval { get; set; }           // Default: 0
        public int TimeCodeOffset { get; set; }             // Default: 0

        public Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>();
        }
    }
}