namespace Cabinet_Prototype.Models
{
    //like the core program, for example in Hits will have software engineering while in other faculties they might have cybersercurity, applied math under 1 faculty
    public class Direction
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid FacultyId { get; set; }

        public ICollection<Group>? Groups { get; set; }

        public ICollection<User>? Users { get; set; }

        public Faculty Faculty { get; set; }
    }
}
