package cielo24.options;

import java.util.ArrayList;
import java.util.Date;

import cielo24.Enums.Tag;
import cielo24.utils.QueryName;

public class TranscriptOptions extends CommonOptions {
	
	@QueryName("create_paragraphs")
	public Boolean createParagraphs = null;
	@QueryName("newlines_after_paragraph")
	public Integer newLinesAfterParagraph = null;
	@QueryName("newlines_after_sentence")
	public Integer newLinesAfterSentence = null;
	@QueryName("timecode_every_paragraph")
	public Boolean timeCodeEveryParagraph = null;
	@QueryName("timecode_format")
	public String timeCodeFormat = null;
	@QueryName("time_code_interval")
	public Integer timeCodeInterval = null;
	@QueryName("timecode_offset")
	public Integer timeCodeOffset = null;

	public TranscriptOptions(){}
	
	public TranscriptOptions(int charactersPerCaptionLine,
			                 Date elementListVersion,
			                 String speakerChangeToken,
			                 boolean maskProfanity,
			                 ArrayList<Tag> removeSoundsList,
			                 boolean removeSoundReferences,
			                 boolean replaceSlang,
			                 char[] soundBoundaries,
			                 boolean createParagraphs,
                             int newLinesAfterParagraph,
                             int newLinesAfterSentence,
                             boolean timecodeEveryParagraph,
                             String timecodeFormat,
                             int timecodeInterval,
                             int timecodeOffset)
	{
		super(charactersPerCaptionLine,
		          elementListVersion,
	              speakerChangeToken,
	              maskProfanity,
	              removeSoundsList,
	              removeSoundReferences,
	              replaceSlang,
	              soundBoundaries);
		
		this.createParagraphs = createParagraphs;
        this.newLinesAfterParagraph = newLinesAfterParagraph;
        this.newLinesAfterSentence = newLinesAfterSentence;
        this.timeCodeEveryParagraph = timecodeEveryParagraph;
        this.timeCodeFormat = timecodeFormat;
        this.timeCodeInterval = timecodeInterval;
        this.timeCodeOffset = timecodeOffset;
	}
}