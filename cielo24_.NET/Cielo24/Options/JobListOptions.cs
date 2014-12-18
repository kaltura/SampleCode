using Cielo24.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cielo24.Options
{
    public class JobListOptions : BaseOptions
    {
        [QueryName("creation_date_from")]
        public DateTime? CreateDateFrom { get; set; }
        [QueryName("creation_date_to")]
        public DateTime? CreateDateTo { get; set; }
        [QueryName("start_date_from")]
        public DateTime? StartDateFrom { get; set; }
        [QueryName("start_date_to")]
        public DateTime? StartDateTo { get; set; }
        [QueryName("due_date_from")]
        public DateTime? DueDateFrom { get; set; }
        [QueryName("due_date_to")]
        public DateTime? DueDateTo { get; set; }
        [QueryName("complete_date_from")]
        public DateTime? CompleteDateFrom { get; set; }
        [QueryName("complete_date_to")]
        public DateTime? CompleteDateTo { get; set; }
        [QueryName("job_status")]
        public JobStatus? JobStatus { get; set; }
        [QueryName("fidelity")]
        public Fidelity? Fidelity { get; set; }
        [QueryName("priority")]
        public Priority? Priority { get; set; }
        [QueryName("turnaround_time_hours_from")]
        public int? TurnaroundTimeHoursFrom { get; set; }
        [QueryName("username")]
        public string SubAccount { get; set; }

        public JobListOptions(DateTime? createDateFrom = null,
                              DateTime? createDateTo = null,
                              DateTime? startDateFrom = null,
                              DateTime? startDateTo = null,
                              DateTime? dueDateFrom = null,
                              DateTime? DueDateTo = null,
                              DateTime? completeDateFrom = null,
                              DateTime? completeDateTo = null,
                              JobStatus? jobStatus = null,
                              Fidelity? fidelity = null,
                              Priority? priority = null,
                              int? TurnaroundTimeHoursFrom = null,
                              string subAccount = null)
        {
            this.CreateDateFrom = createDateFrom;
            this.CreateDateTo = createDateTo;
            this.StartDateFrom = startDateFrom;
            this.StartDateTo = startDateTo;
            this.DueDateFrom = dueDateFrom;
            this.DueDateTo = DueDateTo;
            this.CompleteDateFrom = completeDateFrom;
            this.CompleteDateTo = completeDateTo;
            this.JobStatus = jobStatus;
            this.Fidelity = fidelity;
            this.Priority = priority;
            this.TurnaroundTimeHoursFrom = TurnaroundTimeHoursFrom;
            this.SubAccount = subAccount;
        }
    }
}
