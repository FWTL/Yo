using System.ComponentModel.DataAnnotations;

namespace <%= solutionName %>.Core.Entities
{
    public abstract class BaseEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
