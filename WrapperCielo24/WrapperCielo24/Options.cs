using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using WrapperCielo24.JSON;

namespace WrapperCielo24
{
    /* Options found in both Transcript and Caption options */
    public abstract class BaseOptions : IQueryConvertible
    {
        // DEFAULTS //
        private static readonly char[] soundBoundariesDefault = { '[', ']' };

        [QueryName("characters_per_caption_line")]
        public int CharactersPerCaptionLine { get; set; }   // Default: 0
        [QueryName("elementlist_version")]
        public DateTime? ElementListVersion { get; set; }   // Default: ""
        [QueryName("emit_speaker_change_token_as")]
        public string SpeakerChangeToken { get; set; }      // Default: ">>"
        [QueryName("mask_profanity")]
        public bool MaskProfanity { get; set; }             // Default: false
        [QueryName("remove_sounds_list")]
        public List<Tag> RemoveSoundsList { get; set; }     // Default: empty
        [QueryName("remove_sound_references")]
        public bool RemoveSoundReferences { get; set; }     // Default: true
        [QueryName("replace_slang")]
        public bool ReplaceSlang { get; set; }              // Default: false
        [QueryName("sound_boundaries")]
        public char[] SoundBoundaries { get; set; }         // Default: ('[' , ']')

        public BaseOptions(int charactersPerCaptionLine = 0, DateTime? elementListVersion = null, string speakerChangeToken = ">>", bool maskProfanity = false,
            List<Tag> removeSoundsList = null, bool removeSoundReferences = true, bool replaceSlang = false, char[] soundBoundaries = null)
        {
            this.CharactersPerCaptionLine = charactersPerCaptionLine;
            this.ElementListVersion = elementListVersion;
            this.SpeakerChangeToken = speakerChangeToken;
            this.MaskProfanity = maskProfanity;
            this.RemoveSoundsList = (removeSoundsList == null) ? new List<Tag>() : removeSoundsList;
            this.RemoveSoundReferences = removeSoundReferences;
            this.ReplaceSlang = replaceSlang;
            this.SoundBoundaries = (soundBoundaries == null) ? soundBoundariesDefault : soundBoundaries;
        }

        public virtual Dictionary<string, string> GetDictionary()
        {
            Dictionary<string, string> queryDictionary = new Dictionary<string, string>();
            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                QueryName key = (QueryName)property.GetCustomAttributes(false).First();
                object value = property.GetValue(this, null);
                queryDictionary.Add(key.Name, GetStringValue(value));
            }
            return queryDictionary;
        }

        public virtual string ToQuery()
        {
            Dictionary<string, string> queryDictionary = this.GetDictionary();
            return Utils.ToQuery(queryDictionary);
        }

