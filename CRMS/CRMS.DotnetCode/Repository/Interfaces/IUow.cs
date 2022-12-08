using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUow
    {
        IUserRepository UserRepository { get; }
        IRoomRepository RoomRepository { get; }

        IBookingRepository BookingRepository { get; }


        IAuthRepository AuthRepository { get; }

        IRoomTypeRepository RoomTypeRepository { get; }
        void Save();

        Task SaveAsync();
    }
}
