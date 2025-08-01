using System.ComponentModel.DataAnnotations;
namespace todoApi.CQRS.DTOs
{
    public class UpdateTodoDto
    {
        [Required]
        [MaxLength(200)]
        public string? Name { get; set; }

        public bool IsComplete { get; set; }
    }
}
