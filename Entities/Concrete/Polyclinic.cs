using Core.Entities;
using Entities.Concrete.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Polyclinic :BaseEntity, IEntity
    {
        public string Name { get; set; } // Poliklinik adı
        public string Description { get; set; } 

    }
}
