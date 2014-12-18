using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Cielo24.JSON.Job
{
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
}
