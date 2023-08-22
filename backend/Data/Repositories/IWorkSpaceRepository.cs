using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Entities;

namespace backend.Data.Repositories
{
    public interface IWorkSpaceRepository: IRepository<UserWorkSpace>
    {
        Task<IEnumerable<UserWorkSpace>> GetUserWorkSpaces(string ownerId);
        Task<IEnumerable<UserWorkSpace>> SearchBookWithCategory(string searchedValue);
    }
}