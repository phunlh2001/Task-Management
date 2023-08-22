using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Dtos.TaskList
{
    public class TaskListResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid WorkSpaceId { get; set; }
    }
}