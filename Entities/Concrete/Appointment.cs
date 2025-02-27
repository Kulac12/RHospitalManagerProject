using Entities.Models.CustomModels.EnumModels;
using Core.Entities;

using Entities.Concrete.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Appointment : BaseEntity, IEntity
    {
        [ForeignKey(nameof(Patient))]
        public Guid PatientId { get; set; } // Foreign Key
        public Patient Patient { get; set; } // 1 Hasta, N Randevu


        [ForeignKey(nameof(Doctor))]
        public Guid DoctorId { get; set; } // Foreign Key
        public Doctor Doctor { get; set; } // 1 Doktor, N Randevu


        [ForeignKey(nameof(Polyclinic))]
        public Guid PolyclinicId { get; set; } // Foreign Key
        public Polyclinic Polyclinic { get; set; } // 1 Poliklinik, N Randevu


        public DateTime Date { get; set; } // Randevu Tarihi
        public TimeSpan Time { get; set; } // Randevu Saati
        public AppointmentStatus Status { get; set; } // Enum: Beklemede, Onaylandı, İptal Edildi



    }
}
