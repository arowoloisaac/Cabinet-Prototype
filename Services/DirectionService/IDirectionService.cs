using Cabinet_Prototype.DTOs.DirectionDTOs;
using Cabinet_Prototype.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabinet_Prototype.Services.DirectionService
{
    public interface IDirectionService
    {
        Task<Guid> AddDirection(DirectionDTO model);

        Task<List<DirectionListDTO>> ShowDirectionList(Guid FacultyId);

        Task<DirectionShowDTO> ShowDirectionById(Guid DirectionId);

        Task<Message> EditDirectionById(Guid DirectionId, DirectionDTO model);

        Task<Message> DeleteDirectionById(Guid DirectionId);
    }
}
