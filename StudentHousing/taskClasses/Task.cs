using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace StudentHousing
{
    [Serializable]
    public class Task
    {
        private static int nextId = 1;
        private int id;
        public string taskName;
        private DateTime startTime;
        public DateTime endTime;
        private TaskStatus taskStatus;

        //read-only properties just in case we need info at some point
        public string TaskName { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public TaskStatus TaskStatus { get; private set; }
        public User AssignedUser { get; set; }

        public int TaskId => id;
        

        public Task(string taskName, DateTime startTime, DateTime endTime)
        {
            if (string.IsNullOrWhiteSpace(taskName))
                throw new ArgumentNullException(nameof(taskName), "Task name cannot be null or whitespace");

            if (startTime == DateTime.MinValue)
                throw new ArgumentException("Invalid start time");

            if (endTime == DateTime.MinValue)
                throw new ArgumentException("Invalid end time");

            if (startTime > endTime)
                throw new ArgumentException("Start time cannot be after end time");
            this.id = nextId++;
            this.taskName = taskName;
            this.startTime = startTime;
            this.endTime = endTime;
            this.TaskStatus = TaskStatus.Assigned;
        }

        public Task(string taskName, DateTime startTime, DateTime endTime, TaskStatus taskStatus)
        {
            if (string.IsNullOrWhiteSpace(taskName))
                throw new ArgumentNullException(nameof(taskName), "Task name cannot be null or whitespace");

            if (startTime == DateTime.MinValue)
                throw new ArgumentException("Invalid start time");

            if (endTime == DateTime.MinValue)
                throw new ArgumentException("Invalid end time");

            if (startTime > endTime)
                throw new ArgumentException("Start time cannot be after end time");

            if (!Enum.IsDefined(typeof(TaskStatus), taskStatus))
                throw new InvalidEnumArgumentException($"Enum is not defined");

            this.taskName = taskName;
            this.startTime = startTime;
            this.endTime = endTime;
            this.TaskStatus = taskStatus;
        }

        public void ChangeTaskStatus(TaskStatus taskStatus)
        {
            if (!Enum.IsDefined(typeof(TaskStatus), taskStatus))
                throw new InvalidEnumArgumentException($"Enum is not defined");
            TaskStatus = taskStatus;
        }

        public override string ToString()
        {
            return $"ID:{TaskId} Task:{taskName} Start:{startTime} End:{endTime}Status:{TaskStatus}";
        }
    }
}