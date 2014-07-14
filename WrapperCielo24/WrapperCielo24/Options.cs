using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using WrapperCielo24.JSON;

namespace WrapperCielo24
{
    /* The base class. All of the other option classes inherit from it. */
    public abstract class Options
    {
        /* Returns a dictionary that contains key-value pairs of options, where key is the Name property
         * of the QueryName attribute assigned to every option and value is the value of the property.
         * Options with null value are not included in the dictionary. */
        public virtual Dictionary<string, string> GetDictionary()
        {
            Dictionary<string, string> queryDictionary = new Dictionary<string, string>();
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(this, null);
                if (value != null) // If property is null, don't include the key-value pair in the dictioanary
                {
                    QueryName key = (QueryName)property.GetCustomAttributes(typeof(QueryName), true).First();
                    queryDictionary.Add(key.Name, this.GetStringValue(value));
                }
            }
            return queryDictionary;
        }

        /* Returns a query string representation of options */
        public virtual string ToQuery()
        {
            Dictionary<string, string> queryDictionary = this.GetDictionary();
            return Utils.ToQuery(queryDictionary);
        }

        /* Sets the property whose QueryName attribute matches the key */
        public virtual void PopulateFromKeyValuePair(KeyValuePair<string, string> pair)
        {
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                QueryName key = (QueryName)property.GetCustomAttributes(typeof(QueryName), true).First();
                Type type = property.PropertyType;
                if (key.Name.Equals(pair.Key))
                {
                    property.SetValue(this, this.GetValueFromString(pair.Value, type), null);
                    break;
                }
            }
        }

        // Array of strings in the key=value form 
        public void PopulateFromArray(string[] array)
        {
            if (array == null) { return; }
            foreach (string s in array)
            {
                Dictionary<string, string> dictionary = Regex.Matches(s, "([^?=&]+)(=([^&]*))?").Cast<Match>().ToDictionary(x => x.Groups[1].Value, x => x.Groups[3].Value);
                this.PopulateFromKeyValuePair(dictionary.First());
            }
        }

        /* Converts string into an object */
        protected object GetValueFromString(string str, Type type)
        {
            object result = JsonConvert.DeserializeObject("\"" + str + "\"", type); // Quotes are necessary in json
            return result;
        }

        /* Converts 'value' into string based on its type. Precondition: value != null */
        protected string GetStringValue(object value)
        {
            if (value is List<string>)
            {
                return Utils.JoinQuoteList<string>((List<string>)value, ", ");
            }
            else if (value is List<Tag>)
            {
                return Utils.JoinQuoteList<Tag>((List<Tag>)value, ", ");
            }
            else if (value is List<Fidelity>)
            {
                return Utils.JoinQuoteList<Fidelity>((List<Fidelity>)value, ", ");
            }
            else if (value is char[])       // char[] (returned as (a, b))
            {
                return "(" + String.Join(", ", ((char[])value)) + ")";
            }
            else if (value is DateTime)     // DateTime (in ISO 8601 format)
            {
                return ((DateTime)value).ToString("yyyy-MM-ddTHH:mm:ss.fffffffzzz");
            }
            else                            // Takes care of the rest: int, bool, string, Uri
            {
                return value.ToString();
            }
        }
    }

    /* Options found in both Transcript and Caption options
     * All of the option properties are nullable. Properties that are null are ignored by the ToQuery() method
     * and are not part of the resulting query string. */
    public abstract class CommonOptions : Options
    {
        [QueryName("characters_per_caption_line")]
        public int? CharactersPerCaptionLine { get; set; }
        [QueryName("elementlist_version")]
        public DateTime? ElementListVersion { get; set; }
        [QueryName("emit_speaker_change_token_as")]
        public string SpeakerChangeToken { get; set; }
        [QueryName("mask_profanity")]
        public bool? MaskProfanity { get; set; }
        [QueryName("remove_sounds_list")]
        public List<Tag> RemoveSoundsList { get; set; }
        [QueryName("remove_sound_references")]
        public bool? RemoveSoundReferences { get; set; }
        [QueryName("replace_slang")]
        public bool? ReplaceSlang { get; set; }
        [QueryName("sound_boundaries")]
        public char[] SoundBoundaries { get; set; }

        public CommonOptions(int? charactersPerCaptionLine = null,
                             DateTime? elementListVersion = null,
                             string speakerChangeToken = null,
                             bool? maskProfanity = null,
                             List<Tag> removeSoundsList = null,
                             bool? removeSoundReferences = null,
                             bool? replaceSlang = null,
                             char[] soundBoundaries = null)
        {
            this.CharactersPerCaptionLine = charactersPerCaptionLine;
            this.ElementListVersion = elementListVersion;
            this.SpeakerChangeToken = speakerChangeToken;
            this.MaskProfanity = maskProfanity;
            this.RemoveSoundsList = removeSoundsList;
            this.RemoveSoundReferences = removeSoundReferences;
            this.ReplaceSlang = replaceSlang;
            this.SoundBoundaries = soundBoundaries;
        }
    }

    public class TranscriptOptions : CommonOptions
    {
        [QueryName("create_paragraphs")]
        public bool? CreateParagraphs { get; set; }
        [QueryName("newlines_after_paragraph")]
        public int? NewLinesAfterParagraph { get; set; }
        [QueryName("newlines_after_sentence")]
        public int? NewLinesAfterSentence { get; set; }
        [QueryName("timecode_every_paragraph")]
        public bool? TimeCodeEveryParagraph { get; set; }
        [QueryName("timecode_format")]
        public string TimeCodeFormat { get; set; }
        [QueryName("time_code_interval")]
        public int? TimeCodeInterval { get; set; }
        [QueryName("timecode_offset")]
        public int? TimeCodeOffset { get; set; }

        public TranscriptOptions(int? charactersPerCaptionLine = null,
                                 DateTime? elementListVersion = null,
                                 string speakerChangeToken = null,
                                 bool? maskProfanity = null,
                                 List<Tag> removeSoundsList = null,
                                 bool? removeSoundReferences = null,
                                 bool? replaceSlang = null,
                                 char[] soundBoundaries = null,
                                 bool? createParagraphs = null,
                                 int? newLinesAfterParagraph = null,
                                 int? newLinesAfterSentence = null,
                                 bool? timecodeEveryParagraph = null,
                                 string timecodeFormat = null,
                                 int? timecodeInterval = null,
                                 int? timecodeOffset = null)
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

    public class CaptionOptions : CommonOptions
    {
        [QueryName("build_url")]
        public bool? BuildUrl { get; set; }
        [QueryName("caption_words_min")]
        public int? CaptionWordsMin { get; set; }
        [QueryName("caption_by_sentence")]
        public bool? CaptionBySentence { get; set; }
        [QueryName("dfxp_header")]
        public string DfxpHeader { get; set; }
        [QueryName("disallow_dangling")]
        public bool? DisallowDangling { get; set; }
        [QueryName("display_effects_speaker_as")]
        public string EffectsSpeaker { get; set; }
        [QueryName("display_speaker_id")]
        public SpeakerId? DisplayedSpeakerId { get; set; }
        [QueryName("force_case")]
        public Case? ForceCase { get; set; }
        [QueryName("include_dfxp_metadata")]
        public bool? IncludeDfxpMetadata { get; set; }
        [QueryName("layout_target_caption_length_ms")]
        public int? LayoutTargetCaptionLengthMs { get; set; }
        [QueryName("line_break_on_sentence")]
        public bool? LineBreakOnSentence { get; set; }
        [QueryName("line_ending_format")]
        public LineEnding? LineEndingFormat { get; set; }
        [QueryName("lines_per_caption")]
        public int? LinesPerCaption { get; set; }
        [QueryName("maximum_caption_duration")]
        public int? MaximumCaptionDuration { get; set; }
        [QueryName("merge_gap_interval")]
        public int? MergeGapInterval { get; set; }
        [QueryName("minimum_caption_length_ms")]
        public int? MinimumCaptionLengthMs { get; set; }
        [QueryName("minimum_gap_between_captions_ms")]
        public int? MinimumGapBetweenCaptionsMs { get; set; }
        [QueryName("minimum_merge_gap_interval")]
        public int? MinimumMergeGapInterval { get; set; }
        [QueryName("qt_seamless")]
        public bool? QtSeamless { get; set; }
        [QueryName("silence_max_ms")]
        public int? SilenceMaxMs { get; set; }
        [QueryName("single_speaker_per_caption")]
        public bool? SingleSpeakerPerCaption { get; set; }
        [QueryName("sound_threshold")]
        public int? SoundThreshold { get; set; }
        [QueryName("sound_tokens_by_caption")]
        public bool? SoundTokensByCaption { get; set; }
        [QueryName("sound_tokens_by_line")]
        public bool? SoundTokensByLine { get; set; }
        [QueryName("sound_tokens_by_caption_list")]
        public List<Tag> SoundTokensByCaptionList { get; set; }
        [QueryName("sound_tokens_by_line_list")]
        public List<Tag> SoundTokensByLineList { get; set; }
        [QueryName("speaker_on_new_line")]
        public bool? SpeakerOnNewLine { get; set; }
        [QueryName("srt_format")]
        public string SrtFormat { get; set; }
        [QueryName("srt_print")]
        public bool? SrtPrintCaptionNumbers { get; set; }
        [QueryName("strip_square_brackets")]
        public bool? StripSquareBrackets { get; set; }
        [QueryName("utf8_mark")]
        public bool? Utf8Mark { get; set; }

        public CaptionOptions(int? charactersPerCaptionLine = null,
            DateTime? elementListVersion = null,
            string speakerChangeToken = null,
            bool? maskProfanity = null,
            List<Tag> removeSoundsList = null,
            bool? removeSoundReferences = null,
            bool? replaceSlang = null,
            char[] soundBoundaries = null,
            bool? buildUri = null,
            int? captionWordsMin = null,
            bool? captionBySentence = null,
            string dfxpHeader = null,
            bool? disallowDangling = null,
            string effectsSpeaker = null,
            SpeakerId? displaySpeakerId = null,
            Case? forceCase = null,
            bool? includeDfxpMetadata = null,
            int? layoutDefaultCaptionLengthMs = null,
            bool? lineBreakOnSentence = null,
            LineEnding? lineEndingFormat = null,
            int? linesPerCaption = null,
            int? maximumCaptionDuration = null,
            int? mergeGapInterval = null,
            int? minimumCaptionLengthMs = null,
            int? minimumGapBetweenCaptionsMs = null,
            int? minimumMergeGapInterval = null,
            bool? qtSeamless = null,
            int? silenceMaxMs = null,
            bool? singleSpeakerPerCaption = null,
            int? soundThreshold = null,
            bool? soundTokensByCaption = null,
            bool? soundTokensByLine = null,
            List<Tag> soundTokensByCaptionList = null,
            List<Tag> soundTokensByLineList = null,
            bool? speakerOnNewLine = null,
            string srtFormat = null,
            bool? srtPrintCaptionNumbers = null,
            bool? stripSquareBrackets = null,
            bool? utf8_mark = null)
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
            this.SoundTokensByCaptionList = soundTokensByCaptionList;
            this.SoundTokensByLineList = soundTokensByLineList;
            this.SpeakerOnNewLine = speakerOnNewLine;
            this.SrtFormat = srtFormat;
            this.SrtPrintCaptionNumbers = srtPrintCaptionNumbers;
            this.StripSquareBrackets = stripSquareBrackets;
            this.Utf8Mark = utf8_mark;
        }
    }

    public class PerformTranscriptionOptions : Options
    {
        [QueryName("customer_approval_steps")]
        public CustomerApprovalSteps? CustomerApprovalSteps { get; set; }
        [QueryName("customer_approval_tool")]
        public CustomerApprovalTools? CustomerApprovalTool { get; set; }
        [QueryName("custom_metadata")]
        public string CustomMetadata { get; set; }
        [QueryName("notes")]
        public string Notes { get; set; }
        [QueryName("return_iwp")]
        public List<Fidelity> ReturnIwp { get; set; }
        [QueryName("speaker_id")]
        public bool? SpeakerId { get; set; }                                 

        public PerformTranscriptionOptions(CustomerApprovalSteps? customerApprovalSteps = null,
                                           CustomerApprovalTools? customerApprovalTool = null,
                                           string customMetadata = null,
                                           string notes = null,
                                           List<Fidelity> returnIwp = null,
                                           bool? speakerId = null)
        {
            this.CustomerApprovalSteps = customerApprovalSteps;
            this.CustomerApprovalTool = customerApprovalTool;
            this.CustomMetadata = customMetadata;
            this.Notes = notes;
            this.ReturnIwp = returnIwp;
            this.SpeakerId = speakerId;
        }
    }

    class DeserializationHelper<T>
    {
        [JsonProperty("value")]
        public T Value { get; set; }
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