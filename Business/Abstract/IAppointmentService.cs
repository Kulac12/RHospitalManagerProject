using Entities.Concrete;
using Entities.DTOs;
using Entities.Models.CustomModels.EnumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAppointmentService
    {
        void Add(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(Appointment appointment);

        Appointment GetById(Guid id);
     
        List<Appointment> GetByStatus(AppointmentStatus status);

        void CreateAppointment(AppointmentCreateDto appointmentCreateDto);

        void DeleteAppointment(Guid appointmentId);
    }
}
