using System.Threading.Tasks;

namespace <%= solutionName %>.Core.CQRS
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery;
    }
}
