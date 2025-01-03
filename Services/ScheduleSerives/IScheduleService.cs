using Cabinet_Prototype.Data;
using Cabinet_Prototype.DTOs.ScheduleDTOs;
using Cabinet_Prototype.Models;

namespace Cabinet_Prototype.Services.ScheduleSerives
{
    public interface IScheduleService
    {
        Task<Guid> CreateSchedule(CreateScheduleDTO models);
        Task<Message> EditSchedule(ShowScheduleDTO model);
        Task<Message> DeleteSchedule(Guid scheduleId);
        Task<List<ShowScheduleDTO>> ShowScheduleByStudentGroup(Guid studentId);
        Task<List<ShowScheduleDTO>> ShowScheduleByCourse(Guid courseId);
        Task<List<ShowScheduleDTO>> ShowScheduleByTeacher(Guid teacherId);

    }
}
