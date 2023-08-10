using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Interfaces;

namespace backend.Models.Entities.Base
{
    public abstract class Entity: IBaseEntityDetail
    {
        [Key]
        public Guid Id { get; set; }
        public int Order { get; set; }


        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModify { get; set; } = DateTime.Now;
    }
}