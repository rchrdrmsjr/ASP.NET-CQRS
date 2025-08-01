using MediatR;
using Microsoft.EntityFrameworkCore;
using todoApi.CQRS.Commands;

namespace todoApi.CQRS.Handlers
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
    {
        private readonly TodoDb _context;
        private readonly ILogger<DeleteTodoCommandHandler> _logger;

        public DeleteTodoCommandHandler(TodoDb context, ILogger<DeleteTodoCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Attempting to delete todo with ID: {Id}", request.Id);

                // Method 1: Direct execution approach (recommended)
                var rowsAffected = await _context.Todos
                    .Where(t => t.Id == request.Id)
                    .ExecuteDeleteAsync(cancellationToken);

                if (rowsAffected == 0)
                {
                    _logger.LogWarning("Todo with ID: {Id} not found for deletion", request.Id);
                    return false;
                }

                _logger.LogInformation("Successfully deleted todo with ID: {Id}", request.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting todo with ID: {Id}", request.Id);
                throw;
            }
        }
    }
}