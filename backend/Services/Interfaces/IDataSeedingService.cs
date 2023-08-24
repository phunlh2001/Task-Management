using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Entities;
using Bogus;

namespace backend.Services.Interfaces
{
    public interface IDataSeedingService
    {
        bool SeedWorSpace(int number = 1);
        bool SeedTaskList(Guid workSpaceId, int number = 1);
        bool SeedTaskDetails(Guid listId, int number = 1);
    }
}