using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Dtos.TaskList;

namespace backend.Services.Interfaces
{
    public interface ITaskListService
    {
        Task<TaskListResult> GetByIdAsync(Guid id);
        Task<List<TaskListResult>> GetByWorkSpaceAsync(Guid id);
        Task<List<TaskListResult>> GetAllAsync();
        Task<Guid?> CreateAsync(AddTaskList model);
        Task<bool> UpdateAsync(EditTaskList model);
        Task<bool> DeleteAsync(Guid id);
    }
}