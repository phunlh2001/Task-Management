using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Dtos.TaskList
{
    public class EditTaskList
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(5000, ErrorMessage = "{0} cannot over {1} characters")]
        public string Title { get; set; }
        [Required]
        public Guid WorkSpaceId { get; set; }
    }
}