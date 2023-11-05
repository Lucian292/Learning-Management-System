using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ComplexImplementation
{
    public class EFCoreRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly DbSet<TEntity> entities;

        public EFCoreRepository(DbContext dbContext)
        {
            entities = dbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            entities.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            entities.Remove(entity);
        }

        public async Task<TEntity?> FindByIdAsync(Guid id)
        {
            return await entities.FirstOrDefaultAsync(e => e.Id == id);
        }
    }

}