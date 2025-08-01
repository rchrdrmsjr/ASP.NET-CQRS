using MediatR;

namespace todoApi.CQRS.Commands
{
    public class DeleteTodoCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteTodoCommand(int id)
        {
            Id = id;
        }

        public DeleteTodoCommand() { }
    }
}
