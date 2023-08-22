using System.ComponentModel.DataAnnotations;

namespace backend.Models.Dtos.TaskDetail
{
    public class AddTaskDetail
    {
        [Required]
        [StringLength(5000, ErrorMessage = "{0} cannot over {1} characters")]
        public string Detail { get; set; }
    }
}