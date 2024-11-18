using Microsoft.Identity.Client;

namespace Cabinet_Prototype.Models
{
    public class Group
    {
        public Guid Id { get; set; }

        public ulong GroupNumber { get; set; }

        public Guid DirectionId { get; set; }

        public ICollection<Schedule>? Schedules { get; set; }

        public ICollection<User>? StudentGroup { get; set; }

        public ICollection<Course>? Courses { get; set; }

        public Direction Direction { get; set; }

        //public ICollection<UserRequests>? UserRequests { get; set; }

    }
}
