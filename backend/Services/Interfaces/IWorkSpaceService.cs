using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Dtos.UserWorkSpace;

namespace backend.Services.Interfaces
{
    public interface IWorkSpaceService
    {
        Task<WorkSpaceResult> GetByIdAsync(Guid id);
        Task<List<WorkSpaceResult>> GetByUserAsync(string id);
        Task<List<WorkSpaceResult>> GetAllAsync();
        Task<Guid?> CreateAsync(AddWorkSpace model, string username);
        Task<bool> UpdateAsync(EditWorkSpace model);
        Task<bool> DeleteAsync(Guid id);
    }
}