using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PatientService : IPatientService
    {
        IPatientRepository _patientRepository;
        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public void Add(Patient patient)
        {
            _patientRepository.Add(patient);
        }

        public Patient GetByIdentityNumber(string identityNumber)
        {
            return _patientRepository.Get(u => u.IdentityNumber == identityNumber);
        }

        //public List<OperationClaim> GetClaims(User user)
        //{
        //    return _patientRepository.GetClaims(user);
        //}
    }
}
