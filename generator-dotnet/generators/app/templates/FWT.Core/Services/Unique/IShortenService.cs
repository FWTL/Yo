using HashidsNet;

namespace <%= solutionName %>.Core.Services.Hash
{
    public interface IShortenService
    {
        Hashids Hash<TModel>();
    }
}
