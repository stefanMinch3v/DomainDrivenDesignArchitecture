namespace PetClinic.Infrastructure.Common.Persistence.Models
{
    public abstract class BaseDbEntity<TKey>
        where TKey : struct
    {
        public TKey Id { get; set; }
    }
}
