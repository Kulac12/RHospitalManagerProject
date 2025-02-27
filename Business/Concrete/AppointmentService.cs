using Business.Abstract;
using Core.Entities;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Models.CustomModels.EnumModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AppointmentService : IAppointmentService
    {

        IAppointmentRepository _appointmentRepository;
        IPatientRepository _patientRepository;
        IDoctorRepository _doctorRepository;
        IPolyclinicRepository _polyclinicRepository;
        IHttpContextAccessor _httpContextAccessor;
        IUserDal _userDal;

        public AppointmentService(
            IUserDal userDal,
            IPatientRepository patientRepository,
            IAppointmentRepository appointmentRepository,
            IDoctorRepository doctorRepository,
            IPolyclinicRepository polyclinicRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _patientRepository = patientRepository;
            _userDal = userDal;
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _polyclinicRepository = polyclinicRepository;
            _httpContextAccessor = httpContextAccessor;
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

        public void CreateAppointment(AppointmentCreateDto appointmentCreateDto)
        {
            // Doktoru ve Polikliniği bul
            var doctor = _doctorRepository.Get(d => d.DoctorFirstName + " " + d.DoctorLastName == appointmentCreateDto.DoctorName);
            var polyclinic = _polyclinicRepository.Get(p => p.PoliclinicName == appointmentCreateDto.PoliklinikName);

            if (doctor == null || polyclinic == null)
            {
                throw new Exception("Doktor veya Poliklinik bulunamadı.");
            }

            // JWT token'dan UserId'yi al (UserId int olduğu için, bir şekilde int'e dönüştürülmeli)
            var userIdStr = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
            {
                throw new Exception("User bilgisi bulunamadı veya geçersiz.");
            }

            // UserId'ye bağlı olan Patient'ı al
            var patient = _patientRepository.Get(p => p.UserId == userId);

            if (patient == null)
            {
                throw new Exception("Patient bulunamadı.");
            }

            // PatientId'yi al
            var patientId = patient.Id;  // PatientId (Guid)

            // Yeni randevu oluştur
            var appointment = new Appointment
            {
                PatientId = patientId,  // PatientId
                DoctorId = doctor.Id,
                PolyclinicId = polyclinic.Id,
                Date = appointmentCreateDto.AppointmentDate.Date,
                Time = appointmentCreateDto.AppointmentTime.Value,
                Status = AppointmentStatus.Pending
            };

            // Randevuyu kaydet
            _appointmentRepository.Add(appointment);
        }


    }


}

