using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Defaults
{
    public class RoleName
    {
        public static readonly List<string> Roles = new (){
            Member,
            Admin            
        };

        public const string Member = "Member";
        public const string Admin = "Admin";
    }
}