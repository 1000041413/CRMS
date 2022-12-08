using AutoMapper;
using Data.Entity;
using Data.ViewModel;
using log4net;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace CRMS.Controllers
{
    [RoutePrefix("Booking")]
    public class BookingController : BaseApiController
    {
        private readonly IUow _uow;
        private readonly ILog _log;

        public BookingController(IUow uow, ILog log)
        {
            _uow = uow;
            _log = log;
        }

        [Route("GetBookings")]
        public async Task<IHttpActionResult> GetBookings()
        {
            var oBookingList = await this._uow.BookingRepository.Get();

            var oRoomDetails = await this._uow.RoomRepository.Get();



            // join on the UserId column that is common to both, and select an anonymous type with both pieces of info (you would select just what you need)
            var compositeRepo = oBookingList.Join(oRoomDetails, au => au.RoomId, u => u.Id, (au, u) => new { BookingInfo = au, RoomInfo = u });

            var data = compositeRepo.Select(o => new {
                                                       IsCancelled=o.BookingInfo.IsCancelled,
                                                       StartDateTs=o.BookingInfo.StartDateTs,
                                                       EndDateTs=o.BookingInfo.EndDateTs,
                                                       roomName= o.RoomInfo.Name,
                                                       roomLocation = o.RoomInfo.Location,
                                                       
                                                      }).ToList();
            return Ok(data);
        }

        [Route("GetBookingsByUser/{userId=userId}")]
        public async Task<IHttpActionResult> GetBookingsByUser(string userId)
        {
            var oBookingList = await this._uow.BookingRepository.Get(null, null, "ConfreneceRoom");
            var filterData = oBookingList.Where(b => b.UserId == userId).ToList();
            var bookingByUser = filterData.Select(b => new { b.StartDateTs, b.EndDateTs, roomName = b.ConfreneceRoom.Name, roomLocation = b.ConfreneceRoom.Location,id=b.Id, isCancelled = b.IsCancelled });
                return Ok(bookingByUser);
            
        }
        [Route("GetCurrentMonthBookings/{userId?}")]
        public async Task<IHttpActionResult> GetCurrentMonthBookings(string userId = "")
         {
           
            DateTime dtFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dtTo = dtFrom.AddMonths(1).AddDays(-1);
            List<DateTime> dates = new List<DateTime>();        
              

            var result = await this._uow.BookingRepository.Get(null,null,"ConfreneceRoom,Employee");

            //var filteredData = result.Where(b => b.StartDateTs >= dtFrom && b.EndDateTs <= dtTo).ToList();
            var filteredData = result.ToList();

            if (!string.IsNullOrEmpty(userId))
            {                
                 filteredData = filteredData.Where(b=>b.UserId == userId).ToList();                              
            }
            var data = filteredData.Select(f => new
            {
                startDateTs = f.StartDateTs,
                endDateTs = f.EndDateTs,
                bookedBy = string.Concat(f.Employee.Firstname,' ', f.Employee.Lastname),
                cancelledOnTs = f.ConfreneceRoom.ModifiedTs,
                roomLocation = f.ConfreneceRoom.Location,
                roomName = f.ConfreneceRoom.Name,
                isCancelled=f.IsCancelled
            });
            return Ok(data);

        }


        [HttpPost]
        [Route("save")]
        public async  Task<IHttpActionResult> Save(Booking booking)
        {
            var data = await this._uow.BookingRepository.Get(null,null, "ConfreneceRoom");
            bool isAlreadyExist = data.Any(b =>  b.RoomId == booking.RoomId && b.StartDateTs == booking.StartDateTs &&
                                              b.EndDateTs == booking.EndDateTs);
            if (!isAlreadyExist)
            {
                this._uow.BookingRepository.Insert(booking);
                await this._uow.SaveAsync();
                return Ok();
            }
            else
            {
               throw new NotImplementedException("Please Select Another Start Date and End date");

                // return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent) { Content = new StringContent("Empty result") });
            }
        
        } 
		
        [HttpPut]
        [Route("Cancel")]
        public async Task<IHttpActionResult> CancelBooking(Booking oBooking) //await
		{
            var bk = await _uow.BookingRepository.Get();
            Booking booking = bk.Where(b => b.Id == oBooking.Id).FirstOrDefault();
            
            if (booking == null)
            {
                return NotFound();
            }


            booking.IsCancelled = true;          
             
            await this._uow.SaveAsync();
            return Ok();
            
        } 


         [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IHttpActionResult> Delete(long id) //await
		{
            Booking booking = await _uow.BookingRepository.GetByID(id);
           // Booking booking = bk.Where(b => b.Id == id).FirstOrDefault();
            if (booking == null)
            {
                return NotFound();
            }
            this._uow.BookingRepository.Delete(booking);
            await _uow.SaveAsync();
            return Ok();
        } 
    }
}
