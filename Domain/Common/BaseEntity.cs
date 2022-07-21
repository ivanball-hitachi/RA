namespace Domain.Common
{
    public abstract class BaseEntity<T> : IBaseEntity<T>
    {
        public virtual T Id { get; set; } = default!;
    }
}