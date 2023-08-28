using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Context;
using backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repositories
{
    public class TaskDetailRepository : Repository<TaskDetail>, ITaskDetailRepository
    {
        public TaskDetailRepository(TaskManagerContext db) : base(db)
        {
        }

        public override async Task<List<TaskDetail>> GetAll()
        {
            return await All.AsNoTracking()
                .Include(e => e.OfList)
                .OrderByDescending(e => e.Order)
                .ToListAsync();
        }
        
        public async Task<List<TaskDetail>> GetByList(Guid listId)
        {
            return (await Search(e => e.ListId == listId))
            .OrderBy(e=> e.Order)
            .ToList();
        }

        public override async Task<TaskDetail> GetById(Guid id)
        {
            return await All.AsNoTracking()
                .Include(e => e.OfList)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public override async Task<Guid?> Add(TaskDetail entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
            return entity.Id;
        }
    }
}