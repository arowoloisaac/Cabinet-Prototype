using Cabinet_Prototype.DTOs.UserDTOs;
using Cabinet_Prototype.Models;

namespace Cabinet_Prototype.Services.AdminService
{
    public interface IAdminService
    {
        /***
         * things administrator should have control to do
         * create faculty
         * create group
         * link faculty to group when create the group 
         * add user to role 
         * crud user generally***/
        Task<List<UserRequestDto>> GetRequests(string adminId);

        Task<Message> AddUser(UserRequestDto userRequestDto);
    }
}
