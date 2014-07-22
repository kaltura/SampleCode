module Cielo24
  class Options
    def get_hash
      hash = {}
      self.instance_variables.each{ |var|
        value = self.instance_variable_get(var)
        if !value.nil?
          hash[var.to_s.delete("@")] = self.instance_variable_get(var)
        end
      }
      return hash
    end

    def to_query
      hash = get_hash
      array = Array.new(hash.length)
      counter = 0
      hash.each do |key, value|
        array[counter] = key + '=' + value.to_s
        counter += 1
      end
      return array.join("&")
    end

    def populate_from_key_value_pair

    end
  end

  class CommonOptions < Options

    attr_accessor :characters_per_caption_line
    attr_accessor :elementlist_version
    attr_accessor :emit_speaker_change_token_as
    attr_accessor :mask_profanity
    attr_accessor :remove_sounds_list
    attr_accessor :remove_sound_references
    attr_accessor :replace_slang
    attr_accessor :sound_boundaries

    def initialize
      @characters_per_caption_line = nil
      @elementlist_version = nil
      @emit_speaker_change_token_as = nil
      @mask_profanity = nil
      @remove_sounds_list = nil
      @remove_sound_references = nil
      @replace_slang = nil
      @sound_boundaries = nil
    end
  end

  class TranscriptionOptions < CommonOptions

    attr_accessor :create_paragraphs
    attr_accessor :newlines_after_paragraph
    attr_accessor :newlines_after_sentence
    attr_accessor :timecode_every_paragraph
    attr_accessor :timecode_format
    attr_accessor :time_code_interval
    attr_accessor :timecode_offset

    def initialize
      @create_paragraphs = nil
      @newlines_after_paragraph = nil
      @newlines_after_sentence = nil
      @timecode_every_paragraph = nil
      @timecode_format = nil
      @time_code_interval = nil
      @timecode_offset = nil
    end
  end

  class CaptionOptions < CommonOptions

    attr_accessor :build_url
    attr_accessor :caption_words_min
    attr_accessor :caption_by_sentence
    attr_accessor :dfxp_header
    attr_accessor :disallow_dangling
    attr_accessor :display_effects_speaker_as
    attr_accessor :display_speaker_id
    attr_accessor :force_case
    attr_accessor :include_dfxp_metadata
    attr_accessor :layout_target_caption_length_ms
    attr_accessor :line_break_on_sentence
    attr_accessor :line_ending_format
    attr_accessor :lines_per_caption
    attr_accessor :maximum_caption_duration
    attr_accessor :merge_gap_interval
    attr_accessor :minimum_caption_length_ms
    attr_accessor :minimum_gap_between_captions_ms
    attr_accessor :minimum_merge_gap_interval
    attr_accessor :qt_seamless
    attr_accessor :silence_max_ms
    attr_accessor :single_speaker_per_caption
    attr_accessor :sound_threshold
    attr_accessor :sound_tokens_by_caption
    attr_accessor :sound_tokens_by_line
    attr_accessor :sound_tokens_by_caption_list
    attr_accessor :sound_tokens_by_line_list
    attr_accessor :speaker_on_new_line
    attr_accessor :srt_format
    attr_accessor :srt_print
    attr_accessor :strip_square_brackets
    attr_accessor :utf8_mark

    def initialize
      @build_url = nil
      @caption_words_min = nil
      @caption_by_sentence = nil
      @dfxp_header = nil
      @disallow_dangling = nil
      @display_effects_speaker_as = nil
      @display_speaker_id = nil
      @force_case = nil
      @include_dfxp_metadata = nil
      @layout_target_caption_length_ms = nil
      @line_break_on_sentence = nil
      @line_ending_format = nil
      @lines_per_caption = nil
      @maximum_caption_duration = nil
      @merge_gap_interval = nil
      @minimum_caption_length_ms = nil
      @minimum_gap_between_captions_ms = nil
      @minimum_merge_gap_interval = nil
      @qt_seamless = nil
      @silence_max_ms = nil
      @single_speaker_per_caption = nil
      @sound_threshold = nil
      @sound_tokens_by_caption = nil
      @sound_tokens_by_line = nil
      @sound_tokens_by_caption_list = nil
      @sound_tokens_by_line_list = nil
      @speaker_on_new_line = nil
      @srt_format = nil
      @srt_print = nil
      @strip_square_brackets = nil
      @utf8_mark = nil
    end
  end

  class PerformTranscriptionOptions < Options

    attr_accessor :customer_approval_steps
    attr_accessor :customer_approval_tool
    attr_accessor :custom_metadata
    attr_accessor :notes
    attr_accessor :return_iwp
    attr_accessor :speaker_id

    def initialize
      @customer_approval_steps = nil
      @customer_approval_tool = nil
      @custom_metadata = nil
      @notes = nil
      @return_iwp = nil
      @speaker_id = nil
    end
  end
end