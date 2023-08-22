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
            return await DbSet.AsNoTracking().AsQueryable()
                .Include(e => e.Owner)
                .OrderByDescending(e => e.CreatedDate)
                .ToListAsync();
        }

        public override async Task<UserWorkSpace> GetById(Guid id)
        {
            return await DbSet.AsNoTracking().AsQueryable()
                .Include(e => e.Owner)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<UserWorkSpace>> GetUserWorkSpaces(string ownerId)
        {
            return await Search(e => e.OwnerId == ownerId);
        }

        public async Task<IEnumerable<UserWorkSpace>> SearchBookWithCategory(string searchedValue)
        {
            return await DbSet.AsNoTracking().AsQueryable()
                .Include(e => e.Owner)
                .Where(e => e.Title.Contains(searchedValue) ||
                            e.Owner.UserName.Contains(searchedValue) ||
                            e.Owner.FullName.Contains(searchedValue) ||
                            e.Description.Contains(searchedValue))
                .ToListAsync();
        }
    }
}