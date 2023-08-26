using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Context;
using backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repositories
{
    public class WorkSpaceRepository : Repository<UserWorkSpace>, IWorkSpaceRepository
    {
        public WorkSpaceRepository(TaskManagerContext db) : base(db)
        {
        }

        public override async Task<List<UserWorkSpace>> GetAll()
        {
            return await All.AsNoTracking().AsQueryable()
                .Include(e => e.Owner)
                .OrderByDescending(e => e.CreatedDate)
                .ToListAsync();
        }

        public override async Task<UserWorkSpace> GetById(Guid id)
        {
            return await All.AsNoTracking().AsQueryable()
                .Include(e => e.Owner)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<UserWorkSpace>> GetUserWorkSpaces(string ownerId)
        {
            return await Search(e => e.OwnerId == ownerId);
        }

        public override async Task<Guid?> Add(UserWorkSpace entity)
        {
            DbSet.Add(entity);
            await Db.SaveChangesAsync();
            Console.WriteLine(entity.Id);
            return entity.Id;
        }
        
    }
}