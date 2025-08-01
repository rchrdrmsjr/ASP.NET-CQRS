using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using todoApi.CQRS.Commands;
using todoApi.CQRS.DTOs;
using todoApi.CQRS.Queries;

namespace todoApi.Controllers
{
    [Route("api/todoitems")]
    [ApiController]
    public class TodoesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TodoesController> _logger;

        public TodoesController(IMediator mediator, ILogger<TodoesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get all todos with optional filtering
        /// </summary>
        [HttpGet]
        [ResponseCache(Duration = 60, VaryByQueryKeys = new[] { "isComplete", "page", "size" })]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodos(
            [FromQuery] bool? isComplete = null,
            [FromQuery] int page = 1,
            [FromQuery] int size = 50)
        {
            _logger.LogInformation("Getting todos with IsComplete: {IsComplete}, Page: {Page}, Size: {Size}",
                isComplete, page, size);
            try
            {
                var query = new GetAllTodosQuery(isComplete, page, size);
                var result = await _mediator.Send(query);
                _logger.LogInformation("Retrieved {Count} todos", result.Count());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting todos");
                return StatusCode(500, "An error occurred while retrieving todos");
            }
        }

        /// <summary>
        /// Get completed todos only
        /// </summary>
        [HttpGet("complete")]
        [ResponseCache(Duration = 60, VaryByQueryKeys = new[] { "page", "size" })]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetCompletedTodos(
            [FromQuery] int page = 1,
            [FromQuery] int size = 50)
        {
            try
            {
                var query = new GetCompletedTodosQuery(page, size);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting completed todos");
                return StatusCode(500, "An error occurred while retrieving completed todos");
            }
        }

        /// <summary>
        /// Get a specific todo by ID
        /// </summary>
        [HttpGet("{id}")]
        [ResponseCache(Duration = 60, VaryByQueryKeys = new[] { "id" })]
        public async Task<ActionResult<TodoDto>> GetTodo(int id)
        {
            try
            {
                var query = new GetTodoByIdQuery(id);
                var result = await _mediator.Send(query);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting todo with ID: {Id}", id);
                return StatusCode(500, "An error occurred while retrieving the todo");
            }
        }

        /// <summary>
        /// Update a todo
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoDto>> PutTodo(int id, UpdateTodoDto dto)
        {
            try
            {
                var command = new UpdateTodoCommand(id, dto);
                var result = await _mediator.Send(command);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating todo with ID: {Id}", id);
                return StatusCode(500, "An error occurred while updating the todo");
            }
        }

        /// <summary>
        /// Create a new todo
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TodoDto>> PostTodo(CreateTodoDto dto)
        {
            try
            {
                var command = new CreateTodoCommand(dto);
                var result = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetTodo), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating todo");
                return StatusCode(500, "An error occurred while creating the todo");
            }
        }

        /// <summary>
        /// Delete a todo
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete todo with ID: {Id}", id);
                
                var command = new DeleteTodoCommand(id);
                var success = await _mediator.Send(command);

                if (!success)
                {
                    _logger.LogWarning("Todo with ID: {Id} not found for deletion", id);
                    return NotFound();
                }

                _logger.LogInformation("Successfully deleted todo with ID: {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting todo with ID: {Id}", id);
                return StatusCode(500, "An error occurred while deleting the todo");
            }
        }
    }
}
