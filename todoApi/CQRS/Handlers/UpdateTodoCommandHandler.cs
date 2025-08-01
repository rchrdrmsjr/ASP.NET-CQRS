using MediatR; // Imports MediatR, which allows us to use the IRequestHandler<TRequest, TResponse> interface for handling commands and queries.
using Microsoft.EntityFrameworkCore; // Provides access to EF Core methods like FindAsync, Entry, and SaveChangesAsync.
using todoApi.CQRS.Commands; // Imports the UpdateTodoCommand definition from your CQRS structure.
using todoApi.CQRS.DTOs; // Imports the TodoDto, which is a Data Transfer Object used to return sanitized data (not the full entity).

// Defines the namespace (logical grouping of code). This is where the handler class lives.
namespace todoApi.CQRS.Handlers
{
    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, TodoDto?>
    {
        // fields for the database context and logger
        private readonly TodoDb _context;
        private readonly ILogger<UpdateTodoCommandHandler> _logger;

        // Constructor that takes the database context and logger as parameters via DEPENDENCY INJECTION.
        public UpdateTodoCommandHandler(TodoDb context, ILogger<UpdateTodoCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TodoDto?> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating todo with ID: {Id}", request.Id);

                var todo = await _context.Todos.FindAsync(new object[] { request.Id }, cancellationToken);
                
                if (todo == null)
                {
                    _logger.LogWarning("Todo with ID: {Id} not found for update", request.Id);
                    return null;
                }

                todo.Name = request.Name;
                todo.IsComplete = request.IsComplete;

                _context.Entry(todo).State = EntityState.Modified;
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully updated todo with ID: {Id}", request.Id);

                return new TodoDto
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    IsComplete = todo.IsComplete
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating todo with ID: {Id}", request.Id);
                throw;
            }
        }
    }
}
