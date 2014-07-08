using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WrapperCielo24.JSON
{
    public enum TaskType { JOB_CREATED, JOB_DELETED, JOB_ADD_MEDIA, JOB_ADD_TRANSCRIPT, JOB_PERFORM_TRANSCRIPTION, JOB_PERFORM_PREMIUM_SYNC, JOB_REQUEST_TRANSCRIPT, JOB_REQUEST_CAPTION, JOB_REQUEST_ELEMENTLIST }

    public enum ErrorType { LOGIN_INVALID, ACCOUNT_EXISTS, ACCOUNT_UNPRIVILEGED, BAD_API_TOKEN, INVALID_QUERY, INVALID_OPTION, MISSING_PARAMETER, INVALID_URL, ITEM_NOT_FOUND }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum JobStatus {
        [EnumMember(Value = "Authorizing")]
        Authorizing,
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "In Process")]
        InProcess,
        [EnumMember(Value = "Complete")]
        Complete
    }

    public enum TaskStatus { COMPLETE, INPROGRESS, ABORTED, FAILED }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Priority {
        [EnumMember(Value = "ECONOMY")]
        ECONOMY,
        [EnumMember(Value = "STANDARD")]
        STANDARD,
        [EnumMember(Value = "PRIORITY")]
        PRIORITY,
        [EnumMember(Value = "CRITICAL")]
        CRITICAL
    }

    public enum Fidelity { MECHANICAL, STANDARD, HIGH }

    public enum CaptionFormat { SRT, SBV, DFXP, QT }
}
