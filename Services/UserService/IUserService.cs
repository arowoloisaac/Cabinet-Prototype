using Cabinet_Prototype.DTOs.UserDTOs;

namespace Cabinet_Prototype.Services.UserService
{
    public interface IUserService
    {
        Task<string> RegisterUser(RegisterDto dto);
        Task<List<TeacherDTO>> GetAllTeachers();

    }
}
