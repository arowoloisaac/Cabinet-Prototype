using System.ComponentModel.DataAnnotations;

namespace Cabinet_Prototype.DTOs
{
    public class ResponseModelDTO
    {
        [Required]
        public string Status { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
