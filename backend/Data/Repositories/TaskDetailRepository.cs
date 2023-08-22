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
            return await DbSet.AsNoTracking().AsQueryable()
                .Include(e => e.OfList)
                .OrderByDescending(e => e.Order)
                .ToListAsync();
        }
        
        public async Task<List<TaskDetail>> GetByWorkSpace(Guid listId)
        {
            return await DbSet.AsNoTracking().AsQueryable()
                .Include(e => e.OfList)
                .Where(e => e.ListId == listId)
                .OrderByDescending(e => e.Order)
                .ToListAsync();
        }

        public override async Task<TaskDetail> GetById(Guid id)
        {
            return await DbSet.AsNoTracking().AsQueryable()
                .Include(e => e.OfList)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}