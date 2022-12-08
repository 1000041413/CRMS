using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class ConfreneceRoom : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }

        public int Capacity { get; set; }

        public bool IsActive { get; set; }

        public long RoomTypeId { get; set; }

        [ForeignKey("RoomTypeId")]
        [JsonIgnore]
        public RoomType RoomType { get; set; }
    }
}
