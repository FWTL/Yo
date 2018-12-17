using System.Threading.Tasks;

namespace <%= solutionName %>.Core.CQRS
{
    public interface IReadCacheHandler<TQuery, TResult> : ICacheKey<TQuery>
    {
        Task<TResult> ReadAsync(TQuery query);
    }
}
