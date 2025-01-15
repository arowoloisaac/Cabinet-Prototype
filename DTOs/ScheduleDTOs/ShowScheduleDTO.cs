using Cabinet_Prototype.Enums;

namespace Cabinet_Prototype.DTOs.ScheduleDTOs
{
    public class ShowScheduleDTO
    {
        public Guid ScheduleId { get; set; }

        public DateTime ClassTime { get; set; }

        public string? Location { get; set; }

        public string? ClassNumber { get; set; }

        public ClassFormat Format { get; set; }

        public Guid TeacherId { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? GroupsId { get; set; }
    }
}
