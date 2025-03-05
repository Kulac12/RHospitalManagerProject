using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.ReqModels.Appointment;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IResult = Core.Utilities.Results.IResult;


namespace Business.Concrete
{
    public class AppointmentService : IAppointmentService
    {
        IAppointmentRepository _appointmentRepository;
        IUserDal _userDal;
        IPolyclinicRepository _polyclinicRepository;
        IHttpContextAccessor _httpContextAccessor;
        public AppointmentService(IAppointmentRepository appointmentRepository,
            IUserDal userDal,
            IPolyclinicRepository polyclinicRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _appointmentRepository = appointmentRepository;
            _polyclinicRepository = polyclinicRepository;
            _userDal = userDal;
            _httpContextAccessor = httpContextAccessor;
        }

        public IResult Add(AppointmentCreateDto appointmentCreateDto)
        {
            // 1. Randevu tarihi ve saati boş mu diye kontrol et
            if (appointmentCreateDto.AppointmentDate == null || appointmentCreateDto.AppointmentTime == null)
            {
                return new ErrorResult(Messages.InvalidDateOrTime);  // Geçersiz tarih veya saat
            }

            // 2. Randevu tarihi ve saati ile zaten bir randevu var mı diye kontrol et
            var existingAppointment = _appointmentRepository.GetAll(a => a.PatientId == GetPatientIdFromToken() &&  // Aynı hasta
                                                                        a.DoctorId == appointmentCreateDto.DoctorId &&
                                                                        a.AppointmentDate.Date == appointmentCreateDto.AppointmentDate.Date &&  // Aynı tarih
                                                                        a.AppointmentTime == appointmentCreateDto.AppointmentTime &&  // Aynı saat
                                                                        !a.Deleted); // Silinmiş randevuları dikkate alma

            
            if (existingAppointment.Any())
            {
                return new ErrorResult(Messages.AppointmentAlreadyExists);  // Bu tarihte ve saatte zaten bir randevu var
            }

            // 3. Randevu için belirtilen doktorun var olup olmadığını kontrol et
            var doctor = _userDal.Get(u => u.Id == appointmentCreateDto.DoctorId && !u.Deleted);
            if (doctor == null)
            {
                return new ErrorResult(Messages.DoctorNotFound);  // Doktor bulunamadı
            }

            // 4. Doktorun bağlı olduğu poliklinik bilgisi
            var doctorPolyclinicId = doctor.PoliklinikId;

            // 5. Yeni Appointment nesnesi oluştur
            var appointment = new Appointment
            {
                PatientId = GetPatientIdFromToken(),  // AccessToken'dan hasta ID'sini al
                DoctorId = appointmentCreateDto.DoctorId,
                PolyclinicId = (int)doctorPolyclinicId,
                AppointmentDate = appointmentCreateDto.AppointmentDate,
                AppointmentTime = appointmentCreateDto.AppointmentTime,
                Status = "Onaylandı",  // Randevu başlangıçta onaylandı olacak. İptal ederse de iptal erttş yazıcak
                CreateTime = DateTime.Now,
                Deleted = false
            };

            // 6. Randevuyu veritabanına ekle
            _appointmentRepository.Add(appointment);
            return new SuccessResult(Messages.AppointmentAdded);
        }

        private int GetPatientIdFromToken()
        {
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var patientIdClaim = identity?.FindFirst(ClaimTypes.NameIdentifier); // Bu claim, user ID'sini temsil edio.

            return patientIdClaim != null ? Convert.ToInt32(patientIdClaim.Value) : 0;  // 0 döndürülür, eğer bulunamazsa
        }


    }


}
