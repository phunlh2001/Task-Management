using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace backend.Models.Dtos.TaskDetail
{
    public class EditTaskDetail
    {

        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid ListId { get; set; }
        [Required]
        [StringLength(5000, ErrorMessage = "{0} cannot over {1} characters")]
        public string Detail { get; set; }
    }
}