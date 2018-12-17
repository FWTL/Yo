using System.Threading.Tasks;

namespace <%= solutionName %>.Core.CQRS
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
