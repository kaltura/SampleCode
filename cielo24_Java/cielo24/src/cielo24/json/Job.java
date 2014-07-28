package cielo24.json;

import cielo24.utils.Guid;
import static cielo24.Enums.*;

import java.util.Date;

import com.google.gson.annotations.SerializedName;

public class Job extends JsonBase
{
	@SerializedName("JobId")
    public Guid jobId;
	@SerializedName("JobName")
    public String jobName;
	@SerializedName("JobStatus")
    public JobStatus jobStatus;
	@SerializedName("Priority")
    public Priority priority;
	@SerializedName("Fidelity")
    public Fidelity fidelity;
	@SerializedName("JobLanguage")
    public String jobLanguage;
	@SerializedName("TargetLanguage")
    public String targetLanguage;
	@SerializedName("CreationTime")
    public Date creationTime;
	@SerializedName("DueDate")
    public Date dueDate;
	@SerializedName("TurnaroundTimeHours")
    public int turnaroundTimeHours;
	@SerializedName("StartTime")
    public Date startTime;
	@SerializedName("CompletedTime")
    public Date completedTime;
}