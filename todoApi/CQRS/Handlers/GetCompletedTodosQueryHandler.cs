using MediatR;
using Microsoft.EntityFrameworkCore;
using todoApi.CQRS.Queries;
using todoApi.CQRS.DTOs;

namespace todoApi.CQRS.Handlers
{
    public class GetCompletedTodosQueryHandler : IRequestHandler<GetCompletedTodosQuery, IEnumerable<TodoDto>>
    {
        private readonly TodoDb _context;
        private readonly ILogger<GetCompletedTodosQueryHandler> _logger;

        public GetCompletedTodosQueryHandler(TodoDb context, ILogger<GetCompletedTodosQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<TodoDto>> Handle(GetCompletedTodosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Retrieving completed todos. Page: {Page}, Size: {Size}", 
                    request.PageNumber, request.PageSize);

                var todos = await _context.Todos
                    .AsNoTracking()
                    .Where(t => t.IsComplete == true)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(t => new TodoDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        IsComplete = t.IsComplete
                    })
                    .ToListAsync(cancellationToken);

                _logger.LogInformation("Retrieved {Count} completed todos", todos.Count);
                return todos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving completed todos");
                throw;
            }
        }
    }
}