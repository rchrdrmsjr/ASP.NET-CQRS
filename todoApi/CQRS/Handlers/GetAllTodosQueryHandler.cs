using MediatR;
using Microsoft.EntityFrameworkCore;
using todoApi.CQRS.DTOs;
using todoApi.CQRS.Queries;

namespace todoApi.CQRS.Handlers
{
    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, IEnumerable<TodoDto>>
    {
        private readonly TodoDb _context;
        private readonly ILogger<GetAllTodosQueryHandler> _logger;

        public GetAllTodosQueryHandler(TodoDb context, ILogger<GetAllTodosQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<TodoDto>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetAllTodosQuery with filter: IsComplete={IsComplete}, Page={PageNumber}, Size={PageSize}",
                    request.IsComplete, request.PageNumber, request.PageSize);

                var query = _context.Todos.AsNoTracking();

                if (request.IsComplete.HasValue)
                {
                    query = query.Where(t => t.IsComplete == request.IsComplete.Value);
                }

                var todos = await query
                    .OrderBy(t => t.Id)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(t => new TodoDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        IsComplete = t.IsComplete
                    })
                    .ToListAsync(cancellationToken);

                _logger.LogInformation("Successfully retrieved {Count} todos", todos.Count);
                return todos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling GetAllTodosQuery");
                throw;
            }
        }
    }
}