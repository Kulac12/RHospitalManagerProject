using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models.CustomModels.EnumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AppointmentService : IAppointmentService
    {

        IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public void Add(Appointment appointment)
        {
            _appointmentRepository.Add(appointment);
        }

        public void Delete(Appointment appointment)
        {
            _appointmentRepository.Delete(appointment);
        }
        public void Update(Appointment appointment)
        {
            _appointmentRepository.Update(appointment);
        }

        public Appointment GetById(Guid id)
        {
            return _appointmentRepository.Get(p => p.Id == id);
        }

        //Enum olduğundan GetByStatus(enum status) değil enum tablosunun ismini verdik
        public List<Appointment> GetByStatus(AppointmentStatus status)
        {
            return _appointmentRepository.GetAll(p => p.Status == status).ToList();
        }

     
    
    }
}
