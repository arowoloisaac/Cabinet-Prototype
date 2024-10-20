using Cabinet_Prototype.Data;
using Cabinet_Prototype.DTOs.DirectionDTOs;
using Cabinet_Prototype.DTOs.GroupDTOs;
using Cabinet_Prototype.Models;
using Microsoft.EntityFrameworkCore;

namespace Cabinet_Prototype.Services.GroupService
{
    public class GroupService:IGroupService
    {
        private readonly ApplicationDbContext _dbContext;

        public GroupService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddGroup(GroupDTO model)
        {
            var DirectionExists = await _dbContext.Directions.AnyAsync(f => f.Id == model.DirectionId);
            if (!DirectionExists)
            {
                throw new KeyNotFoundException($"No direction found with ID: {model.DirectionId}");
            }

            var group = new Group
            {
                Id = Guid.NewGuid(),
                GroupNumber = model.GroupNumber,
                DirectionId = model.DirectionId
            };

            _dbContext.Groups.Add(group);
            await _dbContext.SaveChangesAsync();

            return group.Id;
        }


        public async Task<List<GroupListDTO>> ShowGroupList(Guid DirectionId)
        {
            var DirectionExists = await _dbContext.Directions.AnyAsync(f => f.Id == DirectionId);
            if (!DirectionExists)
            {
                throw new KeyNotFoundException($"No direction found with ID: {DirectionId}");
            }

            var groups = await _dbContext.Groups
                .Where(d => d.DirectionId == DirectionId)
                .Select(d => new GroupListDTO
                {
                    GroupId = d.Id,
                    GroupNumber = d.GroupNumber
                })
                .ToListAsync();

            return groups;
        }

        public async Task<GroupShowDTO> ShowGroupById(Guid GroupId)
        {
            var direction = await _dbContext.Groups
                .Where(d => d.Id == GroupId)
                .Select(d => new GroupShowDTO
                {
                    GroupNumber = d.GroupNumber,
                    DirectionId = d.DirectionId,
                    DirectionName = d.Direction.Name
                })
                .FirstOrDefaultAsync();

            if (direction == null)
            {
                throw new KeyNotFoundException($"No group found with ID: {GroupId}");
            }

            return direction;
        }


        public async Task<Message> EditGroupById(Guid GroupId, GroupDTO model)
        {
            var group = await _dbContext.Groups.FindAsync(GroupId);
            if (group == null)
            {
                throw new KeyNotFoundException($"Could not find the group {GroupId}");
            }

            var DirectionExists = await _dbContext.Directions.AnyAsync(f => f.Id == model.DirectionId);
            if (!DirectionExists)
            {
                throw new KeyNotFoundException($"No direction found with ID: {model.DirectionId}");
            }

            group.GroupNumber = model.GroupNumber;
            group.DirectionId = model.DirectionId;

            _dbContext.Groups.Update(group);
            await _dbContext.SaveChangesAsync();

            return new Message($"Success change the group {group}");
        }

        public async Task<Message> DeleteGroupById(Guid GroupId)
        {
            var group = await _dbContext.Groups.FindAsync(GroupId);
            if (group == null)
            {
                throw new KeyNotFoundException($"Could not find the group {GroupId}");
            }

            _dbContext.Groups.Remove(group);

            await _dbContext.SaveChangesAsync();

            return new Message($"Success delete the group {GroupId}");
        }

    }
}
