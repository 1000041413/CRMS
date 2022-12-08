using Data.Vadlidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
   public class Booking:BaseEntity
    {
        public Booking()
        {
            this.TransactionId = Guid.NewGuid();
        }
        public Guid TransactionId { get; set; }
        public long RoomId { get; set; }

        public DateTime StartDateTs { get; set; }
        [BookingValidator]
        public DateTime EndDateTs { get; set; }

        public string UserId { get; set; }

        public bool IsCancelled { get; set; }

        [ForeignKey("RoomId")]
        public virtual ConfreneceRoom ConfreneceRoom { get; set; }

        [ForeignKey("UserId")]
        public virtual  User Employee { get; set; }
    }
}
