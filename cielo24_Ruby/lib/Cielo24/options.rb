module Cielo24
  class Options
    def get_dictionary

    end

    def to_query

    end

    def populate_from_key_value_pair

    end

    def populate_from_array

    end
  end

  class CommonOptions < Options
    @characters_per_caption_line
    @elementlist_version
    @emit_speaker_change_token_as
    @mask_profanity
    @remove_sounds_list
    @remove_sound_references
    @replace_slang
    @sound_boundaries
  end

  class TranscriptionOptions < CommonOptions
    @create_paragraphs
    @newlines_after_paragraph
    @newlines_after_sentence
    @timecode_every_paragraph
    @timecode_format
    @time_code_interval
    @timecode_offset
  end

  class CaptionOptions < CommonOptions
    @build_url
    @caption_words_min
    @caption_by_sentence
    @dfxp_header
    @disallow_dangling
    @display_effects_speaker_as
    @display_speaker_id
    @force_case
    @include_dfxp_metadata
    @layout_target_caption_length_ms
    @line_break_on_sentence
    @line_ending_format
    @lines_per_caption
    @maximum_caption_duration
    @merge_gap_interval
    @minimum_caption_length_ms
    @minimum_gap_between_captions_ms
    @minimum_merge_gap_interval
    @qt_seamless
    @silence_max_ms
    @single_speaker_per_caption
    @sound_threshold
    @sound_tokens_by_caption
    @sound_tokens_by_line
    @sound_tokens_by_caption_list
    @sound_tokens_by_line_list
    @speaker_on_new_line
    @srt_format
    @srt_print
    @strip_square_brackets
    @utf8_mark
  end

  class PerformTranscriptionOptions < Options
    @customer_approval_steps
    @customer_approval_tool
    @custom_metadata
    @notes
    @return_iwp
    @speaker_id
  end
end