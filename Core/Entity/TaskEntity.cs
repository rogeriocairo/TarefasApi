namespace TarefasApi.Core.Entity
{
    public class TaskEntity : BaseEntity
    {
        public required string Description { get; set; }

        public DateTime Date { get; set; }

        public required string Status { get; set; }
    }
}
