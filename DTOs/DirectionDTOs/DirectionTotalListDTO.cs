using Cabinet_Prototype.DTOs.GroupDTOs;

namespace Cabinet_Prototype.DTOs.DirectionDTOs
{
    public class DirectionTotalListDTO
    {
        public Guid DirectionId { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<GroupListDTO> Groups { get; set; }
    }
}
