using Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreationTs { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedTs { get; set; }
        public string ModifiedBy { get; set; }
        public StatusType StatusType { get; set; }
    }
}
