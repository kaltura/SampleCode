using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cielo24.JSON
{
    public class JobList : JsonBase
    {
        [JsonProperty("Username")]
        public string Username { get; set; }
        [JsonProperty("ActiveJobs")]
        public List<Job> ActiveJobs { get; set; }
    }

    public class Job : JsonBase
    {
        [JsonProperty("JobId")]
        public Guid JobId { get; set; }
        [JsonProperty("JobName")]
        public string JobName { get; set; }
        [JsonProperty("JobStatus")]
        public JobStatus JobStatus { get; set; }
        [JsonProperty("Priority")]
        public Priority? Priority { get; set; }
        [JsonProperty("Fidelity")]
        public Fidelity? Fidelity { get; set; }
        [JsonProperty("JobLanguage")]
        public string JobLanguage { get; set; }
        [JsonProperty("TargetLanguage")]
        public string TargetLanguage { get; set; }
        [JsonProperty("CreationTime")]
        public DateTime? CreationTime { get; set; }
        [JsonProperty("DueDate")]
        public DateTime? DueDate { get; set; }
        [JsonProperty("TurnaroundTimeHours")]
        public int TurnaroundTimeHours { get; set; }
        [JsonProperty("StartTime")]
        public DateTime? StartTime { get; set; }
        [JsonProperty("CompletedTime")]
        public DateTime? CompletedTime { get; set; }
    }

    public class JobInfo : JsonBase
    {
        [JsonProperty("JobId")]
        public Guid JobId { get; set; }
        [JsonProperty("JobName")]
        public string JobName { get; set; }
        [JsonProperty("Language")]
        public string Language { get; set; }
        [JsonProperty("Tasks")]
        public List<Task> Tasks { get; set; }
    }

    public class CreateJobResult : JsonBase
    {
        [JsonProperty("JobId")]
        public Guid JobId { get; set; }
        [JsonProperty("TaskId")]
        public string TaskId { get; set; }
    }
}