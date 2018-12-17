namespace <%= solutionName %>.Core.Services.Redis
{
    public interface IRedisCredentialsBase
    {
        string ConnectionString { get; }
    }
}
