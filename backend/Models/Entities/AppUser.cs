using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace backend.Models.Entities
{
    public class AppUser: IdentityUser, IBaseEntityDetail
    {
        public string FullName { get; set; }
        public string? Address { get; set; } = "not set";
        public string? UserAvatar { get; set; } = "not set";

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModify { get; set; } = DateTime.Now;
    }
}