using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Storage :IEntity
    {
        public int Id { get; set; }
        public int ProvinceId { get; set; }  //FK
        public string Name { get; set; }
        public string FullAddress { get; set; }
        public string DepoCode { get; set; }
       

        // Navigation Property
        public Province Province { get; set; }
    }
}
