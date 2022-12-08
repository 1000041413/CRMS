using Data;
using Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Uow : IUow, IDisposable
    {
         private CRMSDbContext dbContext = new CRMSDbContext();
		
		 private RoomRepository roomRepository;

         private UserRepository userRepository;

        private BookingRepository bookingRepository;

        private RoomTypeRepository roomTypeRepository;
        private IAuthRepository authRepository;

        UserManager<User> _userManager;

        public Uow()
        {
            _userManager = new UserManager<User>(new UserStore<User>(dbContext));
        }

        public IRoomRepository RoomRepository
        {

            get
            {
                if (roomRepository == null)
                    roomRepository = new RoomRepository(dbContext);

                return roomRepository;
            }
        }

        public IRoomTypeRepository RoomTypeRepository
        {

            get
            {
                if (roomTypeRepository == null)
                    roomTypeRepository = new RoomTypeRepository(dbContext);

                return roomTypeRepository;
            }
        }


        public IBookingRepository BookingRepository
        {

            get
            {
                if (bookingRepository == null)
                    bookingRepository = new BookingRepository(dbContext);

                return bookingRepository;
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
          await  dbContext.SaveChangesAsync();
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                    _userManager.Dispose();
                    dbContext.Dispose();
                }

                

                disposedValue = true;
            }
        }

        //  override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Uow() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // : uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }


       public  IUserRepository UserRepository {

            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(dbContext);

                return userRepository;
            }
        }
        

      


        public IAuthRepository AuthRepository {

            get
            {
                if (authRepository == null)
                    authRepository = new AuthRepository(dbContext,_userManager);

                return authRepository;
            }
        }
       
        #endregion
    }
}
