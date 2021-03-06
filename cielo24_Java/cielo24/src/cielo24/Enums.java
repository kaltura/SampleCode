package cielo24;

import com.google.gson.annotations.SerializedName;

public class Enums {

	public enum TaskType {
		JOB_CREATED, JOB_DELETED, JOB_ADD_MEDIA, JOB_ADD_TRANSCRIPT, JOB_PERFORM_TRANSCRIPTION, JOB_PERFORM_PREMIUM_SYNC, JOB_UPDATE_ELEMENTLIST, JOB_GET_TRANSCRIPT, JOB_GET_CAPTION, JOB_GET_ELEMENTLIST
	}

	public enum ErrorType {
		LOGIN_INVALID, ACCOUNT_EXISTS, ACCOUNT_UNPRIVILEGED, BAD_API_TOKEN, INVALID_QUERY, INVALID_OPTION, MISSING_PARAMETER, INVALID_URL, ITEM_NOT_FOUND
	}

	public enum JobStatus {
		Authorizing, Pending, @SerializedName("In Process") In_Process, Complete
	}

	public enum TaskStatus {
		COMPLETE, INPROGRESS, ABORTED, FAILED
	}

	public enum Priority {
		ECONOMY, STANDARD, PRIORITY, CRITICAL, HIGH
	}

	public enum Fidelity {
		MECHANICAL, STANDARD, HIGH, EXTERNAL, PREMIUM, PROFESSIONAL
	}

	public enum CaptionFormat {
		SRT, SBV, DFXP, QT, TRANSCRIPT, TWX, TPM, WEB_VTT, ECHO
	}

	public enum TokenType {
		word, punctuation, sound
	}

	public enum Tag {
		ENDS_SENTENCE, UNKNOWN, INAUDIBLE, CROSSTALK, MUSIC, NOISE, LAUGH, COUGH, FOREIGN, GUESSED, BLANK_AUDIO, APPLAUSE, BLEEP
	}

	public enum SpeakerId {
		no, number, name
	}

	public enum SpeakerGender {
		UNKNOWN, MALE, FEMALE
	}

	public enum Case {
		upper, lower
	}

	public enum LineEnding {
		UNIX, WINDOWS, OSX
	}

	public enum CustomerApprovalSteps {
		TRANSLATION, RETURN
	}

	public enum CustomerApprovalTools {
		AMARA, CIELO24
	}

	public enum Languages {
		en, fr, es, de, cmn, pt, jp, ar, ko, zh, hi, it, ru, tr, he
	}
}