namespace <%= solutionName %>.Database
{
    using <%= solutionName %>.Database.Configuration;
    using Microsoft.EntityFrameworkCore;

    public class <%= entityName %>DatabaseContext : DbContext
    {
        private readonly <%= entityName %>DatabaseCredentials _credentials;

        public <%= entityName %>DatabaseContext(<%= entityName %>DatabaseCredentials credentials)
        {
            _credentials = credentials;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_credentials.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new <%= entityName %>Configuration());
        }
    }
}
