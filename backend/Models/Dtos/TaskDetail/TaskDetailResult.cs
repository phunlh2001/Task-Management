using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Dtos.TaskDetail
{
    public class TaskDetailResult
    {
        public Guid Id { get; set; }
        public Guid ListId { get; set; }
        public string Detail { get; set; }
    }
}