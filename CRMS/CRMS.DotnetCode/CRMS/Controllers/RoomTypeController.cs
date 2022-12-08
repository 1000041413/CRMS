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
    [RoutePrefix("RoomType")]
    public class RoomTypeController : BaseApiController
    {
        public RoomTypeController(IUow uow, ILog log)
        {
            _uow = uow;
            _log = log;
        }

        public IUow _uow { get; }
        public ILog _log { get; }

        [Route("GetRoomTypes")]
        public async Task<IHttpActionResult> GetRoomTypes()
        {
            
            var roomType = await this._uow.RoomTypeRepository.Get();
            return Ok(roomType);
        }
    }
}
