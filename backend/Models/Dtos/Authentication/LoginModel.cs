using System.ComponentModel.DataAnnotations;

namespace backend.Models.Dtos.Authentication
{
    public class LoginModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9._@+-]*$")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} must be {2}-{1} characters.")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Invalid {0}!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}