using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DoctorService : IDoctorService
    {
        IDoctorRepository _doctorRepository;
        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public void Add(Doctor doctor)
        {
            _doctorRepository.Add(doctor);
        }

        public Doctor GetByIdentityNumber(string identityNumber)
        {
            return _doctorRepository.GetAll(u => u.IdentityNumber == identityNumber).FirstOrDefault();
        }

        //public List<OperationClaim> GetClaims(User user)
        //{
        //    return _patientRepository.GetClaims(user);
        //}

    }
}
