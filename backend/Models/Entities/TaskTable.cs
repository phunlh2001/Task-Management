using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Entities.Base;

namespace backend.Models.Entities
{
    public class TaskTable: Entity
    {
        public string Tilte { get; set; } = "not set";
        public string Color { get; set; }
        [Required]
        public Guid WorkSpaceId { get; set; }
        [ForeignKey("WorkSpaceId")]
        public UserWorkSpace WorkSpace { get; set; }
        public IEnumerable<TaskList> GetLists { get; set; }
    }
}