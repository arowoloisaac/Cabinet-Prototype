namespace Cabinet_Prototype.Models
{
    public class Faculty
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string BuildingNumber {  get; set; } = string.Empty;

        public ICollection<Direction>? Directions { get; set; }
    }
}
