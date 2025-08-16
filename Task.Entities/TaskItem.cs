namespace Task.Entities
{
    /// <summary>
    /// Represents a task item with details like name, created date, estimation, and status.
    /// </summary>
    public class TaskItem
    {
        public int Id { get; set; }
        /// <summary>
        /// The name or title of the task.
        /// </summary>
        public string TaskName { get; set; } = string.Empty;
        /// <summary>
        /// The date and time when the task was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Estimated time required to complete the task (in hours).
        /// </summary>
        public int Estimation { get; set; }
        /// <summary>
        /// The current status of the task (e.g., Pending, Completed).
        /// </summary>
        public string Status { get; set; } = "Pending";
    }
}
