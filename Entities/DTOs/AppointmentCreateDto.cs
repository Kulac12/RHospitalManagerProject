using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class AppointmentCreateDto:IDto
    {
        public string ?DoctorName { get; set; }
        public string PoliklinikName { get; set; }
        public DateTime AppointmentDate { get; set; }  // Randevu Tarihi (Seçilecek tarih)
        public TimeSpan AppointmentTime { get; set; }  // Randevu Saati (Seçilecek saat)
      

    }
}
