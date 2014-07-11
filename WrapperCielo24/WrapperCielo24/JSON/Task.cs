using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WrapperCielo24.JSON
{
    public class Task
    {
        public Guid TaskId { get; set; }
        public TaskType TaskType { get; set; }
        public DateTime TaskRequestTime { get; set; }
        public DateTime TaskCompletionTime { get; set; }
        public string TaskInfo { get; set; }
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