        public virtual void FromQuery(string queryString)
        {
            Dictionary<string, string> dictionary = Regex.Matches(queryString, "([^?=&]+)(=([^&]*))?").Cast<Match>().ToDictionary(x => x.Groups[1].Value, x => x.Groups[3].Value);
            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                QueryName key = (QueryName)property.GetCustomAttributes(false).First();
                if (dictionary.ContainsKey(key.Name))
                {
                    property.SetValue(this, dictionary[key.Name], null); // TODO convert to proper type
                }
            }
        }

        /* Converts 'value' into string based on its type */
        protected string GetStringValue(object value)
        {
            if (value is List<string>)      // List<string> (returned as ["item", "item"]
            {
                List<string> stringList = ((List<string>)value);
                for (int i = 0; i < stringList.Count; i++)
                {
                    stringList[i] = "\"" + stringList[i] + "\""; // Add quotation marks
                }
                return "[" + String.Join(", ", stringList) + "]";
            }
            else if (value is List<Tag>)    // List<Tag> (returned as ["TAG", "TAG", "TAG"]
            {
                List<string> stringList = new List<string>();
                List<Tag> tagList = ((List<Tag>)value);
                for (int i = 0; i < tagList.Count; i++)
                {
                    stringList.Add("\"" + tagList[i].ToString() + "\""); // Add quotation marks
                }
                return "[" + String.Join(", ", stringList) + "]";
            }
            else if (value is char[])       // char[] (returned as (a, b))
            {
                return "(" + String.Join(", ", ((char[])value)) + ")";
            }
            else if (value is DateTime)     // DateTime (in ISO 8601 format)
            {
                DateTime dt = (DateTime)value;
                return (dt == null) ? "" : dt.ToString("yyyy-MM-ddTHH:mm:ss.fffffffzzz");
            }
            else                            // Takes care of the rest: int, bool, string, Uri
            {
                return (value == null) ? "" : value.ToString();
            }
        }
    }

    public class TranscriptOptions : BaseOptions, IQueryConvertible
    {
        [QueryName("create_paragraphs")]
        public bool CreateParagraphs { get; set; }          // Default: true
        [QueryName("newlines_after_paragraph")]
        public int NewLinesAfterParagraph { get; set; }     // Default: 2
        [QueryName("newlines_after_sentence")]
        public int NewLinesAfterSentence { get; set; }      // Default: 0
        [QueryName("timecode_every_paragraph")]
        public bool TimeCodeEveryParagraph { get; set; }    // Default: true
        [QueryName("timecode_format")]
        public string TimeCodeFormat { get; set; }          // Default: [%H:%M:%S.%f]
        [QueryName("time_code_interval")]
        public int TimeCodeInterval { get; set; }           // Default: 0
        [QueryName("timecode_offset")]
        public int TimeCodeOffset { get; set; }             // Default: 0

        public TranscriptOptions(int charactersPerCaptionLine = 0, DateTime? elementListVersion = null, string speakerChangeToken = ">>", bool maskProfanity = false,
            List<Tag> removeSoundsList = null, bool removeSoundReferences = true, bool replaceSlang = false, char[] soundBoundaries = null,
            bool createParagraphs = true, int newLinesAfterParagraph = 2, int newLinesAfterSentence = 0, bool timecodeEveryParagraph = true,
            string timecodeFormat = "[%H:%M:%S.%f]", int timecodeInterval = 0, int timecodeOffset = 0)
            : base(charactersPerCaptionLine, elementListVersion, speakerChangeToken, maskProfanity, removeSoundsList, removeSoundReferences, replaceSlang, soundBoundaries)
        {
            this.CreateParagraphs = createParagraphs;
            this.NewLinesAfterParagraph = newLinesAfterParagraph;
            this.NewLinesAfterSentence = newLinesAfterSentence;
            this.TimeCodeEveryParagraph = timecodeEveryParagraph;
            this.TimeCodeFormat = timecodeFormat;
            this.TimeCodeInterval = timecodeInterval;
            this.TimeCodeOffset = timecodeOffset;
        }
    }

    public class CaptionOptions : BaseOptions, IQueryConvertible
    {
        // DEFAULTS //
        private static readonly string srtFormatDefault = "{caption_number:d}\n{start_hour:02d}:{start_minute:02d}:{start_second:02d},{start_millisecond:03d} --> {end_hour:02d}:{end_minute:02d}:{end_second:02d},{end_millisecond:03d}\n{caption_text}\n\n";

        /* Note: some data types are nullable intentionally. GetStringValue() function treats nulls as empty strings. */
        [QueryName("build_url")]
        public bool BuildUrl { get; set; }                  // Default: false
        [QueryName("caption_words_min")]
        public int CaptionWordsMin { get; set; }            // Default: 1
        [QueryName("caption_by_sentence")]
        public bool CaptionBySentence { get; set; }         // Default: true
        [QueryName("dfxp_header")]
        public string DfxpHeader { get; set; }              // Default: ""
        [QueryName("disallow_dangling")]
        public bool DisallowDangling { get; set; }          // Default: false
        [QueryName("display_effects_speaker_as")]
        public string EffectsSpeaker { get; set; }          // Default: "Effects"
        [QueryName("display_speaker_id")]
        public SpeakerId DisplayedSpeakerId { get; set; }   // Default: "name" ["no","number", "name"]
        [QueryName("force_case")]
        public Case? ForceCase { get; set; }                 // Default: "" ["upper", "lower", ""]
        [QueryName("include_dfxp_metadata")]
        public bool IncludeDfxpMetadata { get; set; }       // Default: true
        [QueryName("layout_target_caption_length_ms")]
        public int LayoutTargetCaptionLengthMs { get; set; }// Default: 5000
        [QueryName("line_break_on_sentence")]
        public bool LineBreakOnSentence { get; set; }       // Default: false
        [QueryName("line_ending_format")]
        public LineEnding LineEndingFormat { get; set; }     // Default: UNIX
        [QueryName("lines_per_caption")]
        public int LinesPerCaption { get; set; }            // Default: 2
        [QueryName("maximum_caption_duration")]
        public int? MaximumCaptionDuration { get; set; }     // Default: none
        [QueryName("merge_gap_interval")]
        public int MergeGapInterval { get; set; }           // Default: 1000
        [QueryName("minimum_caption_length_ms")]
        public int? MinimumCaptionLengthMs { get; set; }     // Default: none
        [QueryName("minimum_gap_between_captions_ms")]
        public int? MinimumGapBetweenCaptionsMs { get; set; } // Default: none
        [QueryName("minimum_merge_gap_interval")]
        public int MinimumMergeGapInterval { get; set; }    // Default: 0
        [QueryName("qt_seamless")]
        public bool QtSeamless { get; set; }                // Default: false
        [QueryName("silence_max_ms")]
        public int SilenceMaxMs { get; set; }               // Default: 2000
        [QueryName("single_speaker_per_caption")]
        public bool SingleSpeakerPerCaption { get; set; }   // Default: false
        [QueryName("sound_threshold")]
        public int? SoundThreshold { get; set; }             // Default: none
        [QueryName("sound_tokens_by_caption")]
        public bool SoundTokensByCaption { get; set; }      // Default: false
        [QueryName("sound_tokens_by_line")]
        public bool SoundTokensByLine { get; set; }         // Default: false
        [QueryName("sound_tokens_by_caption_list")]
        public List<Tag> SoundTokensByCaptionList { get; set; }  // Default: ["BLANK_AUDIO", "MUSIC"]
        [QueryName("sound_tokens_by_line_list")]
        public List<Tag> SoundTokensByLineList { get; set; }     // Default: ["BLANK_AUDIO", "MUSIC"]
        [QueryName("speaker_on_new_line")]
        public bool SpeakerOnNewLine { get; set; }          // Default: true
        [QueryName("srt_format")]
        public string SrtFormat { get; set; }               // Default: "{caption_number:d}\n{start_hour:02d}:{start_minute:02d}:{start_second:02d},{start_millisecond:03d} --> {end_hour:02d}:{end_minute:02d}:{end_second:02d},{end_millisecond:03d}\n{caption_text}\n\n"
        [QueryName("srt_print")]
        public bool SrtPrintCaptionNumbers { get; set; }    // Default: true
        [QueryName("strip_square_brackets")]
        public bool StripSquareBrackets { get; set; }       // Default: false
        [QueryName("utf8_mark")]
        public bool Utf8Mark { get; set; }                  // Default: false

        public CaptionOptions(int charactersPerCaptionLine = 42, DateTime? elementListVersion = null, string speakerChangeToken = ">>", bool maskProfanity = false,
            List<Tag> removeSoundsList = null, bool removeSoundReferences = true, bool replaceSlang = false, char[] soundBoundaries = null,
            bool buildUri = false, int captionWordsMin = 1, bool captionBySentence = true, string dfxpHeader = "", bool disallowDangling = false,
            string effectsSpeaker = "Effects", SpeakerId displaySpeakerId = SpeakerId.name, Case? forceCase = null, bool includeDfxpMetadata = true,
            int layoutDefaultCaptionLengthMs = 5000, bool lineBreakOnSentence = false, LineEnding lineEndingFormat = LineEnding.UNIX,
            int linesPerCaption = 2, int? maximumCaptionDuration = null, int mergeGapInterval = 1000, int? minimumCaptionLengthMs = null, int? minimumGapBetweenCaptionsMs = null,
            int minimumMergeGapInterval = 0, bool qtSeamless = false, int silenceMaxMs = 2000, bool singleSpeakerPerCaption = false, int? soundThreshold = null,
            bool soundTokensByCaption = false, bool soundTokensByLine = false, List<Tag> soundTokensByCaptionList = null, List<Tag> soundTokensByLineList = null,
            bool speakerOnNewLine = true, string srtFormat = null, bool srtPrintCaptionNumbers = true, bool stripSquareBrackets = false, bool utf8_mark = false)
            : base(charactersPerCaptionLine, elementListVersion, speakerChangeToken, maskProfanity, removeSoundsList, removeSoundReferences, replaceSlang, soundBoundaries)
        {
            this.BuildUrl = buildUri;
            this.CaptionWordsMin = captionWordsMin;
            this.CaptionBySentence = captionBySentence;
            this.DfxpHeader = dfxpHeader;
            this.DisallowDangling = disallowDangling;
            this.EffectsSpeaker = effectsSpeaker;
            this.DisplayedSpeakerId = displaySpeakerId;
            this.ForceCase = forceCase;
            this.IncludeDfxpMetadata = includeDfxpMetadata;
            this.LayoutTargetCaptionLengthMs = layoutDefaultCaptionLengthMs;
            this.LineBreakOnSentence = lineBreakOnSentence;
            this.LineEndingFormat = lineEndingFormat;
            this.LinesPerCaption = linesPerCaption;
            this.MaximumCaptionDuration = maximumCaptionDuration;
            this.MergeGapInterval = mergeGapInterval;
            this.MinimumCaptionLengthMs = minimumCaptionLengthMs;
            this.MinimumGapBetweenCaptionsMs = minimumGapBetweenCaptionsMs;
            this.MinimumMergeGapInterval = minimumMergeGapInterval;
            this.QtSeamless = qtSeamless;
            this.SilenceMaxMs = silenceMaxMs;
            this.SingleSpeakerPerCaption = singleSpeakerPerCaption;
            this.SoundThreshold = soundThreshold;
            this.SoundTokensByCaption = soundTokensByCaption;
            this.SoundTokensByLine = soundTokensByLine;
            this.SoundTokensByCaptionList = (soundTokensByCaptionList == null) ? new List<Tag>() { Tag.BLANK_AUDIO, Tag.MUSIC } : soundTokensByCaptionList;
            this.SoundTokensByLineList = (soundTokensByLineList == null) ? new List<Tag>() { Tag.BLANK_AUDIO, Tag.MUSIC } : soundTokensByLineList;
            this.SpeakerOnNewLine = speakerOnNewLine;
            this.SrtFormat = (srtFormat == null) ? srtFormatDefault : srtFormat;
            this.SrtPrintCaptionNumbers = srtPrintCaptionNumbers;
            this.StripSquareBrackets = stripSquareBrackets;
            this.Utf8Mark = utf8_mark;
        }
    }

    public class QueryOptions : IQueryConvertible
    {
        public string QueryString { get; set; }

        public QueryOptions(string queryString)
        {
            this.QueryString = queryString;
        }

        public string ToQuery()
        {
            return this.QueryString;
        }
    }

    public interface IQueryConvertible
    {
        string ToQuery();
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class QueryName : Attribute
    {
        public string Name;

        public QueryName(string name)
        {
            this.Name = name;
        }
    }
}