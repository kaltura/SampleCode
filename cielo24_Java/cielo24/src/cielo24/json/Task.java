package cielo24.json;

import cielo24.utils.Guid;
import static cielo24.Enums.*;

import java.util.Date;

import com.google.gson.annotations.SerializedName;

public class Task extends JsonBase {
	@SerializedName("TaskId")
	public Guid taskId;
	@SerializedName("TaskType")
	public TaskType taskType;
	@SerializedName("TaskRequestTime")
	public Date taskRequestTime;
	@SerializedName("TaskCompletionTime")
	public Date taskCompletionTime;
	@SerializedName("TaskInfo")
	public String taskInfo;
	@SerializedName("TaskStatus")
	public TaskStatus taskStatus;
}