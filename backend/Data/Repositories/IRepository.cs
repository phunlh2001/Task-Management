using System.Linq.Expressions;
using backend.Models.Entities.Base;
using backend.Models.Interfaces;

namespace backend.Data.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity :class, IBaseEntityDetail
    {
        Task<bool> IsExist(Guid id);
        Task<Guid?> Add(TEntity entity);
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}