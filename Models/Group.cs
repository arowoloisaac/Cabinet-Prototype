namespace Cabinet_Prototype.Models
{
    public class Group
    {
        public Guid Id { get; set; }

        public string GroupNumber { get; set; } = string.Empty;

        public Guid DirectionId { get; set; }

        public ICollection<Schedule>? Schedules { get; set; }

        public ICollection<User>? StudentGroup { get; set; }
    }
}
