using MediatR;
using Microsoft.EntityFrameworkCore;
using todoApi.CQRS.Commands;
using todoApi.CQRS.DTOs;

namespace todoApi.CQRS.Handlers
{
    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoDto>
    {
        private readonly TodoDb _context;
        private readonly ILogger<CreateTodoCommandHandler> _logger;

        public CreateTodoCommandHandler(TodoDb context, ILogger<CreateTodoCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TodoDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating new todo with name: {Name}", request.Name);

                var todo = new Todo
                {
                    Name = request.Name,
                    IsComplete = request.IsComplete
                };

                _context.Todos.Add(todo);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully created todo with ID: {Id}", todo.Id);

                return new TodoDto
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    IsComplete = todo.IsComplete
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating todo with name: {Name}", request.Name);
                throw;
            }
        }
    }
}
