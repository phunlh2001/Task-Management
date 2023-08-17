using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Context;
using backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repositories
{
    public class TaskListRepository : Repository<TaskList>, ITaskListRepository
    {
        public TaskListRepository(TaskManagerContext db) : base(db)
        {
        }

        public override async Task<List<TaskList>> GetAll()
        {
            return await DbSet.AsNoTracking().AsQueryable()
                .Include(e => e.OfWorkSpace)
                .OrderByDescending(e => e.Order)
                .ToListAsync();
        }
        
        public async Task<List<TaskList>> GetByWorkSpace(Guid workSpaceId)
        {
            return await DbSet.AsNoTracking().AsQueryable()
                .Include(e => e.OfWorkSpace)
                .Where(e => e.WorkSpaceId == workSpaceId)
                .OrderByDescending(e => e.Order)
                .ToListAsync();
        }

        public override async Task<TaskList> GetById(Guid id)
        {
            return await DbSet.AsNoTracking().AsQueryable()
                .Include(e => e.OfWorkSpace)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

    }
}