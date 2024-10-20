namespace Cabinet_Prototype.DTOs.UserDTOs
{
    public class GetUsersDto
    {
        public Guid userId { get; set; }

        public string Email { get; set; } = string.Empty;
    }
}
