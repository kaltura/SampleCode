using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrapperCielo24
{
    public class CaptionOptions
    {
        public Uri BuildUrl { get; set; }
        public int CharactersPerCaptionLine { get; set; }   // Default: 42
        public int CaptionWordsMin { get; set; }            // Default: 1
        public bool CaptionBySentence { get; set; }         // Default: true
        public string DfxpHeader { get; set; }              // Default: ""
        public bool DisallowDangling { get; set; }          // Default: false
        public string EffectsSpeaker { get; set; }          // Default: "Effects"
        public bool DisplaySpeakerId { get; set; }          // Default: "name" ["no","number", "name"]
        public DateTime ElementListVersion { get; set; }    // Default: ""
        public string SpeakerChangeToken { get; set; }      // Default: ">>"
        public bool ForceCase { get; set; }                 // Default: "" ["upper", "lower", ""]
        public bool IncludeDfxpMetadata { get; set; }       // Default: true
        public int LayoutTargetCaptionLengthMs { get; set; }// Default: 5000
        public bool LineBreakOnSentence { get; set; }       // Default: false
        public int LinesPerCaption { get; set; }            // Default: 2
        public bool MaskProfanity { get; set; }             // Default: false
        public int MaximumCaptionDuration { get; set; }     // Default: none
        public int MergeGapInterval { get; set; }           // Default: 1000
        public int MinimumCaptionLengthMs { get; set; }     // Default: none
        //public bool RemoveSoundReferences { get; set; }     // Default: true ???
        public int MinimumMergeGapInterval { get; set; }    // Default: 0
        public bool QtSeamless { get; set; }                // Default: false
        public bool RemoveDisfluences { get; set; }         // Default: true
        public List<string> RemoveSoundsList { get; set; }  // Default: empty ["UNKNOWN", "INAUDIBLE", "CROSSTALK", "MUSIC", "NOISE", "LAUGH", "COUGH", "FOREIGN", "SOUND", "BLANK_AUDIO"]
        public bool RemoveSoundReferences { get; set; }     // Default: false
        public bool ReplaceSlang { get; set; }              // Default: false
        public int SilenceMaxMs { get; set; }               // Default: 2000
        public bool SingleSpeakerPerCaption { get; set; }   // Default: false
        public char[] SoundBoundaries { get; set; }         // Default: ('[' , ']')
        public int SoundThreshold { get; set; }             // Default: none
        public bool SoundTokensByCaption { get; set; }      // Default: false
        public bool SoundTokensByLine { get; set; }         // Default: false
        public List<string> SoundTokensByCaptionList { get; set; }  // Default: ["BLANK_AUDIO", "MUSIC"]
        public List<string> SoundTokensByLineList { get; set; }     // Default: ["BLANK_AUDIO", "MUSIC"]
        public bool SpeakerOnNewLine { get; set; }          // Default: true
        public string SrtFormat { get; set; }               // Default: "{caption_number:d}\n{start_hour:02d}:{start_minute:02d}:{start_second:02d},{start_millisecond:03d} --> {end_hour:02d}:{end_minute:02d}:{end_second:02d},{end_millisecond:03d}\n{caption_text}\n\n"
        public bool SrtPrintCaptionNumbers { get; set; }    // Default: true
        public bool StripSquareBrackets { get; set; }       // Default: false
        public bool Utf8Mark { get; set; }                  // Default: false
    }
}
