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
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [JsonProperty(nameof(Password))]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password not match!")]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}