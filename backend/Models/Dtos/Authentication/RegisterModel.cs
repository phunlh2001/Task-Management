using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace backend.Models.Dtos.Authentication
{
    public class RegisterModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9._@+-]*$", ErrorMessage = "Invalid {0}!")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} must be {2}-{1} characters.")]
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Invalid {0}!")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password not match!")]
        public string ConfirmPassword { get; set; }
        // [EmailAddress]
        // public string Email { get; set; }
    }
}