using Cabinet_Prototype.DTOs.DirectionDTOs;

namespace Cabinet_Prototype.DTOs.FacultyDTOs
{
    public class FacultyTotalShowDTO
    {
        public Guid FacultyId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string BuildingNumber { get; set; } = string.Empty;

        public List<DirectionTotalListDTO> Directions { get; set; }
    }
}
