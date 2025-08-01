using MediatR;
using todoApi.CQRS.DTOs;

namespace todoApi.CQRS.Queries
{
    public class GetTodoByIdQuery : IRequest<TodoDto?>
    {
        public int Id { get; set; }
        public GetTodoByIdQuery(int id)
        {
            Id = id;
        }
        public GetTodoByIdQuery() { }
    }
}
