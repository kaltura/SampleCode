module Cielo24
  class TaskType < BasicObject
    def self.JOB_CREATED; "JOB_CREATED"; end
    def self.JOB_DELETED; "JOB_DELETED"; end
    def self.JOB_ADD_MEDIA; "JOB_ADD_MEDIA"; end
    def self.JOB_ADD_TRANSCRIPT; "JOB_ADD_TRANSCRIPT"; end
    def self.JOB_PERFORM_TRANSCRIPTION; "JOB_PERFORM_TRANSCRIPTION"; end
    def self.JOB_PERFORM_PREMIUM_SYNC; "JOB_PERFORM_PREMIUM_SYNC"; end
    def self.JOB_UPDATE_ELEMENTLIST; "JOB_UPDATE_ELEMENTLIST"; end
    def self.JOB_GET_TRANSCRIPT; "JOB_GET_TRANSCRIPT"; end
    def self.JOB_GET_CAPTION; "JOB_GET_CAPTION"; end
    def self.JOB_GET_ELEMENTLIST; "JOB_GET_ELEMENTLIST"; end
  end

  class ErrorType < BasicObject
    def self.LOGIN_INVALID; "LOGIN_INVALID"; end
    def self.ACCOUNT_EXISTS; "ACCOUNT_EXISTS"; end
    def self.ACCOUNT_UNPRIVILEGED; "ACCOUNT_UNPRIVILEGED"; end
    def self.BAD_API_TOKEN; "BAD_API_TOKEN"; end
    def self.INVALID_QUERY; "INVALID_QUERY"; end
    def self.INVALID_OPTION; "INVALID_OPTION"; end
    def self.MISSING_PARAMETER; "MISSING_PARAMETER"; end
    def self.INVALID_URL; "INVALID_URL"; end
    def self.ITEM_NOT_FOUND; "ITEM_NOT_FOUND"; end
  end

  class JobStatus < BasicObject
    def self.Authorizing; "SRT"; end
    def self.Pending; "SRT"; end
    def self.In_Process; "In Process"; end
    def self.Complete; "Complete"; end
  end

  class TaskStatus < BasicObject
    def self.COMPLETE; "COMPLETE"; end
    def self.INPROGRESS; "INPROGRESS"; end
    def self.ABORTED; "ABORTED"; end
    def self.FAILED; "FAILED"; end
  end

  class Priority < BasicObject
    def self.ECONOMY; "ECONOMY"; end
    def self.STANDARD; "STANDARD"; end
    def self.PRIORITY; "PRIORITY"; end
    def self.CRITICAL; "CRITICAL"; end
    def self.HIGH; "HIGH"; end
    def self.all; "[ ECONOMY, STANDARD, PRIORITY, CRITICAL, HIGH ]"; end
  end

  class Fidelity < BasicObject
    def self.MECHANICAL; "MECHANICAL"; end
    def self.STANDARD; "STANDARD"; end
    def self.HIGH; "HIGH"; end
    def self.PREMIUM; "PREMIUM"; end
    def self.PROFESSIONAL; "PROFESSIONAL"; end
    def self.EXTERNAL; "EXTERNAL"; end
    def self.all; "[ MECHANICAL, STANDARD, HIGH, PREMIUM, PROFESSIONAL, EXTERNAL ]"; end
  end

  class CaptionFormat < BasicObject
    def self.SRT; "SRT"; end
    def self.SBV; "SBV"; end
    def self.DFXP; "DFXP"; end
    def self.TRANSCRIPT; "TRANSCRIPT"; end
    def self.TWX; "TWX"; end
    def self.TPM; "TPM"; end
    def self.WEB_VTT; "WEB_VTT"; end
    def self.ECHO; "ECHO"; end
    def self.all; "[ SRT, SBV, DFXP, QT, TRANSCRIPT, TWX, TPM, WEB_VTT, ECHO ]"; end
  end

  class TokenType < BasicObject
    def self.word; "word"; end
    def self.punctuation; "punctuation"; end
    def self.sound; "sound"; end
  end

  class Tag < BasicObject
    def self.ENDS_SENTENCE; "ENDS_SENTENCE"; end
    def self.UNKNOWN; "UNKNOWN"; end
    def self.INAUDIBLE; "INAUDIBLE"; end
    def self.CROSSTALK; "CROSSTALK"; end
    def self.MUSIC; "MUSIC"; end
    def self.NOISE; "NOISE"; end
    def self.LAUGH; "LAUGH"; end
    def self.COUGH; "COUGH"; end
    def self.FOREIGN; "FOREIGN"; end
    def self.GUESSED; "GUESSED"; end
    def self.BLANK_AUDIO; "BLANK_AUDIO"; end
    def self.APPLAUSE; "APPLAUSE"; end
    def self.BLEEP; "BLEEP"; end
  end

  class SpeakerId < BasicObject
    def self.no; "no"; end
    def self.number; "number"; end
    def self.name; "name"; end
  end

  class SpeakerGender < BasicObject
    def self.UNKNOWN; "UNKNOWN"; end
    def self.MALE; "MALE"; end
    def self.FEMALE; "FEMALE"; end
  end

  class Case < BasicObject
    def self.upper; "upper"; end
    def self.lower; "lower"; end
    def self.unchanged; ""; end
  end

  class LineEnding < BasicObject
    def self.UNIX; "UNIX"; end
    def self.WINDOWS; "WINDOWS"; end
    def self.OSX; "OSX"; end
  end

  class CustomerApprovalSteps < BasicObject
    def self.TRANSLATION; "TRANSLATION"; end
    def self.RETURN; "RETURN"; end
  end

  class CustomerApprovalTools < BasicObject
    def self.AMARA; "AMARA"; end
    def self.CIELO24; "CIELO24"; end
  end

  class Language < BasicObject
    def self.English; "en"; end
    def self.French; "fr"; end
    def self.Spanish; "es"; end
    def self.German; "de"; end
    def self.Mandarin_Chinese; "cmn"; end
    def self.Portuguese; "pt"; end
    def self.Japanese; "jp"; end
    def self.Arabic; "ar"; end
    def self.Korean; "ko"; end
    def self.Traditional_Chinese; "zh"; end
    def self.Hindi; "hi"; end
    def self.Italian; "it"; end
    def self.Russian; "ru"; end
    def self.Turkish; "tr"; end
    def self.Hebrew; "he"; end
    def self.all; "[ en, fr, es, de, cmn, pt, jp, ar, ko, zh, hi, it, ru, tr, he ]"; end
  end
end