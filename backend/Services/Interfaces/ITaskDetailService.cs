using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Dtos.TaskDetail;

namespace backend.Services.Interfaces
{
    public interface ITaskDetailService
    {
        Task<TaskDetailResult> GetByIdAsync(Guid id);
        Task<List<TaskDetailResult>> GetByListAsync(Guid id);
        Task<List<TaskDetailResult>> GetAllAsync();
        Task<Guid?> CreateAsync(AddTaskDetail model);
        Task<bool> UpdateAsync(EditTaskDetail model);
        Task<bool> DeleteAsync(Guid id);
    }
}