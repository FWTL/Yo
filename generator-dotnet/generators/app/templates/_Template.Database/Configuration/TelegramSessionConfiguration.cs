namespace <%= solutionName %>.Database.Configuration
{
    using <%= solutionName %>.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class JobConfiguration : IEntityTypeConfiguration<<%= entityName %>>
    {
        public void Configure(EntityTypeBuilder<<%= entityName %>> builder)
        {
          
        }
    }
}
