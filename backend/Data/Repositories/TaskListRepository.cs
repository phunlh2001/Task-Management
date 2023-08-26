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
            return await All.AsNoTracking()
                .Include(e => e.OfWorkSpace)
                .OrderBy(e => e.Order)
                .ToListAsync();
        }
        
        public async Task<List<TaskList>> GetByWorkSpace(Guid workSpaceId)
        {
            return (await Search(e => e.WorkSpaceId == workSpaceId))
            .OrderByDescending(e=> e.Order)
            .ToList();
        }

        public override async Task<TaskList> GetById(Guid id)
        {
            return await All.AsNoTracking()
                .Include(e => e.OfWorkSpace)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<Guid?> Add(TaskList entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
            return entity.Id;
        }

    }
}