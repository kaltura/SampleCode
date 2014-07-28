package cielo24.options;

import java.util.ArrayList;
import java.util.Date;

import cielo24.utils.QueryName;
import static cielo24.Enums.*;

public abstract class CommonOptions extends BaseOptions {

	@QueryName("characters_per_caption_line")
	public Integer charactersPerCaptionLine = null;
	@QueryName("elementlist_version")
	public Date elementListVersion = null;
	@QueryName("emit_speaker_change_token_as")
	public String speakerChangeToken = null;
	@QueryName("mask_profanity")
	public Boolean maskProfanity = null;
	@QueryName("remove_sounds_list")
	public ArrayList<Tag> removeSoundsList = null;
	@QueryName("remove_sound_references")
	public Boolean removeSoundReferences = null;
	@QueryName("replace_slang")
	public Boolean replaceSlang = null;
	@QueryName("sound_boundaries")
	public char[] soundBoundaries = null;

	public CommonOptions(){}
	
	public CommonOptions(int charactersPerCaptionLine,
			Date elementListVersion,
			String speakerChangeToken,
			boolean maskProfanity,
			ArrayList<Tag> removeSoundsList,
			boolean removeSoundReferences,
			boolean replaceSlang,
			char[] soundBoundaries)
	{
		this.charactersPerCaptionLine = charactersPerCaptionLine;
		this.elementListVersion = elementListVersion;
		this.speakerChangeToken = speakerChangeToken;
		this.maskProfanity = maskProfanity;
		this.removeSoundsList = removeSoundsList;
		this.removeSoundReferences = removeSoundReferences;
		this.replaceSlang = replaceSlang;
		this.soundBoundaries = soundBoundaries;
	}
}