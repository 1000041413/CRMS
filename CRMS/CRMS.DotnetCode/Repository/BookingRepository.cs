using Data;
using Data.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repository
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(CRMSDbContext dbContext) : base(dbContext)
        {
        }

        public async Task SaveBooking(Booking booking)
        {
            if (await IsRoomAvailable(booking))
            {
                // Room is availble, now we can proceed with booking
                Save(booking);
            }
            else
            {
                throw new Exception("Selected room is not available!!!!");
            }
        }

        private async Task<bool> IsRoomAvailable(Booking booking)
        {
            return true;
        }

       
    }
}
