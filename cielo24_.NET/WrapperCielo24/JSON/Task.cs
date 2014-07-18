using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrapperCielo24.JSON
{
    public class Task
    {
        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }
        [JsonProperty("TaskType")]
        public TaskType TaskType { get; set; }
        [JsonProperty("TaskRequestTime")]
        public DateTime TaskRequestTime { get; set; }
        [JsonProperty("TaskCompletionTime")]
        public DateTime TaskCompletionTime { get; set; }
        [JsonProperty("TaskInfo")]
        public string TaskInfo { get; set; }
        [JsonProperty("TaskStatus")]
        public TaskStatus TaskStatus { get; set; }

        public override string ToString()
        {
            return "TaskId: " + this.TaskId.ToString("N") +
                   "\nTaskType: " + this.TaskType.ToString() +
                   "\nTaskRequestTime: " + this.TaskRequestTime.ToString() +
                   "\nTaskCompletionTime: " + this.TaskCompletionTime.ToString() +
                   "\nTaskInfo: " + this.TaskInfo +
                   "\nTaskStatus: " + this.TaskStatus.ToString();
        }
    }
}
