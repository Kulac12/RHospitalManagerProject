using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Entities.DTOs.ReqModels.Appointment
{
    public class AppointmentCreateDto:IDto
    {
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }  // Randevu tarihi

        public TimeSpan AppointmentTime { get; set; }  // Randevu saati
        
    }
}
