using MediatR;
using todoApi.CQRS.DTOs;

namespace todoApi.CQRS.Commands
{
    public class CreateTodoCommand : IRequest<TodoDto>
    {
        public string? Name { get; set; }
        public bool IsComplete { get; set; }

        public CreateTodoCommand(CreateTodoDto dto)
        {
            Name = dto.Name;
            IsComplete = dto.IsComplete;
        }

        public CreateTodoCommand() { }
    }
}
