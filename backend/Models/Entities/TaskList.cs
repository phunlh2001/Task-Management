using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models.Entities.Base;

namespace backend.Models.Entities
{
    public class TaskList: Entity
    {
        [Required]
        [StringLength(5000)]
        public string Title { get; set; }
        [Required]
        public Guid TableId { get; set; }
        [ForeignKey("TableId")]
        public TaskTable OfTable { get; set; }
        public IEnumerable<TaskDetail> GetDetails { get; set; }
    }
}