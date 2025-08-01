using MediatR;
using todoApi.CQRS.DTOs;

namespace todoApi.CQRS.Queries
{
    public class GetCompletedTodosQuery : IRequest<IEnumerable<TodoDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;

        public GetCompletedTodosQuery(int pageNumber = 1, int pageSize = 50)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
       public GetCompletedTodosQuery() { }
    }
}