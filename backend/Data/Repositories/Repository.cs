using System.Linq.Expressions;
using backend.Data.Context;
using backend.Models.Entities;
using backend.Models.Entities.Base;
using backend.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity :class, IBaseEntityDetail
    {
        protected readonly TaskManagerContext Db;

        protected readonly DbSet<TEntity> DbSet;
        protected readonly IQueryable<TEntity> All;

        protected Repository(TaskManagerContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
            All = DbSet.Where(e=>!e.IsDeleted);
        }
        public virtual async Task<bool> IsExist(string id){
            try{
                var entity = await GetById(new Guid(id));
                if(entity == null || entity.IsDeleted) return false;

            }catch(Exception e){
                Console.WriteLine("Repository error: "+e.Message);
                return false;
            }
            return true;
        }

        

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await All.AsNoTracking()
                    .Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var entity = await DbSet.FindAsync(id);
            if(entity==null || entity.IsDeleted) return null;
            return entity;
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await All.ToListAsync();
        }

        public virtual async Task<Guid?> Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
            return default;
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
            entity.DeletedDate = DateTime.Now;
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