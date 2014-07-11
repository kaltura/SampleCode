using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrapperCielo24.JSON
{
    public class JobInfo
    {
        [JsonProperty("JobId")]
        public Guid JobId { get; set; }
        [JsonProperty("JobName")]
        public string JobName { get; set; }
        [JsonProperty("Language")]
        public string Language { get; set; }
        [JsonProperty("Tasks")]
        public List<Task> Tasks { get; set; }

        public override string ToString()
        {
            string tasks = "";
            foreach (Task task in this.Tasks)
            {
                tasks += task.ToString() + "\n";
            }
            return "JobId: " + this.JobId.ToString("N") +
                   "\nJobName: " + this.JobName +
                   "\nLanguage: " + this.Language +
                   "\nTasks:\n\n" + tasks;
        }
    }
}
