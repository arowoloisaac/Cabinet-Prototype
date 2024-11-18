namespace Cabinet_Prototype.Models
{
    // same idea with discipline
    public class Course
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Literature {  get; set; } = string.Empty;

        public string Reading {  get; set; } = string.Empty;

        public string Year {  get; set; } = string.Empty;

        public string Semester {  get; set; } = string.Empty;

        public Guid GroupId { get; set; }

        public ICollection<Schedule>? Schedules { get; set; }

        public ICollection<Result>? Results { get; set; }

        public ICollection<CourseTeacher>? CourseTeachers { get; set; }

        public Group Group { get; set; }


    }
}
