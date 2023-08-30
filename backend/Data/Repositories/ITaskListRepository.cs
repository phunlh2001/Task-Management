using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Entities;

namespace backend.Data.Repositories
{
    public interface ITaskListRepository: IRepository<TaskList>
    {
        Task<List<TaskList>> GetByWorkSpace(Guid workSpaceID);
    }
}