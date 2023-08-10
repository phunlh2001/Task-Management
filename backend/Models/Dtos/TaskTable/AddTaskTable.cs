using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Dtos.TaskTable
{
    public class AddTaskTable
    {
        [Required]
        [StringLength(150, ErrorMessage = "The field {0} cannot over {1} characters")]
        public string Tilte { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public Guid WorkSpaceId { get; set; }
    }
}