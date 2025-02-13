﻿using Cabinet_Prototype.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Cabinet_Prototype.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }

        [Required]
        public UserType UserType { get; set; }

        public string Avatar { get; set; } = string.Empty;

        public string? Password { get; set; }

        public string GradeNumber { get; set; } = string.Empty;

        public ICollection<CourseTeacher>? CourseTeachers { get; set; }

        public ICollection<Result>? StudentResults { get; set; }

        public ICollection<Schedule>? Schedules { get; set; }

        public Group? StudentGroupId { get; set; }

        public Direction? StudentDirection { get; set; }

        public Faculty? StudentFacultyId { get; set; }
    }
}
