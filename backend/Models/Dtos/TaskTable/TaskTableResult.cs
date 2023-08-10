using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Dtos.TaskTable
{
    public class TaskTableResult
    {
        public Guid Id { get; set; }
        public string Tilte { get; set; }
        public string Color { get; set; }
        public Guid WorkSpaceId { get; set; }
    }
}