using MediatR;
using todoApi.CQRS.DTOs;

namespace todoApi.CQRS.Commands
{
    public class UpdateTodoCommand : IRequest<TodoDto?>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }

        public UpdateTodoCommand(int id, UpdateTodoDto dto)
        {
            Id = id;
            Name = dto.Name;
            IsComplete = dto.IsComplete;
        }

        public UpdateTodoCommand() { }
    }
}
