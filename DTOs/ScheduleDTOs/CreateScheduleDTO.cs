using Cabinet_Prototype.Enums;
using Cabinet_Prototype.Models;

namespace Cabinet_Prototype.DTOs.ScheduleDTOs
{
    public class CreateScheduleDTO
    {
        public DateTime ClassTime { get; set; }

        public string? Location { get; set; }

        public string? ClassNumber { get; set; }

        public ClassFormat Format { get; set; }

        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public Guid GroupsId { get; set; }
    }
}
