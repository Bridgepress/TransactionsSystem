using TransactionsSystem.Domain.Entities;

namespace TransactionsSystem.Repositories.Contracts.Repositories
{
    public interface IRepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token);

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        public void AddRange(List<TEntity> entity);

        void Delete(TEntity entity);
    }
}
