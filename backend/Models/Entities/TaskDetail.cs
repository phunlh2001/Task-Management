using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models.Entities.Base;

namespace backend.Models.Entities
{
    public class TaskDetail: Entity
    {
        [Required]
        [StringLength(5000)]
        public string Detail { get; set; }
        public DateTime DueDate {get; set;} = DateTime.Now.AddDays(1);
        [Required]
        public Guid ListId { get; set; }
        [ForeignKey("ListId")]
        public TaskList OfList { get; set; }
    }
}