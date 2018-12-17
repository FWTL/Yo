using System.Threading.Tasks;

namespace <%= solutionName %>.Core.CQRS
{
    public interface IWriteCacheHandler<TQuery, TResult> : ICacheKey<TQuery>
    {
        Task WriteAsync(TQuery query, TResult result);
    }
}
