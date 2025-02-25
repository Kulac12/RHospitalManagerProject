using Core.Entities;
using Core.Entities.Concrete;
using Entities.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Doctor:BaseEntity, IEntity
    {

        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorSpecialty { get; set; }
        public string IdentityNumber { get; set; }

        [ForeignKey(nameof(User))] //bu ilişki FluentApı de verildi
        public int UserId { get; set; }
        public User User { get; set; }




        [ForeignKey(nameof(Polyclinic))]
        public Guid PolyclinicId { get; set; }

        //Tablolar arası ilişkiler 1-n n-n 1-1 gibi
        public Polyclinic Polyclinic { get; set; }

        // 1 doktor, N randevu
        public ICollection<Appointment> Appointment { get; set; }
    }
}
