using System.Linq.Expressions;
using backend.Data.Context;
using backend.Models.Entities;
using backend.Models.Entities.Base;
using backend.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntityDetail
    {
        protected readonly TaskManagerContext Db;

        protected readonly DbSet<TEntity> DbSet;

        protected Repository(TaskManagerContext db)
        {
            Db = db;
            DbSet = (DbSet<TEntity>) db.Set<TEntity>().Where(e=>!e.IsDeleted);
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking()
                    .Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Update(TEntity entity)
        {
            entity.LastModify = DateTime.Now;
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remove(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.LastModify = DateTime.Now;
            DbSet.Update(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}