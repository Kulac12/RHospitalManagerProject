using Core.Entities.Concrete;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete.Base;

namespace Entities.Concrete
{
    public class Patient : BaseEntity, IEntity
    {
        public string PatientName { get; set; }
        public string IdentityNumber { get; set; }
        //public string Phone { get; set; }


        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        //// 1 hasta, N randevu
        //public ICollection<Appointment> Appointment { get; set; }

    }


}
