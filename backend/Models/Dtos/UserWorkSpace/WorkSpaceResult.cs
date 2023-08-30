using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.Dtos.UserWorkSpace
{
    public class WorkSpaceResult
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "The field {0} cannot over {1} characters")]
        public string Title { get; set; }
        [Required]
        [StringLength(3000, ErrorMessage = "The field {0} cannot over {1} characters")]
        public string Description { get; set; }
        [Required]
        public string OwnerId { get; set; }
    }
}