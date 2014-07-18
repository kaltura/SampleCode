using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrapperCielo24.JSON
{
    public class JobList
    {
        [JsonProperty("Username")]
        public string Username { get; set; }
        [JsonProperty("ActiveJobs")]
        public List<Job> ActiveJobs { get; set; }

        public override string ToString()
        {
            string activeJobs = "";
            foreach (Job job in this.ActiveJobs)
            {
                activeJobs += job.ToString() + "\n";
            }
            return "Username: " + this.Username +
                   "\nActiveJobs:\n\n" + activeJobs;
        }
    }

    public class Job
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

        public override string ToString()
        {
            return "JobId: " + this.JobId.ToString("N") +
                   "\nJobName: " + this.JobName +
                   "\nJobStatus: " + this.JobStatus.ToString() +
                   "\nPriority: " + this.Priority.ToString() +
                   "\nFidelity: " + this.Fidelity.ToString() +
                   "\nJobLanguage: " + this.JobLanguage +
                   "\nTargetLanguage: " + this.TargetLanguage +
                   "\nCreationTime: " + this.CreationTime.ToString() +
                   "\nDueDate: " + this.DueDate.ToString() +
                   "\nTurnaroundTimeHours: " + this.TurnaroundTimeHours +
                   "\nStartTime: " + this.StartTime.ToString() +
                   "\nCompletedTime: " + this.CompletedTime.ToString();
        }
    }

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

    public class CreateJobResult
    {
        [JsonProperty("JobId")]
        public Guid JobId { get; set; }
        [JsonProperty("TaskId")]
        public string TaskId { get; set; }

        public override string ToString()
        {
            return "JobId: " + this.JobId.ToString("N") +
                   "\nTaskId: " + this.TaskId;
        }
    }
}