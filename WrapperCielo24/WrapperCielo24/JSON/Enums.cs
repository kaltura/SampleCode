using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WrapperCielo24.JSON
{
    public enum TaskType { JOB_CREATED, JOB_DELETED, JOB_ADD_MEDIA, JOB_ADD_TRANSCRIPT, JOB_PERFORM_TRANSCRIPTION, JOB_PERFORM_PREMIUM_SYNC, JOB_REQUEST_TRANSCRIPT, JOB_REQUEST_CAPTION, JOB_REQUEST_ELEMENTLIST }

    public enum ErrorType { LOGIN_INVALID, ACCOUNT_EXISTS, ACCOUNT_UNPRIVILEGED, BAD_API_TOKEN, INVALID_QUERY, INVALID_OPTION, MISSING_PARAMETER, INVALID_URL, ITEM_NOT_FOUND }

    [JsonConverter(typeof(JobStatusConverter))]
    public enum JobStatus { Authorizing, Pending, In_Process, Complete }

    public enum TaskStatus { COMPLETE, INPROGRESS, ABORTED, FAILED }

    public enum Priority { ECONOMY, STANDARD, PRIORITY, CRITICAL }

    public enum Fidelity { STANDARD, MECHANICAL, PREMIUM, PROFESSIONAL, INTERIM_PROFESSIONAL, FINAL }

    public enum CaptionFormat { SRT, SBV, DFXP, QT }

    public enum TokenType { word, punctuation, sound }

    public enum Tag { ENDS_SENTENCE, UNKNOWN, INAUDIBLE, CROSSTALK, MUSIC, NOISE, LAUGH, COUGH, FOREIGN, GUESSED, BLANK_AUDIO, APPLAUSE, BLEEP }

    public enum SpeakerId { no, number, name }

    public enum SpeakerGender { UNKNOWN, MALE, FEMALE }

    public enum Case { upper, lower }

    public enum LineEnding { UNIX, WINDOWS, OSX }

    public enum CustomerApprovalSteps { TRANSLATION, RETURN }

    public enum CustomerApprovalTools { AMARA, CIELO24 }

    /* JobStatus enum requires a converter because strings with spacescannot be implicitly converted to enum */
    public class JobStatusConverter : StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return (reader.Value.ToString().Equals("In Process")) ? JobStatus.In_Process : base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }
}
