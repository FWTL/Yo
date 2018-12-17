namespace <%= solutionName %>.Core.Sql
{
    public interface IDatabaseCredentials
    {
        string ConnectionString { get; }
    }
}
