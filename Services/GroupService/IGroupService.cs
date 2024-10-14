using Cabinet_Prototype.DTOs.GroupDTOs;
using Cabinet_Prototype.Models;

namespace Cabinet_Prototype.Services.GroupService
{
    public interface IGroupService
    {
        Task<Guid> AddGroup(GroupDTO model);

        Task<List<GroupListDTO>> ShowGroupList(Guid DirectionId);

        Task<GroupShowDTO> ShowGroupById(Guid GroupId);

        Task<Message> EditGroupById(Guid GroupId, GroupDTO model);

        Task<Message> DeleteGroupById(Guid GroupId);
    }
}
