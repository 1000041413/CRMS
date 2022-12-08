using Data;
using Data.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public class RoomTypeRepository: GenericRepository<RoomType>, IRoomTypeRepository
    {
        private readonly CRMSDbContext _dBContext;
        public RoomTypeRepository(CRMSDbContext dbContext) : base(dbContext)
        {
            _dBContext = dbContext;
        }
    }
}
