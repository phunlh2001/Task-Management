using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Entities.Base;

namespace backend.Models.Entities
{
    public class UserWorkSpace: Entity
    {
        [Required]
        [StringLength(500)]
        public string Title { get; set; }
        [Required]
        [StringLength(3000)]
        public string Description { get; set; }

        [Required]
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public AppUser Owner { get; set; }
        public IEnumerable<TaskList> GetTaskLists { get; set; }

    }
}