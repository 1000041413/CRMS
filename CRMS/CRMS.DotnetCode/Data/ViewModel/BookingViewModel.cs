using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
  public class BookingViewModel
    {
        public long Id { get; set; }
        public Guid TransactionId { get; set; }

        public long RoomId { get; set; }
        public DateTime StartDateTs { get; set; }
        public DateTime EndDateTs { get; set; }
        public string UserId { get; set; }
        public bool IsCancelled { get; set; }

        public string RoomName { get; set; }
        public string RoomLocation { get; set; }
        public string BookedBy { get; set; }

        public DateTime CancelledOnTs { get; set; }
    }
}
