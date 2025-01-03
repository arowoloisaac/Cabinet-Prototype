using Cabinet_Prototype.Data;
using Cabinet_Prototype.DTOs.ScheduleDTOs;
using Cabinet_Prototype.Enums;
using Cabinet_Prototype.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabinet_Prototype.Services.ScheduleSerives
{
    public class ScheduleService: IScheduleService
    {
        private readonly ApplicationDbContext _dbContext;

        public ScheduleService (ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateSchedule(CreateScheduleDTO models)
        {
            var courseTeacher = await _dbContext.CourseTeachers
                .FirstOrDefaultAsync(ct => ct.CourseId == models.CourseId && ct.TeacherId == models.TeacherId);

            if (courseTeacher == null)
            {
                throw new InvalidOperationException("The provided TeacherId is not associated with the provided CourseId.");
            }

            if (models.Format == ClassFormat.Offline)
            {
                if (string.IsNullOrWhiteSpace(models.Location))
                {
                    throw new InvalidOperationException("Location must be provided for Offline format.");
                }

                if (string.IsNullOrWhiteSpace(models.ClassNumber))
                {
                    throw new InvalidOperationException("ClassNumber must be provided for Offline format.");
                }
            }
            else if (models.Format == ClassFormat.Online)
            {
                if (!string.IsNullOrWhiteSpace(models.Location))
                {
                    throw new InvalidOperationException("Location should not be provided for Online format.");
                }

                if (!string.IsNullOrWhiteSpace(models.ClassNumber))
                {
                    throw new InvalidOperationException("ClassNumber should not be provided for Online format.");
                }
            }

            var schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                ClassTime = models.ClassTime,
                Location = models.Location,
                ClassNumber = models.ClassNumber,
                Format = models.Format,
                TeacherId = models.TeacherId,
                CourseId = models.CourseId,
                GroupsId = models.GroupsId
            };

            _dbContext.Schedules.Add(schedule);

            await _dbContext.SaveChangesAsync();

            return schedule.Id;
        }

        public async Task<Message> EditSchedule(ShowScheduleDTO model)
        {
            var schedule = await _dbContext.Schedules
                .FirstOrDefaultAsync(s => s.Id == model.ScheduleId);

            if (schedule == null)
            {
                throw new KeyNotFoundException("The specified schedule does not exist.");
            }

            var courseTeacher = await _dbContext.CourseTeachers
                .FirstOrDefaultAsync(ct => ct.CourseId == model.CourseId && ct.TeacherId == model.TeacherId);

            if (courseTeacher == null)
            {
                throw new InvalidOperationException("The provided TeacherId is not associated with the provided CourseId.");
            }

            if (model.Format == ClassFormat.Offline)
            {
                if (string.IsNullOrWhiteSpace(model.Location))
                {
                    throw new InvalidOperationException("Location must be provided for Offline format.");
                }

                if (string.IsNullOrWhiteSpace(model.ClassNumber))
                {
                    throw new InvalidOperationException("ClassNumber must be provided for Offline format.");
                }
            }
            else if (model.Format == ClassFormat.Online)
            {
                if (!string.IsNullOrWhiteSpace(model.Location))
                {
                    throw new InvalidOperationException("Location should not be provided for Online format.");
                }

                if (!string.IsNullOrWhiteSpace(model.ClassNumber))
                {
                    throw new InvalidOperationException("ClassNumber should not be provided for Online format.");
                }
            }

            schedule.ClassTime = model.ClassTime;
            schedule.Location = model.Location;
            schedule.ClassNumber = model.ClassNumber;
            schedule.Format = model.Format;
            schedule.TeacherId = model.TeacherId;
            schedule.CourseId = model.CourseId;
            schedule.GroupsId = model.GroupsId;

            await _dbContext.SaveChangesAsync();

            return new Message("Schedule updated successfully.");
        }

        public async Task<Message> DeleteSchedule(Guid scheduleId)
        {
            var schedule = await _dbContext.Schedules
                .FirstOrDefaultAsync(s => s.Id == scheduleId);

            if (schedule == null)
            {
                throw new KeyNotFoundException("The specified schedule does not exist.");
            }

            // 从数据库中移除 Schedule
            _dbContext.Schedules.Remove(schedule);

            // 保存更改
            await _dbContext.SaveChangesAsync();

            return new Message("Schedule deleted successfully.");
        }

        public async Task<List<ShowScheduleDTO>> ShowScheduleByStudentGroup(Guid studentId)
        {
            var student = await _dbContext.Users
                .Include(u => u.StudentGroupId)
                .FirstOrDefaultAsync(u => u.Id == studentId);

            if (student == null)
            {
                throw new KeyNotFoundException("Student not found.");
            }

            if (student.StudentGroupId == null)
            {
                throw new InvalidOperationException("The student is not associated with any group.");
            }

            var groupId = student.StudentGroupId.Id;

            var schedules = await _dbContext.Schedules
                .Where(s => s.GroupsId == groupId)
                .OrderBy(s => s.ClassTime)
                .Select(s => new ShowScheduleDTO
                {
                    ScheduleId = s.Id,
                    ClassTime = s.ClassTime,
                    Location = s.Location,
                    ClassNumber = s.ClassNumber,
                    Format = s.Format,
                    TeacherId = s.TeacherId,
                    CourseId = s.CourseId,
                    GroupsId = s.GroupsId
                })
                .ToListAsync();

            return schedules;
        }

        public async Task<List<ShowScheduleDTO>> ShowScheduleByCourse(Guid courseId)
        {
            var course = await _dbContext.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                throw new KeyNotFoundException("Course not found.");
            }

            var schedules = await _dbContext.Schedules
                .Where(s => s.CourseId == courseId)
                .OrderBy(s => s.ClassTime)
                .Select(s => new ShowScheduleDTO
                {
                    ScheduleId = s.Id,
                    ClassTime = s.ClassTime,
                    Location = s.Location,
                    ClassNumber = s.ClassNumber,
                    Format = s.Format,
                    TeacherId = s.TeacherId,
                    CourseId = s.CourseId,
                    GroupsId = s.GroupsId
                })
                .ToListAsync();

            return schedules;
        }

        public async Task<List<ShowScheduleDTO>> ShowScheduleByTeacher(Guid teacherId)
        {
            var teacher = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == teacherId && u.UserType == UserType.Teacher);

            if (teacher == null)
            {
                throw new KeyNotFoundException("Teacher not found or the user is not a teacher.");
            }

            var schedules = await _dbContext.Schedules
                .Where(s => s.TeacherId == teacherId)
                .OrderBy(s => s.ClassTime)
                .Select(s => new ShowScheduleDTO
                {
                    ScheduleId = s.Id,
                    ClassTime = s.ClassTime,
                    Location = s.Location,
                    ClassNumber = s.ClassNumber,
                    Format = s.Format,
                    TeacherId = s.TeacherId,
                    CourseId = s.CourseId,
                    GroupsId = s.GroupsId
                })
                .ToListAsync();

            return schedules;
        }
    }


}
