using Cabinet_Prototype.Data;
using Cabinet_Prototype.DTOs.DirectionDTOs;
using Cabinet_Prototype.DTOs.FacultyDTOs;
using Cabinet_Prototype.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Cabinet_Prototype.Services.DirectionService
{
    public class DirectionService:IDirectionService 
    {
        private readonly ApplicationDbContext _dbContext;

        public DirectionService (ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddDirection(DirectionDTO model)
        {
            var facultyExists = await _dbContext.Faculties.AnyAsync(f => f.Id == model.FacultyId);
            if (!facultyExists)
            {
                throw new KeyNotFoundException($"No faculty found with ID: {model.FacultyId}");
            }

            var direction = new Direction
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                FacultyId = model.FacultyId
            };

            _dbContext.Directions.Add(direction);
            await _dbContext.SaveChangesAsync();

            return direction.Id;
        }

        public async Task<List<DirectionListDTO>> ShowDirectionList(Guid FacultyId)
        {
            var facultyExists = await _dbContext.Faculties.AnyAsync(f => f.Id == FacultyId);
            if (!facultyExists)
            {
                throw new KeyNotFoundException($"No faculty found with ID: {FacultyId}");
            }

            var directions = await _dbContext.Directions
                .Where(d => d.FacultyId == FacultyId)
                .Select(d => new DirectionListDTO
                {
                    DirectionId = d.Id,
                    Name = d.Name
                })
                .ToListAsync();

            return directions;
        }

        public async Task<DirectionShowDTO> ShowDirectionById(Guid DirectionId)
        {
            var direction = await _dbContext.Directions
                .Where(d => d.Id == DirectionId)
                .Select(d => new DirectionShowDTO
                {
                    Name = d.Name,
                    FacultyId = d.FacultyId,
                    FacultyName = d.Faculty.Name  
                })
                .FirstOrDefaultAsync();

            if (direction == null)
            {
                throw new KeyNotFoundException($"No direction found with ID: {DirectionId}");
            }

            return direction;
        }

        public async Task<Message> EditDirectionById(Guid DirectionId, DirectionDTO model)
        {
            var direction = await _dbContext.Directions.FindAsync(DirectionId);
            if (direction == null)
            {
                throw new KeyNotFoundException($"Could not find the direction {DirectionId}");
            }

            direction.Name = model.Name;
            direction.FacultyId = model.FacultyId;

            _dbContext.Directions.Update(direction);
            await _dbContext.SaveChangesAsync();

            return new Message($"Success change the faculty {direction}");
        }

        public async Task<Message> DeleteDirectionById(Guid DirectionId)
        {
            var direction = await _dbContext.Directions.FindAsync(DirectionId);
            if (direction == null)
            {
                throw new KeyNotFoundException($"Could not find the faculty {DirectionId}");
            }

            _dbContext.Directions.Remove(direction);

            await _dbContext.SaveChangesAsync();

            return new Message($"Success delete the faculty {DirectionId}");
        }
    }
}
