using System;

namespace <%= solutionName %>.Core.CQRS
{
    public interface ICacheKey<TQuery>
    {
        Func<TQuery, string> KeyFn { get; set; }
    }
}
