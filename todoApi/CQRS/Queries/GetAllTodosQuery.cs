using MediatR;
using todoApi.CQRS.DTOs;

namespace todoApi.CQRS.Queries
{
    public class GetAllTodosQuery : IRequest<IEnumerable<TodoDto>>
    {
        public bool? IsComplete { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;

        public GetAllTodosQuery(bool? isComplete = null, int pageNumber = 1, int pageSize = 50)
        {
            IsComplete = isComplete;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public GetAllTodosQuery() { }
    }
}
