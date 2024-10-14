using Cabinet_Prototype.DTOs.FacultyDTOs;
using Cabinet_Prototype.Models;

namespace Cabinet_Prototype.Services.FacultyService
{
    public interface IFacultyService
    {
        Task<Guid> AddFaculty(FacultyDTO model);
        Task<List<FacultyListDTO>> ShowFacultyList();
        Task<FacultyDTO> ShowFacultyById(Guid FacultyId);
        Task<Message> ChangeFacultyById(Guid facultyId, FacultyDTO model);
        Task<Message> DeleteFacultyById(Guid facultyId);
        Task<List<FacultyTotalShowDTO>> ShowALLChainsWithFacultyList();
        Task<FacultyTotalShowDTO> ShowALLChainsWithFacultyListById(Guid FacultyId);

    }
}
