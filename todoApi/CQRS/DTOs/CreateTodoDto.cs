using System.ComponentModel.DataAnnotations;

namespace todoApi.CQRS.DTOs
{
    public class CreateTodoDto
    {
        [Required]
        [MaxLength(200)]
        public string? Name { get; set; }
        public bool IsComplete { get; set; } = false; // Default to false if not provided
    }
}
