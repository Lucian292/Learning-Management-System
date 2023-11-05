namespace Infrastructure.ComplexImplementation
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
        Task<TEntity?> FindByIdAsync(Guid id);
    }
}
