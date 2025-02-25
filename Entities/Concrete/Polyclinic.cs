using Core.Entities;
using Entities.Concrete.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Polyclinic : BaseEntity, IEntity
    {
        public string PoliclinicName { get; set; }
        public string PoliclinicDescription { get; set; }

        // 1 poliklinik, N doktor
        public ICollection<Doctor> Doctor { get; set; }
    }
}
