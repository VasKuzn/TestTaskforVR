namespace TestTaskVR.Models
{
    public class TodoItem
    {
        public long Id { get; set; }

        public string? Title { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
