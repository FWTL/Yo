using System.Threading.Tasks;

namespace <%= solutionName %>.Core.CQRS
{
    public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand
    {
        Task<TResult> ExecuteAsync(TCommand command);
    }

    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task ExecuteAsync(TCommand command);
    }
}
