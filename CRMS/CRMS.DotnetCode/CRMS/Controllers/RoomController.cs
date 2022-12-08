using Data.Entity;
using Data.Enum;
using Data.ViewModel;
using log4net;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CRMS.Controllers
{
    [RoutePrefix("Room")]
    public class RoomController : BaseApiController
    {
        private IUow _uow;
        private ILog _log;

        public RoomController(IUow uow, ILog log)
        {
            _uow = uow;
            _log = log;
        }
        [Route("GetRooms")]
        public async Task<IHttpActionResult> GetRooms()
        {
            var room = await this._uow.RoomRepository.Get();
            return Ok(room);
        }

        [Route("GetActiveRooms")]
        public async Task<IHttpActionResult> GetActiveRooms()
        {        
            var activeRooms = await this._uow.RoomRepository.Get();
            var filterData = activeRooms.Where(b => b.IsActive == true).ToList();
            return Ok(filterData);
        }

        [Route("GetRoomsLookup")]
        public async Task<IHttpActionResult> GetRoomsLookup()
        {
            var activeRooms = await this._uow.RoomRepository.Get();
            var filterData = activeRooms.Where(b => b.StatusType.Equals(StatusType.Disabled)).ToList();
            return Ok(filterData);
        }

       [HttpPost]
        [Route("Save")]
        public async Task<IHttpActionResult> Save(ConfreneceRoom room)
        {
            try
            {
                _uow.RoomRepository.Save(room);
                await _uow.SaveAsync();
               
            }
            catch(Exception ex)
            {
            }
            return Ok();
        }


        [HttpDelete]        
        [Route("Delete/{id=id}")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            await _uow.RoomRepository.Delete(id);
            await _uow.SaveAsync();
            return Ok();
        }

    }
}
