using Cabinet_Prototype.Data;
using Cabinet_Prototype.DTOs.FacultyDTOs;
using Cabinet_Prototype.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Cabinet_Prototype.Services.FacultyService
{
    public class FacultyService: IFacultyService
    {
        private readonly ApplicationDbContext _dbContext;

        public FacultyService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       public async Task<Guid> AddFaculty(FacultyDTO model)
       {
            var faculty = new Faculty
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                BuildingNumber = model.BuildingNumber
            };

            _dbContext.Faculties.Add(faculty);
            await _dbContext.SaveChangesAsync();

            return faculty.Id;
       }

        public async Task<List<FacultyListDTO>> ShowFacultyList()
        {
            var faculties = await _dbContext.Faculties
            .Select(f => new FacultyListDTO
            {
                FacultyId = f.Id,
                Name = f.Name,
                BuildingNumber = f.BuildingNumber
            })
            .ToListAsync();

            return faculties;
        }

        public async Task<FacultyDTO> ShowFacultyById(Guid FacultyId)
        {
            var faculty = await _dbContext.Faculties
                .Where(f => f.Id == FacultyId)
                .Select(f => new FacultyDTO
                {
                    Name = f.Name,
                    BuildingNumber = f.BuildingNumber
                })
                .FirstOrDefaultAsync();

            if (faculty == null)
            {
                throw new KeyNotFoundException($"Could not find the faculty with ID: {FacultyId}");
            }

            return faculty;
        }

        public async Task<Message> ChangeFacultyById(Guid facultyId, FacultyDTO model)
        {
            var faculty = await _dbContext.Faculties.FindAsync(facultyId);
            if (faculty == null)
            {
                throw new KeyNotFoundException($"Could not find the faculty {facultyId}");
            }

            faculty.Name = model.Name;
            faculty.BuildingNumber = model.BuildingNumber;

            _dbContext.Faculties.Update(faculty);
            await _dbContext.SaveChangesAsync();

            return new Message($"Success change the faculty {facultyId}");
        }

        public async Task<Message> DeleteFacultyById(Guid facultyId)
        {
            var faculty = await _dbContext.Faculties.FindAsync(facultyId);
            if (faculty == null)
            {
                throw new KeyNotFoundException($"Could not find the faculty {facultyId}");
            }

            _dbContext.Faculties.Remove(faculty);

            await _dbContext.SaveChangesAsync();

            return new Message($"Success delete the faculty {facultyId}");
        }
    }
}
