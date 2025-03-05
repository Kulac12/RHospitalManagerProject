using Core.Entities;
using Core.Entities.Concrete;
using Entities.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Appointment : BaseEntity, IEntity
    {
        [Required]
        public int PatientId { get; set; }  // Hasta (User tablosuna bağlı)

        [ForeignKey("PatientId")]
        public User Patient { get; set; }  // Hasta ile ilişki

        [Required]
        public int DoctorId { get; set; }  // Doktor (User tablosuna bağlı)

        [ForeignKey("DoctorId")]
        public User Doctor { get; set; }  // Doktor ile ilişki

        [Required]
        public int PolyclinicId { get; set; }  // Poliklinik (Foreign Key)

        [ForeignKey("PolyclinicId")]
        public Polyclinic Polyclinic { get; set; }  // Poliklinik ile ilişki

        [Required]
        public DateTime AppointmentDate { get; set; } // Randevu tarihi

        [Required]
        public TimeSpan AppointmentTime { get; set; } // Randevu saati

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Onaylandı"; // Randevu durumu (Onaylandı, İptal Edildi)
    }

}
