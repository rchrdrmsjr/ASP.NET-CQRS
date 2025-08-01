using MediatR;
using Microsoft.EntityFrameworkCore;
using todoApi.CQRS.Queries;
using todoApi.CQRS.DTOs;

namespace todoApi.CQRS.Handlers
{
    public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, TodoDto?>
    {
        // fields for the database context and logger
        private readonly TodoDb _context;
        private readonly ILogger<GetTodoByIdQueryHandler> _logger;

        // Constructor that takes the database context and logger as parameters via DEPENDENCY INJECTION.
        public GetTodoByIdQueryHandler(TodoDb context, ILogger<GetTodoByIdQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TodoDto?> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving todo with ID: {Id}", request.Id);

                var todo = await _context.Todos
                    .AsNoTracking()
                    .Where(t => t.Id == request.Id)
                    .Select(t => new TodoDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        IsComplete = t.IsComplete
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (todo == null)
                {
                    _logger.LogWarning("Todo with ID: {Id} not found", request.Id);
                }
                else
                {
                    _logger.LogInformation("Successfully retrieved todo with ID: {Id}", request.Id);
                }

                return todo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving todo with ID: {Id}", request.Id);
                throw;
            }
        }
    }
}